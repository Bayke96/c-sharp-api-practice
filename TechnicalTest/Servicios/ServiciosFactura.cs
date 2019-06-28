using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using TechnicalTest.DB_Config;
using TechnicalTest.Modelos;

namespace TechnicalTest.Servicios.Clases
{
    public static class ServiciosFactura
    {
        public static JArray BuscarFactura(int facturaID)
        {
            using (var context = new ServicesContext())
            {
                var factura = context.facturas.Where(x => x.ID == facturaID).
                    Select(x => new { x.ID, x.fecha, x.clienteFK }).FirstOrDefault();

                // La factura no fue encontrada, retornar nulo.
                if(factura == null)
                {
                    return null;
                }
                else
                {
                    var datosCliente = (from c in context.clientes
                                        join f in context.facturas on c.ID equals f.clienteFK
                                        where c.ID == factura.clienteFK
                                        select new
                                        {
                                            c.RUT,
                                            c.nombreCliente,
                                            c.cargoCliente
                                        }).Take(1);

                    var productosFactura = (from producto in context.productos
                                            join d in context.detallesFacturas on producto.ID equals d.productoFK
                                            where d.facturaFK == facturaID
                                            select new
                                            {
                                                producto
                                            }).ToList();


                    JArray arrayJSON = new JArray();
                    arrayJSON.Add("datosCliente");
                    arrayJSON.Add(JArray.FromObject(datosCliente));
                    arrayJSON.Add("datosFactura");
                    arrayJSON.Add(JObject.FromObject(factura));
                    arrayJSON.Add("productosFactura");
                    arrayJSON.Add(JArray.FromObject(productosFactura));

                    return arrayJSON;
                }
            }
        }

        public static JArray CrearFactura(Factura factura)
        {
            using(var context = new ServicesContext())
            {
                context.facturas.Add(factura);
                context.SaveChanges();

                var ultimaFactura = context.facturas.OrderByDescending(x => x.ID).
                    Select(x => new { x.ID, x.fecha, x.clienteFK }).Take(1).FirstOrDefault();

                for (int i = 0; i < factura.detallesVenta.Count; i++)
                {
                    factura.detallesVenta.ElementAt(i).facturaFK = ultimaFactura.ID;
                }
                ServiciosDetallesFactura.CrearDetallesFactura(factura.detallesVenta);

                JArray arrayJSON = BuscarFactura(ultimaFactura.ID);
                return arrayJSON;
            }
        }

        public static JArray EliminarFactura(int facturaID)
        {
            JArray arrayJSON = BuscarFactura(facturaID);
            using (var context = new ServicesContext())
            {
                var factura = context.facturas.Where(x => x.ID == facturaID).
                    Select(x => new { x.ID, x.fecha, x.clienteFK }).FirstOrDefault();

                // La factura no existe, retornar nulo.
                if(factura == null)
                {
                    return null;
                }
                else
                {
                    var encontrarFactura = context.facturas.SingleOrDefault(x => x.ID == facturaID);
                    context.facturas.Remove(encontrarFactura);
                    ServiciosDetallesFactura.RemoverDetallesFactura(facturaID);

                    context.SaveChanges();
                    return arrayJSON;
                }
            }
        }

    }
}