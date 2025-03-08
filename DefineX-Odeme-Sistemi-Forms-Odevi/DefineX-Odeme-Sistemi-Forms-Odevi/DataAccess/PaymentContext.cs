using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefineX_Odeme_Sistemi_Forms_Odevi.DataAccess
{
    public class PaymentContext : DbContext
    {
        public DbSet<PaymentType> PaymentTypes { get; set; }

        public PaymentContext(DbContextOptions<PaymentContext> options)
            : base(options)
        {
        }

        // Eğer OnConfiguring metodunu kullanmak isterseniz:
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["defineX_Payment"].ConnectionString;
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

    }
}
