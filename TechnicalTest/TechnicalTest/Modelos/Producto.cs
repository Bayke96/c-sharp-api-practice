using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TechnicalTest.Modelos
{
    [Table("productos")]
    public class Producto
    {
        [Key]
        public int ID { get; set; }

        [ConcurrencyCheck, Index(IsUnique = true)]
        [MinLength(3, ErrorMessage = "El nombre del producto debe contener al menos 3 caracteres!")]
        [MaxLength(255, ErrorMessage = "El campo no puede exceder 255 caracteres!")]
        [Required(ErrorMessage = "El nombre del producto es obligatorio")]
        public string nombreProducto { get; set; }
        [DefaultValue(0)]
        [Required(ErrorMessage = "Debe introducir un precio!")]
        [Range(0.0, Double.MaxValue, ErrorMessage = "El valor debe ser mayor a 0!")]

        public decimal precioProducto { get; set; }
    }
}