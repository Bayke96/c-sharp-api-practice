using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TechnicalTest.DB_Config;
using TechnicalTest.Models;

namespace TechnicalTest.Servicios
{
    public static class ServiciosCliente { 

        public static IList BuscarClientes()
        {
            using(var context = new ServicesContext())
            {
                var lista = context.clientes.ToList();
                if (lista.Count > 0)
                {
                    var listaClientes = lista.Select(x => new { x.RUT, x.nombreCliente, x.cargoCliente }).ToList();
                    return listaClientes;
                }
                else
                {
                    return null;
                }
            }
        }

        public static Cliente BuscarCliente(int RUT)
        {
            using(var context = new ServicesContext())
            {
                var cliente = context.clientes.SingleOrDefault(x => x.RUT == RUT);
                return cliente;
            }
        }

        public static Cliente CrearCliente(Cliente cliente)
        {
            string nombreCliente = cliente.nombreCliente.ToUpper();
            using(var context = new ServicesContext())
            {
                var encontrarCliente = context.clientes.SingleOrDefault(
                    x => x.nombreCliente.ToUpper() == nombreCliente);

                if(encontrarCliente != null)
                {
                    return null;
                }
                else
                {
                    context.clientes.Add(cliente);
                    context.SaveChanges();
                    var ultimoCliente = context.clientes.OrderByDescending(x => x.ID).FirstOrDefault();
                    return ultimoCliente;
                }
            }
        }

        public static Cliente ModificarCliente(int numeroRUT, Cliente cliente)
        {
            using(var context = new ServicesContext())
            {
                var encontrarCliente = context.clientes.SingleOrDefault(x => x.RUT == numeroRUT);
                if(encontrarCliente != null)
                {
                    encontrarCliente.nombreCliente = cliente.nombreCliente.Trim();
                    encontrarCliente.RUT = cliente.RUT;
                    encontrarCliente.cargoCliente = cliente.cargoCliente.Trim();
                    context.SaveChanges();
                    return encontrarCliente;
                }
                else
                {
                    return null;
                }
            }
        }

        public static Cliente EliminarCliente(int numeroRUT)
        {
            using(var context = new ServicesContext())
            {
                var encontrarCliente = context.clientes.SingleOrDefault(x => x.RUT == numeroRUT);
                context.clientes.Remove(encontrarCliente);
                context.SaveChanges();
                return encontrarCliente;
            }
        }

    }
}