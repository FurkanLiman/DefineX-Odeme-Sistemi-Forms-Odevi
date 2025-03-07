using DefineX_Odeme_Sistemi_Forms_Odevi.Interfaces;

namespace DefineX_Odeme_Sistemi_Forms_Odevi.Models
{
    public class EftOdeme : IOdeme
    {
        public string OdemeYontemi => "EFT";

        public string OdemeYap(decimal tutar)
        {
            return $"EftOdeme ile {tutar} TL ödeme yapıldı.";
        }
    }
}