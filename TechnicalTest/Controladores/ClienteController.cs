using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TechnicalTest.Models;
using TechnicalTest.Servicios;

namespace TechnicalTest.Controladores
{
    public class ClienteController : ApiController
    {
        [HttpGet]
        [Route("clientes")]
        public IHttpActionResult GetClientes()
        {
            var listaClientes = ServiciosCliente.BuscarClientes();
            if(listaClientes == null)
            {
                return NotFound();
            }
            else
            {
                HttpContext.Current.Response.AppendHeader("URL", Request.RequestUri.AbsoluteUri);

                JsonSerializerSettings serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
                return Json(listaClientes, serializerSettings);
            }
        }

        [HttpGet]
        [Route("clientes/{id}")]
        public IHttpActionResult GetClientes(int id)
        {
            var listaClientes = ServiciosCliente.BuscarCliente(id);
            if (listaClientes == null)
            {
                return NotFound();
            }
            else
            {
                HttpContext.Current.Response.AppendHeader("URL", Request.RequestUri.AbsoluteUri);

                JsonSerializerSettings serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
                return Json(listaClientes, serializerSettings);
            }
        }

        [HttpPost]
        [Route("clientes")]
        public HttpResponseMessage PostCliente(Cliente cliente)
        {
            var clienteCreado = ServiciosCliente.CrearCliente(cliente);

            // Si ya existe un cliente con ese nombre, retornar error.
            if (clienteCreado == null)
            {
                var alreadyExistsResponse = Request.CreateResponse
                    (HttpStatusCode.Conflict, "(409) Cliente ya existe o no fue encontrado", Configuration.Formatters.JsonFormatter);

                return alreadyExistsResponse;
            }
            // En otro caso, proceder.
            else
            {
                var response = Request.CreateResponse(HttpStatusCode.Created, clienteCreado, Configuration.Formatters.JsonFormatter);

                if (Request.RequestUri.AbsoluteUri.EndsWith("/"))
                {
                    response.Headers.Add("URL", Request.RequestUri.AbsoluteUri + clienteCreado.ID);
                }
                else
                {
                    response.Headers.Add("URL", Request.RequestUri.AbsoluteUri + "/" + clienteCreado.ID);
                }
                return response;
            }
        }

        [HttpPut]
        [Route("clientes/{rutID}")]
        public HttpResponseMessage EditarCliente(int rutID, Cliente cliente)
        {
            var clienteModificado = ServiciosCliente.ModificarCliente(rutID, cliente);

            var response = Request.CreateResponse(HttpStatusCode.OK, clienteModificado, Configuration.Formatters.JsonFormatter);
            response.Headers.Add("URL", Request.RequestUri.AbsoluteUri);

            if (clienteModificado == null)
            {
                var notFoundResponse = Request.CreateResponse(HttpStatusCode.NotFound,
                    "(404) Cliente no encontrado",
                    Configuration.Formatters.JsonFormatter);

                return notFoundResponse;
            }
            else
            {
                return response;
            }
        }

        [HttpDelete]
        [Route("clientes/{rutID}")]
        public HttpResponseMessage EliminarCliente(int rutID)
        {
            var clienteEliminado = ServiciosCliente.EliminarCliente(rutID);

            var response = Request.CreateResponse(HttpStatusCode.OK, clienteEliminado, Configuration.Formatters.JsonFormatter);

            if (clienteEliminado == null)
            {
                var notFoundResponse = Request.CreateResponse(HttpStatusCode.NotFound, "(404) Cliente no encontrado",
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
