using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnicalTest.DB_Config;
using TechnicalTest.Modelos;
using TechnicalTest.Models;

namespace TechnicalTest.Servicios.Clases
{
    public static class ServiciosDetallesFactura
    {
        public static IEnumerable<DetallesFactura> BuscarDetallesFactura(int facturaID)
        {
            using(var context = new ServicesContext())
            {
                var listaDetalles = context.detallesFacturas.ToList().Where(x => x.facturaFK == facturaID);
                return listaDetalles;
            }
        }

        public static Cliente CrearDetallesFactura(DetallesFactura[] listaProductos)
        {
            using (var context = new ServicesContext())
            {
                if(listaProductos.Length > 0)
                {
                    for (int i = 0; i < listaProductos.Length; i++)
                    {
                        context.detallesFacturas.Add(listaProductos[i]);
                    }
                    int facturaID = listaProductos.First().facturaFK;
                    var joinClienteID = (from f in context.facturas
                                     join d in context.detallesFacturas on f.ID equals facturaID
                                     select new
                                     {
                                         f.clienteFK
                                     }).SingleOrDefault().ToString();

                    int clienteID = Int32.Parse(joinClienteID);

                    var cliente = (from c in context.clientes
                                join f in context.facturas on c.ID equals clienteID
                                select new
                                {
                                    c.ID,
                                    c.nombreCliente,
                                    c.RUT
                                }).SingleOrDefault();

                    context.SaveChanges();
                    Cliente infoCliente = new Cliente(cliente.ID, cliente.RUT, cliente.nombreCliente);
                    return infoCliente;
                }
                else
                {
                    return null;
                }
            }
        }

        public static IEnumerable<DetallesFactura> RemoverDetallesFactura(int facturaID)
        {
            using(var context = new ServicesContext())
            {
                var factura = context.facturas.FirstOrDefault(x => x.ID == facturaID);
                var listaProductos = context.detallesFacturas.Where(x => x.facturaFK == facturaID).ToList();
                for(int i = 0; i < listaProductos.Count; i++)
                {
                    context.detallesFacturas.Remove(listaProductos[i]);
                    context.facturas.Remove(factura);
                }
                context.SaveChanges();
                return listaProductos;
            }
        }
    }
}