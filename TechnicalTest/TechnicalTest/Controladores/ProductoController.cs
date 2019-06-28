using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TechnicalTest.Modelos;
using TechnicalTest.Servicios.Clases;

namespace TechnicalTest.Controladores
{
    public class ProductoController : ApiController
    {
        [HttpGet]
        [Route("productos")]
        public IHttpActionResult GetProductos()
        {
            var listaProductos = ServiciosProducto.BuscarProductos();

            HttpContext.Current.Response.AppendHeader("URL", Request.RequestUri.AbsoluteUri);

            JsonSerializerSettings serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
            return Json(listaProductos, serializerSettings);
        }

        [HttpPost]
        [Route("productos")]
        public HttpResponseMessage PostProductos(Producto producto)
        {
            var productoCreado = ServiciosProducto.CrearProducto(producto);

            // Si ya existe un cliente con ese nombre, retornar error.
            if (productoCreado == null)
            {
                var alreadyExistsResponse = Request.CreateResponse
                    (HttpStatusCode.Conflict, "(409) Producto ya existe", Configuration.Formatters.JsonFormatter);

                return alreadyExistsResponse;
            }
            // En otro caso, proceder.
            else
            {
                var response = Request.CreateResponse(HttpStatusCode.Created, productoCreado, Configuration.Formatters.JsonFormatter);

                if (Request.RequestUri.AbsoluteUri.EndsWith("/"))
                {
                    response.Headers.Add("URL", Request.RequestUri.AbsoluteUri + productoCreado.ID);
                }
                else
                {
                    response.Headers.Add("URL", Request.RequestUri.AbsoluteUri + "/" + productoCreado.ID);
                }
                return response;
            }
        }

        [HttpPut]
        [Route("productos/{productoID}")]
        public HttpResponseMessage EditarProducto(int productoID, Producto producto)
        {
            var productoModificado = ServiciosProducto.EditarProducto(productoID, producto);

            var response = Request.CreateResponse(HttpStatusCode.OK, productoModificado, Configuration.Formatters.JsonFormatter);
            response.Headers.Add("URL", Request.RequestUri.AbsoluteUri);

            if (productoModificado == null)
            {
                var notFoundResponse = Request.CreateResponse(HttpStatusCode.NotFound,
                    "(404) Producto no encontrado",
                    Configuration.Formatters.JsonFormatter);

                return notFoundResponse;
            }
            else
            {
                return response;
            }
        }

        [HttpDelete]
        [Route("productos/{productoID}")]
        public HttpResponseMessage EliminarProducto(int productoID)
        {
            var productoEliminado = ServiciosProducto.EliminarProducto(productoID);

            var response = Request.CreateResponse(HttpStatusCode.OK, productoEliminado, Configuration.Formatters.JsonFormatter);

            if (productoEliminado == null)
            {
                var notFoundResponse = Request.CreateResponse(HttpStatusCode.NotFound, "(404) Producto no encontrado",
                    Configuration.Formatters.JsonFormatter);

                return notFoundResponse;
            }
            else
            {
                return response;
            }
        }
    }
}
