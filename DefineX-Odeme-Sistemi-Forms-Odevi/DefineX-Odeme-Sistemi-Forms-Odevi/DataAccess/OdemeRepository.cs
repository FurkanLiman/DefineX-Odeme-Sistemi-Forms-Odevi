using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DefineX_Odeme_Sistemi_Forms_Odevi.DataAccess
{
    [Tablo(TabloAdi = "PaymentTypes")]
    public class OdemeRepository
    {
        public readonly List<PaymentType> _context;
       
        public OdemeRepository(List<PaymentType> context)
        {
            _context = context;
        }
        public OdemeRepository() : this(CreateContextFromConfiguration())
        {
        }

        private static List<PaymentType> CreateContextFromConfiguration()
        {
            return new List<PaymentType>();
        }

        public List<PaymentType> GetOdemeYontemleri()
        {
            List<PaymentType> odemeYontemleri = new List<PaymentType>();
            PaymentType result = null;
            Type tip = typeof(PaymentType);

            TabloAttribute tblAtr = ((TabloAttribute[])tip.GetCustomAttributes(typeof(TabloAttribute), false))[0];
            string tabloAdi = tblAtr.TabloAdi;
            string schemaAdi = tblAtr.SchemaAdi;

            string query = $"SELECT Id, Display_member, Display_value FROM {schemaAdi}.{tabloAdi}";
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["defineX_Payment"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PaymentType paymentType = new PaymentType();

                            paymentType.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                            paymentType.DisplayMember = reader.GetString(reader.GetOrdinal("Display_member"));
                            paymentType.DisplayValue = reader.GetString(reader.GetOrdinal("Display_value"));

                            odemeYontemleri.Add(paymentType);
                        }
                    }
                }
            }
            return odemeYontemleri;

        }
        public int AddOdemeYontemi(PaymentType paymentType)
        {
            Type tip = paymentType.GetType();
            TabloAttribute tblAtr = ((TabloAttribute[])tip.GetCustomAttributes(typeof(TabloAttribute), false))[0];
            string tabloAdi = tblAtr.TabloAdi;
            string schemaAdi = tblAtr.SchemaAdi;
            StringBuilder insertBuilder = new StringBuilder();
            insertBuilder.Append("Insert into ");
            insertBuilder.Append(schemaAdi);
            insertBuilder.Append(".");
            insertBuilder.Append(tabloAdi);
            insertBuilder.Append(" (");

            foreach (PropertyInfo prp in tip.GetProperties())
            {
                AlanAttribute atr = ((AlanAttribute[])prp.GetCustomAttributes(typeof(AlanAttribute), false))[0];
                if (!atr.Identity)
                {
                    string alanAdi = atr.AlanAdi;
                    insertBuilder.Append(alanAdi);
                    insertBuilder.Append(",");
                }
            }
            insertBuilder.Remove(insertBuilder.Length - 1, 1);
            insertBuilder.Append(") Values (");

            foreach (PropertyInfo prp in tip.GetProperties())
            {
                AlanAttribute atr = ((AlanAttribute[])prp.GetCustomAttributes(typeof(AlanAttribute), false))[0];
                if (!atr.Identity)
                {
                    object alanDegeri = prp.GetValue(paymentType, null);
                    if ((prp.PropertyType.Name == "String")
                            || (prp.PropertyType.Name == "DateTime"))
                        insertBuilder.Append("'" + prp.GetValue(paymentType, null).ToString() + "',");
                    else
                        insertBuilder.Append(prp.GetValue(paymentType, null).ToString() + ",");
                }
            }
            insertBuilder.Remove(insertBuilder.Length - 1, 1);
            insertBuilder.Append(")");
            try
            {
                string query = insertBuilder.ToString();
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["defineX_Payment"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected;
                    }
                }

            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
