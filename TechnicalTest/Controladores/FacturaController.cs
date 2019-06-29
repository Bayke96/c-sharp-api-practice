using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using TechnicalTest.Modelos;
using TechnicalTest.Servicios.Clases;

namespace TechnicalTest.Controladores
{
    public class FacturaController : ApiController
    {

        [HttpGet]
        [Route("facturas/{facturaID}")]
        public IHttpActionResult GetFactura(int facturaID)
        {
            var factura = ServiciosFactura.BuscarFactura(facturaID);

            if(factura == null)
            {
                return NotFound();
            }
            else
            {
                HttpContext.Current.Response.AppendHeader("URL", Request.RequestUri.AbsoluteUri);

                JsonSerializerSettings serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
                return Json(factura, serializerSettings);
            }
        }

        [HttpPost]
        [Route("facturas")]
        public HttpResponseMessage PostFacturas(Factura factura)
        {
            var facturaCreada = ServiciosFactura.CrearFactura(factura);
            string datosFactura = facturaCreada.Children().ElementAt(3).First().ToString();
            string facturaID = Regex.Replace(datosFactura, "[^0-9]", "");

            var response = Request.CreateResponse(HttpStatusCode.Created, facturaCreada, Configuration.Formatters.JsonFormatter);

            if (Request.RequestUri.AbsoluteUri.EndsWith("/"))
            {
                response.Headers.Add("URL", Request.RequestUri.AbsoluteUri + facturaID);
            }
            else
            {
                response.Headers.Add("URL", Request.RequestUri.AbsoluteUri + "/" + facturaID);
            }
            return response;
        }

        [HttpDelete]
        [Route("facturas/{facturaID}")]
        public HttpResponseMessage EliminarFactura(int facturaID)
        {
            var facturaEliminada = ServiciosFactura.EliminarFactura(facturaID);

            if(facturaEliminada != null)
            {
                var response = Request.CreateResponse(HttpStatusCode.OK, facturaEliminada, Configuration.Formatters.JsonFormatter);
                return response;
            }
            else
            {
                string error = "Una factura con este ID no fue encontrada!";
                var response = Request.CreateResponse(HttpStatusCode.NotFound, error, Configuration.Formatters.JsonFormatter);
                return response;
            }
            
        }

    }
}
