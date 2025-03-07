using DefineX_Odeme_Sistemi_Forms_Odevi.Interfaces;

namespace DefineX_Odeme_Sistemi_Forms_Odevi.Models
{
    public class KrediKartiOdeme : IOdeme
    {
        public string OdemeYontemi => "Kredi Kartı";

        public string OdemeYap(decimal tutar)
        {
            return $"KrediKartiOdeme ile {tutar} TL ödeme yapıldı.";
        }
    }
}