using DefineX_Odeme_Sistemi_Forms_Odevi.Interfaces;

namespace DefineX_Odeme_Sistemi_Forms_Odevi.Models
{
    public class HavaleOdeme : IOdeme
    {
        public string OdemeYontemi => "Havale";

        public string OdemeYap(decimal tutar)
        {
            return $"HavaleOdeme ile {tutar} TL ödeme yapıldı.";
        }
    }
}