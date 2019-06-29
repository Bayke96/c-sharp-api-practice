using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public static Cliente CrearDetallesFactura(List<DetallesFactura> listaProductos)
        {
            using (var context = new ServicesContext())
            {
                if(listaProductos.Count > 0)
                {
                    for (int i = 0; i < listaProductos.Count; i++)
                    {
                        context.detallesFacturas.Add(listaProductos.ElementAt(i));
                    }
                    context.BulkInsert(listaProductos);
                    int facturaID = listaProductos.FirstOrDefault().facturaFK;

                    int[] encontrarFKCliente = (from f in context.facturas
                                     join d in context.detallesFacturas on f.ID equals d.facturaFK where f.ID == facturaID
                                     select f.clienteFK
                                     ).ToArray();

                    int clienteID = encontrarFKCliente.First();

                    var datosCliente = (from c in context.clientes
                                join f in context.facturas on c.ID equals f.clienteFK where c.ID == clienteID
                                select new
                                {
                                    c.ID,
                                    c.nombreCliente,
                                    c.RUT
                                }).FirstOrDefault();

                    Cliente infoCliente = new Cliente(datosCliente.ID, datosCliente.RUT, datosCliente.nombreCliente);
                    return infoCliente;
                }
                else
                {
                    return null;
                }
            }
        }

        public static JArray RemoverDetallesFactura(int facturaID)
        {
            using(var context = new ServicesContext())
            {
                var listaProductos = context.detallesFacturas.Where(x => x.facturaFK == facturaID).ToList();
                for(int i = 0; i < listaProductos.Count; i++)
                {
                    context.detallesFacturas.Remove(listaProductos[i]);
                }
                context.SaveChanges();

                JArray arrayJSON = new JArray();
                arrayJSON.Add("productosFactura");
                arrayJSON.Add(JArray.FromObject(listaProductos));

                return arrayJSON;
            }
        }
    }
}