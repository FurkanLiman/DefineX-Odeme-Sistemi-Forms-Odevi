﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DefineX_Odeme_Sistemi_Forms_Odevi.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class ZorunluAlanAttribute : Attribute
    {

        public ZorunluAlanAttribute()
        {
        }

        public static bool Dogrula(object? dogrulanacakEntity)
        {
            if (dogrulanacakEntity == null)
            {
                return false;
            }
            Type dogrulamacakTur = dogrulanacakEntity.GetType();
            FieldInfo[] dogrulanacakTurAlanlari = dogrulamacakTur.GetFields(
                                                  BindingFlags.Public |
                                                  BindingFlags.Instance);
            foreach (FieldInfo dogrulanacakTurAlani in dogrulanacakTurAlanlari)
            {
                object[] zorunluAlanOznitelikleri = dogrulanacakTurAlani.GetCustomAttributes(typeof(ZorunluAlanAttribute), true);
                if ( zorunluAlanOznitelikleri.Length != 0)
                {
                    string alanDegeri = dogrulanacakTurAlani.GetValue(dogrulanacakEntity) as string;
                    if (string.IsNullOrEmpty(alanDegeri))
                    {
                        return false;
                    }
                }

            }


            var properties = dogrulamacakTur.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in properties)
            {
                object[] zorunluAlanOznitelikleri = prop.GetCustomAttributes(typeof(ZorunluAlanAttribute), true);
                if (zorunluAlanOznitelikleri.Length != 0)
                {
                    string alanDegeri = prop.GetValue(dogrulanacakEntity) as string;
                    if (string.IsNullOrEmpty(alanDegeri))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
