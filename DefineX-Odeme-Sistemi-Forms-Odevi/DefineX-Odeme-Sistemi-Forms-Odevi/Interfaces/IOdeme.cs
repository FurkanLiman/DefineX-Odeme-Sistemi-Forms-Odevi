using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefineX_Odeme_Sistemi_Forms_Odevi.Interfaces
{
    public interface IOdeme
    {
        string OdemeYontemi { get; }
        string OdemeYap(decimal tutar);
    }
}
