using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnicalTest.DB_Config;
using TechnicalTest.Modelos;

namespace TechnicalTest.Servicios.Clases
{
    public static class ServiciosProducto
    {
        public static List<Producto> BuscarProductos()
        {
            using (var context = new ServicesContext())
            {
                var listaProductos = context.productos.ToList();
                return listaProductos;
            }
        }
        public static Producto BuscarProducto(int productoID)
        {
            using (var context = new ServicesContext())
            {
                var producto = context.productos.FirstOrDefault(x => x.ID == productoID);
                return producto;
            }
        }

        public static Producto CrearProducto(Producto producto)
        {
            using (var context = new ServicesContext())
            {
                context.productos.Add(producto);
                context.SaveChanges();
                var ultimoProducto = context.productos.OrderByDescending(x => x.ID).FirstOrDefault();
                return ultimoProducto;
            }
        }

        public static Producto EditarProducto(int productoID, Producto producto)
        {
            using(var context = new ServicesContext())
            {
                var encontrarProducto = context.productos.SingleOrDefault(x => x.ID == productoID);
                encontrarProducto.nombreProducto = producto.nombreProducto.Trim();
                encontrarProducto.precioProducto = producto.precioProducto;
                context.SaveChanges();
                return encontrarProducto;
            }
        }

        public static Producto EliminarProducto(int productoID)
        {
            using(var context = new ServicesContext())
            {
                var encontrarProducto = context.productos.SingleOrDefault(x => x.ID == productoID);
                context.productos.Remove(encontrarProducto);
                context.SaveChanges();
                return encontrarProducto;
            }
        }
    }
}