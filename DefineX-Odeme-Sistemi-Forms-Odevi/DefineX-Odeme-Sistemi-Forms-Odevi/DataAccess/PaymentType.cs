//using DefineX_Odeme_Sistemi_Forms_Odevi.Attributes;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace DefineX_Odeme_Sistemi_Forms_Odevi.DataAccess
{
    [Tablo(TabloAdi = "PaymentTypes")]
    public class PaymentType
    {

        [Alan(AlanAdi = "Id", Identity = true, NullIcerebilir = false)]
        public int Id { get; set; }

        [Alan("Display_member", false, false)]
        public string DisplayMember { get; set; }


        [Alan("Display_value", Identity = false, NullIcerebilir = false)]
        public string DisplayValue { get; set; }

        public PaymentType(string gorunenIsim, string odemeTuru)
        {
            DisplayMember = gorunenIsim;
            DisplayValue = odemeTuru;
        }
        public PaymentType()
        {
        }
        public int GetOdemeYontemleri()
        {
            return Id;
        }
    }
}