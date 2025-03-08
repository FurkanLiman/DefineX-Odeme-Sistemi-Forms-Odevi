using DefineX_Odeme_Sistemi_Forms_Odevi.Interfaces;

namespace DefineX_Odeme_Sistemi_Forms_Odevi.Models
{
    public class NakitOdeme : IOdeme
    {
        public string OdemeYontemi => "Nakit";

        public string OdemeYap(decimal tutar)
        {
            return $"NakitOdeme ile {tutar} TL ödeme yapıldı.";
        }
    }
}