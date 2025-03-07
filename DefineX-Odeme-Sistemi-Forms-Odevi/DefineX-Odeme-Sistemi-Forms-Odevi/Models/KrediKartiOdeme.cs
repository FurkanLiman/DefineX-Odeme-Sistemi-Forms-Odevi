using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DefineX_Odeme_Sistemi_Forms_Odevi.Interfaces;

namespace DefineX_Odeme_Sistemi_Forms_Odevi.Models
{
    internal class KrediKartiOdeme : IOdeme
    {
        public string OdemeYontemi => "Kredi Karti";
        public string OdemeYap(decimal tutar)
        {
            return $"Kredi Kartı ile {tutar} TL ödeme yapıldı.";
        }
    }
}
