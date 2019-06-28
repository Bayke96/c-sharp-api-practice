using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using TechnicalTest.Models;

namespace TechnicalTest.Modelos
{
    [Table("facturas")]
    public class Factura
    {
        public int ID { get; set; }

        [Required]
        public DateTime fecha { get; set; } = DateTime.Now;

        [Column("clienteFK")]
        public int clienteFK { get; set; }

        [ForeignKey("clienteFK")]
        [JsonIgnore]
        private Cliente cliente { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Debe incluir una lista de productos")]
        [JsonRequired]
        public List<DetallesFactura> detallesVenta { get; set; }
    }
}