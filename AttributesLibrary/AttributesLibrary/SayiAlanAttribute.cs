using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


[AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
public class SayiAlanAttribute : Attribute
{
    public string HataMesaji { get; set; }

    public SayiAlanAttribute(string hataMesaji = "Bu alan sadece sayı içermelidir.")
    {
        HataMesaji = hataMesaji;
    }

    public static bool Dogrula(object dogrulanacakEntity)
    {
        var propertyInfo = dogrulanacakEntity.GetType().GetProperty("Text");
        if (propertyInfo != null)
        {
            var value = propertyInfo.GetValue(dogrulanacakEntity)?.ToString();
            return decimal.TryParse(value, out _);
        }
        bool isValid = true; 
        Type dogrulamacakTur = dogrulanacakEntity.GetType();
        FieldInfo[] dogrulanacakTurAlanlari = dogrulamacakTur.GetFields(
                                                BindingFlags.Public |
                                                BindingFlags.Instance);
        foreach (FieldInfo dogrulanacakTurAlani in dogrulanacakTurAlanlari)
        {
            object[] oz = dogrulanacakTurAlani.GetCustomAttributes(typeof(SayiAlanAttribute), true);
            if (oz.Length > 0)
            {
                string deger = dogrulanacakTurAlani.GetValue(dogrulanacakEntity) as string;
                if (!decimal.TryParse(deger, out _))
                {
                    isValid = false;
                }
            }
        }
        return isValid;
    }
}
