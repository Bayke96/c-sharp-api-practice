using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TechnicalTest.Modelos;
using TechnicalTest.Models;

namespace TechnicalTest.DB_Config
{
    public class ServicesContext : DbContext
    {
        public ServicesContext()
            : base("ServicioVentas")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ServicesContext>());
        }

        public DbSet<Cliente> clientes { get; set; }
        public DbSet<DetallesFactura> detallesFacturas { get; set; }
        public DbSet<Factura> facturas { get; set; }
        public DbSet<Producto> productos { get; set; }
    }
}