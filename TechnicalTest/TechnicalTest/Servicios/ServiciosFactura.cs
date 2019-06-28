using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnicalTest.DB_Config;
using TechnicalTest.Modelos;

namespace TechnicalTest.Servicios.Clases
{
    public static class ServiciosFactura
    {
        public static Dictionary<string, string[]> BuscarFactura(int facturaID)
        {

            using (var context = new ServicesContext())
            {
                var factura = context.facturas.SingleOrDefault(x => x.ID == facturaID).ToString();
                var clienteFK = context.facturas.FirstOrDefault(x => x.ID == facturaID);

                var datosCliente = (from c in context.clientes
                                        join f in context.facturas on c.ID equals f.clienteFK
                                    where c.ID == facturaID
                                        select new
                                        {
                                            c.RUT,
                                            c.nombreCliente,
                                            c.cargoCliente
                                        }).ToList().ToString();

                var productosFactura = (from p in context.productos
                                        join d in context.detallesFacturas on facturaID equals d.productoFK
                                        where d.facturaFK == facturaID
                                        select new
                                        {
                                            p
                                        }).ToList().ToString();

                var datosFactura = new Dictionary<string, string[]>();
                datosFactura.Add("factura", new string[2]);
                datosFactura["factura"][0] = factura;
                datosFactura["factura"][1] = datosCliente;
                datosFactura["factura"][2] = productosFactura;

                return datosFactura;
            }
        }

        public static Dictionary<string, string[]> CrearFactura(Factura factura, DetallesFactura[] listaProductos)
        {
            using(var context = new ServicesContext())
            {
                context.facturas.Add(factura);
                ServiciosDetallesFactura.CrearDetallesFactura(listaProductos);
                context.SaveChanges();

                var ultimaFactura = context.facturas.LastOrDefault();

                var datosCliente = (from c in context.clientes
                                    join f in context.facturas on c.ID equals f.clienteFK where c.ID == factura.clienteFK
                                    select new
                                    {
                                        c.RUT,
                                        c.nombreCliente,
                                        c.cargoCliente
                                    }).ToList().ToString();

                var productosFactura = (from p in context.productos
                                        join d in context.detallesFacturas on p.ID equals d.productoFK
                                        where d.facturaFK == context.facturas.LastOrDefault().ID
                                        select new
                                        {
                                            p
                                        }).ToList().ToString();


                var datosFactura = new Dictionary<string, string[]>();
                datosFactura.Add("factura", new string[2]);
                datosFactura["factura"][0] = ultimaFactura.ToString();
                datosFactura["factura"][1] = datosCliente;
                datosFactura["factura"][2] = productosFactura;

                return datosFactura;
            }
        }

        public static Dictionary<string, string[]> EliminarFactura(int facturaID)
        {
            using(var context = new ServicesContext())
            {
                var encontrarFactura = context.facturas.SingleOrDefault(x => x.ID == facturaID);
                context.facturas.Remove(encontrarFactura);
                var facturaProductos = context.detallesFacturas.SingleOrDefault(x => x.ID == facturaID);
                context.detallesFacturas.Remove(facturaProductos);
                context.SaveChanges();

                var datosCliente = (from c in context.clientes
                                    join f in context.facturas on c.ID equals f.clienteFK
                                    where c.ID == facturaID
                                    select new
                                    {
                                        c.RUT,
                                        c.nombreCliente,
                                        c.cargoCliente
                                    }).ToList().ToString();

                var productosFactura = (from p in context.productos
                                        join d in context.detallesFacturas on facturaID equals d.productoFK
                                        where d.facturaFK == facturaID
                                        select new
                                        {
                                            p
                                        }).ToList().ToString();


                var datosFactura = new Dictionary<string, string[]>();
                datosFactura.Add("factura", new string[2]);
                datosFactura["factura"][0] = encontrarFactura.ToString();
                datosFactura["factura"][1] = datosCliente;
                datosFactura["factura"][2] = productosFactura;

                return datosFactura;
            }
        }

    }
}