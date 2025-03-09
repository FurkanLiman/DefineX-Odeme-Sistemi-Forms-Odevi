using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


[AttributeUsage(AttributeTargets.Property)]
public class AlanAttribute : Attribute
{

    public bool NullIcerebilir { get; set; }
    public string AlanAdi { get; set; }
    public bool Identity { get; set; }

    public AlanAttribute(string alaninAdi, bool identityMi, bool nullIcerirmi)
    {
        AlanAdi = alaninAdi;
        Identity = identityMi;
        NullIcerebilir = nullIcerirmi;
    }
    public AlanAttribute(string alaninAdi, bool identityMi)
        : this(alaninAdi, identityMi, true)
    {
    }
    public AlanAttribute(string alaninAdi)
        : this(alaninAdi, false)
    {
    }
    public AlanAttribute()
    {

    }

}

