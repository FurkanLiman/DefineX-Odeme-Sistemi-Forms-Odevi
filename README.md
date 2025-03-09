# DefineX-Odeme-Sistemi-Forms-Odevi 

DefineX Odeme Sistemi, Windows Forms tabanlı ödeme işlemlerini yöneten, SOLID prensipleri, dinamik tip oluşturma (Reflection.Emit) ve custom attribute'lerle form validasyonu üzerine eğitim amaçlayan bir uygulamadır. Uygulama, SQL veritabanından ödeme yöntemlerini okur, mevcut sistemle senkronize eder ve eğer veritabanında yeni bir ödeme yöntemi varsa, çalışma zamanında dinamik olarak tip oluşturur. Ayrıca oluşturulan dinamik tipin kaynak kodu, proje ana dizinindeki `Models` klasörüne (.cs dosyası olarak) kaydedilir.

---

## Özellikler

- **Windows Forms UI**
  - **ComboBox (cmbOdemeTipi):** SQL veritabanından çekilen ödeme yöntemlerini listeler.
  - **TextBox (txtTutar):** Ödeme tutarını kullanıcıdan alır.
  - **Button (btnGonder):** Ödeme işlemini tetikler.
  - **Button (btnKaydet):** Yeni ödeme yöntemi ekleme işlemini başlatır.
  - **Label (lblSonuc):** İşlem sonucunu görüntüler.

- **Veritabanı Entegrasyonu**
  - SQL Server veritabanındaki `PaymentTypes` tablosundan ödeme yöntemleri çekilir.
  - `PaymentTypes` tablosu, her ödeme yöntemi için `Display_member` (öğenin gösterilen adı) ve `Display_value` (dinamik olarak oluşturulacak tipin sınıf adı) bilgilerini içerir.
  - Veritabanı işlemleri, **ADO.NET** veya **EF Core** gibi teknolojilerle gerçekleştirilebilir (bu projede ADO.NET doğrudan kullanılmıştır).
  - WinForms uygulamasında, `App.config` veya `appSettings.json` üzerinden merkezi olarak yönetilen connection string kullanılır.

- **Dinamik Tip Oluşturma**
  - **OdemeFabrikasi** sınıfı, veritabanından gelen ödeme yöntemleri ile mevcut sistemdeki implementasyonları karşılaştırır.
  - Eksik ödeme yöntemleri için, **Reflection.Emit** kullanarak dinamik tip oluşturur ve bu tipin örneğini sistem sözlüğüne ekler.
  - Oluşturulan dinamik tipin kaynak kodu, proje ana dizinindeki `Models` klasörüne `.cs` dosyası olarak kaydedilir.

- **Senkronizasyon**
  - Uygulama başlatıldığında (Form açılırken) ve ComboBox açıldığında (DropDown event) veritabanı ile senkronizasyon sağlanır.
  - Uygulama çalışırken, veritabanına yeni ödeme yöntemi eklenirse, tekrar senkronize edildiğinde dinamik olarak tip oluşturulur.

- **Custom Attribute ile Validasyon**
  - **ZorunluAlanAttribute:** Formdan alınan verilerde, boş (null, boş string veya sadece boşluk) değerleri kontrol eder.
  - **SayiAlanAttribute:** Form verilerinde, girilen değerin sayı formatında olup olmadığını kontrol eder.
  - Bu attribute'ler, doğrudan `Form` üzerindeki kontrollerde veya model sınıfları üzerinde uygulanabilir ve form gönderiminde custom validasyon metotları aracılığıyla kontrol edilir.

---

## Class Diagram

![ClassDiagram](https://github.com/user-attachments/assets/2ed4b1ea-079c-4b4f-9577-1a6a56e5a884)

## Proje Yapısı
```
DefineX-Odeme-Sistemi-Forms-Odevi/
├── AttributesLibrary/ 
│   ├── AlanAttribute.cs
│   ├── SayiAlanAttribute.cs
│   ├── TabloAttribute.cs
│   └── ZorunluAlanAttribute.cs
│   // Bu proje bir Class Library olarak oluşturulmuştur.
│   // Veritabanı ve alan bilgileriyle ilgili custom attribute'leri barındırır.
|
├── DefineX-Odeme-Sistemi-Forms-Odevi/
│   ├── DataAccess/
│   |   ├── PaymentType.cs   // Veritabanı ile ilişkili model sınıfı
│   │   └── OdemeRepository.cs // PaymentTypes tablosuna erişim, ekleme, listeleme
│   │
│   ├── Interfaces/
│   │   └── IOdeme.cs // Tüm ödeme yöntemlerinin uygulaması gereken arayüz
│   │
│   ├── Models/
│   │   ├── KrediKartiOdeme.cs
│   │   ├── NakitOdeme.cs
│   │   └── (Dinamik oluşturulan ödeme yöntemi .cs dosyaları)
│   │
│   ├── Form1.cs         // Ana Form; UI işlemleri ve validasyon kontrolleri
│   ├── OdemeFabrikasi.cs // Dinamik tip oluşturma ve senkronizasyon
│   ├── PaymentType.cs   // Veritabanı ile ilişkili model sınıfı
│   ├── Program.cs       // Uygulama giriş noktası
│   └── ... diğer proje dosyaları (csproj vb.)
│
├── .gitignore
└── README.md

```

## Ana Bileşenler

### Form1.cs (UI)
- **Ana form**: UI işlemleri ve validasyon kontrollerinin yapıldığı yerdir.
- **btnGonder_Click:**  
  - Kullanıcının girdiği tutarı ve seçtiği ödeme tipini `ZorunluAlanAttribute` ve `SayiAlanAttribute` ile kontrol eder.  
  - Geçerli ise `OdemeYap` metodunu çağırır ve sonucu `lblSonuc` üzerinde gösterir.
- **cmbOdemeTipi_DropDown:**  
  - Kullanıcı ComboBox’ı açtığında, veritabanı ile tekrar senkronizasyon sağlanır.  
  - Yeni eklenen ödeme yöntemleri varsa listeye dahil edilir.
- **btnKaydet_Click:**  
  - Yeni bir ödeme yöntemi eklemek için gerekli alanların (ör. ödeme yöntemi adı) `ZorunluAlanAttribute` ile boş olup olmadığı kontrol edilir.  
  - `OdemeRepository.AddOdemeYontemi` metodu çağrılarak veritabanına INSERT işlemi yapılır.

---

### OdemeFabrikasi.cs (Business)
- **DinamikTypeOlustur:**  
  - Veritabanından veya kullanıcıdan gelen ödeme yöntemi bilgileri (sınıf adı, görüntülenen ad vb.) temel alınarak **Reflection.Emit** ile çalışma zamanında `IOdeme` arayüzünü uygulayan yeni bir C# sınıfı oluşturur.
- **YeniTypeDosyala:**  
  - Oluşturulan dinamik tipin kaynak kodunu (`.cs` dosyası) `Models` klasörüne kaydeder.  
  - Eğer aynı isimde bir dosya varsa, yeniden oluşturmadan geçer.
- **OdemeYontemleriniGetir:**  
  - Mevcut (veya dinamik) ödeme tiplerini sözlük (Dictionary) olarak döndürür.  
  - Bu sözlük, **ComboBox** gibi UI bileşenlerinin veri kaynağı olarak kullanılır.  
  - Mevcut tipler `Assembly.GetExecutingAssembly()` üzerinden reflection ile bulunur, eksik tipler ise `DinamikTypeOlustur` aracılığıyla eklenir.

---

### OdemeRepository.cs (DataAccess)
- **GetOdemeYontemleri:**  
  - `PaymentTypes` tablosundan (`Id`, `Display_member`, `Display_value`) alanlarını okuyarak, her bir satır için bir `PaymentType` nesnesi oluşturur.  
  - Bu liste, `OdemeFabrikasi` gibi sınıflara iletilerek dinamik tip oluşturma sürecinde kullanılır.
- **AddOdemeYontemi:**  
  - Yeni bir `PaymentType` eklemek için **INSERT** sorgusunu `AlanAttribute` bilgilerini kullanarak oluşturur.  
  - `Id` sütunu identity olduğundan ekleme esnasında dahil edilmez.  
  - Başarılı olursa etkilenen satır sayısını (ör. 1) döndürür, hata durumunda -1 döndürür.
- **[Tablo(TabloAdi = "PaymentTypes")]**  
  - Bu sınıfın, `PaymentTypes` tablosuna karşılık geldiğini belirtir.

---

### PaymentType.cs (Model)
- **[Tablo("PaymentTypes")]**  
  - Hangi tabloya karşılık geldiği bilgisini tutan custom attribute.
- **[Alan(…)]**  
  - `Id`, `DisplayMember`, `DisplayValue` gibi özelliklerin hangi veritabanı sütununa denk geldiğini tanımlar.  
  - `Identity = true` ayarıyla `Id` sütununun otomatik arttırılan birincil anahtar olduğu belirtilir.
- **Kullanım:**  
  - `OdemeRepository` içinde `PaymentType` nesneleriyle SQL sorguları dinamik olarak oluşturulur.  
  - Kullanıcı yeni bir yöntem eklediğinde, bu model üzerinden veritabanına kayıt atılır.

---

### IOdeme.cs (Interfaces)
```csharp
public interface IOdeme
{
    string OdemeYontemi { get; }
    string OdemeYap(decimal tutar);
}
```
- Tüm ödeme yöntemlerinin uygulaması gereken temel arayüzdür.
- `OdemeYontemi` property'si, ödeme yönteminin adını döner.
- `OdemeYap(decimal tutar)` metodu, verilen tutar ile ödeme işlemini yapar ve işlem sonucunu mesaj olarak döner.

### AttributesLibrary Projesi

- `AlanAttribute.cs`: Bir property’nin veritabanında hangi sütuna denk geldiğini, identity olup olmadığını ve null değer alıp alamayacağını belirtir.
- `TabloAttribute.cs`: Bir sınıfın veritabanında hangi tablo ve şemaya ait olduğunu belirtir.
- `ZorunluAlanAttribute.cs`: Bir alanın null veya boş değer içeremeyeceğini kontrol eder.
- `SayiAlanAttribute.cs`: Bir alanın sayı formatında olmasını sağlar.

### Models Klasörü

- `KrediKartiOdeme.cs`: IOdeme interface’ini uygular. Kredi kartı ile ödeme yapılmasını sağlar.
- `NakitOdeme.cs`: IOdeme interface’ini uygular. Nakit ödeme yapılmasını sağlar.
- **Dinamik oluşturulan ödeme yöntemi .cs dosyaları**: Çalışma zamanında veritabanına yeni eklenen ödeme yöntemleri için otomatik olarak oluşturulur.

---
