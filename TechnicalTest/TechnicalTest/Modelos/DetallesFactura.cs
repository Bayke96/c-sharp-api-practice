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
    [Table("detalles_facturas")]
    public class DetallesFactura
    {
        [Key]
        public int ID { get; set; }

        [Column("facturaFK")]
        [Required(ErrorMessage = "La FK de la factura es obligatoria.")]
        public int facturaFK { get; set; }

        [ForeignKey("facturaFK")]
        [JsonIgnore]
        private Factura factura { get; set; }

        [Column("productoFK")]
        [Required(ErrorMessage = "La FK del producto es obligatoria.")]
        public int productoFK { get; set; }

        [ForeignKey("productoFK")]
        [JsonIgnore]
        private Producto producto { get; set; }
    }
}