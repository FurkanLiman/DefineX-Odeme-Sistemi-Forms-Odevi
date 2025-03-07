using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefineX_Odeme_Sistemi_Forms_Odevi.DataAccess
{
    public class OdemeRepository
    {
        private string connectionString = "Server=.;Database=defineX_Payment;Trusted_Connection=True;TrustServerCertificate=True;";
        public Dictionary<string,string> GetOdemeYontemleri()
        {
            var odemeYontemleri = new Dictionary<string, string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Tablo adı: OdemeYontemleri, Kolon adı: OdemeYontemi
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
    }
}
