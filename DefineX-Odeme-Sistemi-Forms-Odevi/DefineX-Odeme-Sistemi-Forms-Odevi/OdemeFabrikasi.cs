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
        
        public static void YeniTypeDosyala(string className,string odemeTipStr)
        {
            try
            {
                // Çalışma dizininden proje kök dizinine ulaşmak için (örnek: 3 seviye yukarı)
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string projectRoot = Path.GetFullPath(Path.Combine(baseDir, "..", "..", ".."));

                // Proje kökü üzerinden PaymentSystem.Business/Models klasörüne ulaşmak
                string modelsFolderPath = Path.Combine(projectRoot, "Models");

                if (!Directory.Exists(modelsFolderPath))
                {
                    Directory.CreateDirectory(modelsFolderPath);
                }

                // Dosya adı, örn: SelamOdeme.cs
                string fileName = className + ".cs";
                string fullPath = Path.Combine(modelsFolderPath, fileName);

                // Dosya zaten varsa, üzerine yazmak istemiyorsak çıkabiliriz.
                if (File.Exists(fullPath))
                    return;

                // Dinamik tip için kaynak kodu şablonu.
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
        public static Dictionary<string,IOdeme> OdemeYontemleriniGetir(Dictionary<string,string> odemeTypes)
        {
            var odemeYontemleri = new Dictionary<string,IOdeme>();

            var existingTypes = Assembly.GetExecutingAssembly().GetTypes()
               .Where(t => t.IsClass && !t.IsAbstract && typeof(IOdeme).IsAssignableFrom(t))
               .ToDictionary(t => t.Name, StringComparer.OrdinalIgnoreCase);

            foreach (var (odemeTypeName,odemeTypeClassName) in odemeTypes)
            {
                IOdeme instance = null;
                if (existingTypes.ContainsKey(odemeTypeClassName))
                {
                    // Eğer tip mevcutsa, reflection ile instance oluştur.
                    instance = Activator.CreateInstance(existingTypes[odemeTypeClassName]) as IOdeme;
                }
                else
                {
                    
                    YeniTypeDosyala(odemeTypeClassName,odemeTypeName);

                    // Dinamik olarak oluşturulan tipin derlenip assembly'ye yüklendiğinden emin olmalısınız.
                    instance = Activator.CreateInstance(dynamicType) as IOdeme;
                }

                if (instance != null)
                    odemeYontemleri.Add(odemeTypeName,instance);
            }

            return odemeYontemleri;
            /*
            //var odemeYontemleri = new List<IOdeme>();


            //var codeOdemeTypes = Assembly.GetExecutingAssembly().GetTypes()
            //    .Where(t => t.IsClass && !t.IsAbstract && typeof(IOdeme).IsAssignableFrom(t))
            //    .ToList();

            //var varolanTypes = codeOdemeTypes
            //   .Where(t => odemeTypes.Any(dbType => string.Equals(dbType, t.Name, StringComparison.OrdinalIgnoreCase)))
            //   .ToList();

            //foreach (var type in varolanTypes)
            //{
            //    IOdeme instance = Activator.CreateInstance(type) as IOdeme;
            //    odemeYontemleri.Add(instance);
            //}

            //var eksikTypes = odemeTypes.Except(codeOdemeTypes, StringComparer.OrdinalIgnoreCase).ToList();

            //foreach (var odemeType in eksikTypes)
            //{

            //    Type type = CreateLogType(odemeType, odemeType);
            //    SaveDynamicTypeToFile(odemeType);
            //    IOdeme a = Activator.CreateInstance(type) as IOdeme;
            //    odemeYontemleri.Add(a);
            //}
            //return odemeYontemleri;
            */
        }
    }
}
