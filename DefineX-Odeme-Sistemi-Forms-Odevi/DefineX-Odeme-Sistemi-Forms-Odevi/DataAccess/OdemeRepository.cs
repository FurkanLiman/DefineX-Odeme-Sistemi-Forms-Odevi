using Microsoft.Data.SqlClient;
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
    public class OdemeRepository
    {
        private readonly PaymentContext _context;

        // DI ile PaymentContext'in dışarıdan alınması durumunda kullanılacak constructor.
        public OdemeRepository(PaymentContext context)
        {
            _context = context;
        }

        // DI kullanılmıyorsa, App.config üzerinden connection string okuyup context oluşturur.
        public OdemeRepository() : this(CreateContextFromConfiguration())
        {
        }

        private static PaymentContext CreateContextFromConfiguration()
        {
            // App.config üzerinden connection string'i alıyoruz.
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["defineX_Payment"].ConnectionString;
            var optionsBuilder = new DbContextOptionsBuilder<PaymentContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return new PaymentContext(optionsBuilder.Options);
        }

        public Dictionary<string, string> GetOdemeYontemleri()
        {
            // PaymentTypes tablosundaki verileri çekip, sözlük olarak döndürür.
            return _context.PaymentTypes
                .AsNoTracking()
                .ToDictionary(pt => pt.DisplayMember, pt => pt.DisplayValue);
        }
        public void AddOdemeYontemi(string odemeYontemi)
        {
            string displayMember = odemeYontemi;
            string displayValue = odemeYontemi.Replace(" ", "") + "Odeme";
            // Yeni PaymentType entity'si oluşturuluyor.
            PaymentType yeniOdeme = new PaymentType
            {
                DisplayMember = displayMember,
                DisplayValue = displayValue
            };

            // Entity EF context'ine ekleniyor.
            _context.PaymentTypes.Add(yeniOdeme);

            // Değişiklikler veritabanına kaydediliyor.
            _context.SaveChanges();
        }
    }
    /*public class OdemeRepository
    {
        private string connectionString = "Server=.;Database=defineX_Payment;Trusted_Connection=True;TrustServerCertificate=True;";
        public Dictionary<string, string> GetOdemeYontemleri()
        {
            var odemeYontemleri = new Dictionary<string, string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM PaymentTypes";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            odemeYontemleri.Add(reader["Display_member"].ToString(), reader["Display_value"].ToString());
                        }
                    }
                }
            }

            return odemeYontemleri;
        }
    }*/
}
