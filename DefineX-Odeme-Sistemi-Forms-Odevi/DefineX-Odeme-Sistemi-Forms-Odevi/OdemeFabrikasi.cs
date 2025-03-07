using DefineX_Odeme_Sistemi_Forms_Odevi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DefineX_Odeme_Sistemi_Forms_Odevi
{
    public class OdemeFabrikasi
    {
        public static Type DinamikTypeOlustur(string className, string paymentType)
        {
            // Dinamik assembly ve modül oluşturuluyor.
            AssemblyName assemblyName = new AssemblyName("DynamicPaymentMethods");
            AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("MainModule");

            TypeBuilder typeBuilder = moduleBuilder.DefineType(className,
                TypeAttributes.Public | TypeAttributes.Class,
                null,
                new Type[] { typeof(IOdeme) });

            typeBuilder.DefineDefaultConstructor(MethodAttributes.Public);

            PropertyBuilder propertyBuilder = typeBuilder.DefineProperty("OdemeYontemi",
                PropertyAttributes.None,
                typeof(string),
                null);

            MethodBuilder getPropertyMethodBuilder = typeBuilder.DefineMethod("get_OdemeYontemi",
                MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig | MethodAttributes.Virtual,
                typeof(string),
                Type.EmptyTypes);

            ILGenerator ilGet = getPropertyMethodBuilder.GetILGenerator();

            ilGet.Emit(OpCodes.Ldstr, paymentType);
            ilGet.Emit(OpCodes.Ret);

            propertyBuilder.SetGetMethod(getPropertyMethodBuilder);

            MethodInfo interfaceGetMethod = typeof(IOdeme).GetMethod("get_OdemeYontemi");
            typeBuilder.DefineMethodOverride(getPropertyMethodBuilder, interfaceGetMethod);

            MethodBuilder methodBuilder = typeBuilder.DefineMethod("OdemeYap",
                MethodAttributes.Public | MethodAttributes.Virtual,
                typeof(string),
                new Type[] { typeof(decimal) });

            ILGenerator il = methodBuilder.GetILGenerator();
            il.Emit(OpCodes.Ldstr, "{0} ile {1} TL ödeme yapıldı.");
            il.Emit(OpCodes.Ldstr, paymentType);

            il.Emit(OpCodes.Ldarg_1);

            il.Emit(OpCodes.Box, typeof(decimal));

            MethodInfo formatMethod = typeof(string).GetMethod("Format", new Type[] { typeof(string), typeof(object), typeof(object) });
            il.Emit(OpCodes.Call, formatMethod);
            il.Emit(OpCodes.Ret);

            MethodInfo interfaceMethod = typeof(IOdeme).GetMethod("OdemeYap");
            typeBuilder.DefineMethodOverride(methodBuilder, interfaceMethod);

            return typeBuilder.CreateType();
        }
        public static void YeniTypeDosyala(string className, string odemeTipStr)
        {
            try
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string projectRoot = Path.GetFullPath(Path.Combine(baseDir, "..", "..", ".."));

                string modelsFolderPath = Path.Combine(projectRoot, "Models");

                if (!Directory.Exists(modelsFolderPath))
                {
                    Directory.CreateDirectory(modelsFolderPath);
                }

                string fileName = className + ".cs";
                string fullPath = Path.Combine(modelsFolderPath, fileName);

                if (File.Exists(fullPath))
                    return;

                string code = $@"using DefineX_Odeme_Sistemi_Forms_Odevi.Interfaces;

namespace DefineX_Odeme_Sistemi_Forms_Odevi.Models
{{
    public class {className} : IOdeme
    {{
        public string OdemeYontemi => ""{odemeTipStr}"";

        public string OdemeYap(decimal tutar)
        {{
            return $""{className} ile {{tutar}} TL ödeme yapıldı."";
        }}
    }}
}}";

                File.WriteAllText(fullPath, code);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Dinamik tip dosyası kaydedilirken hata oluştu: " + ex.Message);
            }
        }
        public static Dictionary<string, IOdeme> OdemeYontemleriniGetir(Dictionary<string, string> odemeTypes)
        {
            var odemeYontemleri = new Dictionary<string, IOdeme>();

            var existingTypes = Assembly.GetExecutingAssembly().GetTypes()
               .Where(t => t.IsClass && !t.IsAbstract && typeof(IOdeme).IsAssignableFrom(t))
               .ToDictionary(t => t.Name, StringComparer.OrdinalIgnoreCase);

            foreach (var (odemeTypeName, odemeTypeClassName) in odemeTypes)
            {
                IOdeme instance = null;
                if (existingTypes.ContainsKey(odemeTypeClassName))
                {
                    // Eğer tip mevcutsa, reflection ile instance oluştur.
                    instance = Activator.CreateInstance(existingTypes[odemeTypeClassName]) as IOdeme;
                }
                else
                {
                    // Eğer mevcut değilse, dinamik olarak tip oluştur ve kaydet.
                    Type dynamicType = DinamikTypeOlustur(odemeTypeClassName, odemeTypeName);
                    YeniTypeDosyala(odemeTypeClassName, odemeTypeName);

                    // Dinamik olarak oluşturulan tipin derlenip assembly'ye yükle.
                    instance = Activator.CreateInstance(dynamicType) as IOdeme;
                }

                if (instance != null)
                    odemeYontemleri.Add(odemeTypeName, instance);
            }

            return odemeYontemleri;
        }
    }
}
