using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TechnicalTest.Models
{
    [Table("clientes")]
    public class Cliente
    {
        [Key, Column("clienteID")]
        public int ID { get; set; }

        [Column("RUT"), Index(IsUnique = true)]
        [Required(ErrorMessage = "Debe introducir un RUT!")]
        [Range(0.0, Double.MaxValue, ErrorMessage = "El valor debe ser mayor a 0!")]
        public int RUT { get; set; }

        [Column("nombre"), Index(IsUnique = true), ConcurrencyCheck]
        [MinLength(2, ErrorMessage = "El nombre del cliente debe contener al menos 2 caracteres.")]
        [MaxLength(255, ErrorMessage = "El campo no puede exceder 255 caracteres!")]
        [Required(ErrorMessage = "Debe introducir el nombre del cliente!")]
        public string nombreCliente { get; set; }

        [MinLength(2, ErrorMessage = "El cargo debe contener al menos 2 caracteres!")]
        [Required(ErrorMessage = "Debe introducir el nombre del cargo!")]
        public string cargoCliente { get; set; }

        public Cliente() { }

        public Cliente(int ID, int RUT, string nombreCliente)
        {
            this.ID = ID;
            this.RUT = RUT;
            this.nombreCliente = nombreCliente;
        }
    }
}