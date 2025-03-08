# DefineX-Odeme-Sistemi-Forms-Odevi 

DefineX Odeme Sistemi, Windows Forms tabanlı ödeme işlemlerini yöneten, SOLID prensipleri ve dinamik tip oluşturma (Reflection.Emit) üzerine eğitim amaçlayan bir uygulamadır. Uygulama, SQL veritabanından ödeme yöntemlerini okur, mevcut sistemle senkronize eder ve eğer veritabanında yeni bir ödeme yöntemi varsa, çalışma zamanında dinamik olarak tip oluşturur. Ayrıca oluşturulan dinamik tipin kaynak kodu, proje ana dizinindeki Models klasörüne (.cs dosyası olarak) kaydedilir.

---

## Özellikler

- **Windows Forms UI**
  - **ComboBox (cmbOdemeTipi):** Ödeme yöntemlerini listeler.
  - **TextBox (txtTutar):** Ödeme tutarını kullanıcıdan alır.
  - **Button (btnGonder):** Ödeme işlemini tetikler.
  - **Label (lblSonuc):** İşlem sonucunu görüntüler.

- **Veritabanı Entegrasyonu**
  - SQL Server veritabanındaki `PaymentTypes` tablosundan ödeme yöntemleri çekilir.
  - `PaymentTypes` tablosu, her ödeme yöntemi için `Display_member` (öğenin gösterilen adı) ve `Display_value` (dinamik olarak oluşturulacak tipin sınıf adı) bilgilerini içerir.
  - Entity Framework Core (EF Core) kullanılarak veritabanı işlemleri gerçekleştirilir.
  - WinForms uygulamasında, App.config üzerinden merkezi olarak yönetilen connection string kullanılır.

- **Dinamik Tip Oluşturma**
  - **OdemeFabrikasi** sınıfı, veritabanından gelen ödeme yöntemleri ile sistemdeki mevcut implementasyonları karşılaştırır.
  - Eksik ödeme yöntemleri için, Reflection.Emit kullanarak dinamik tip oluşturur ve bu tipin örneğini sistem sözlüğüne ekler.
  - Oluşturulan dinamik tipin kaynak kodu, proje ana dizinindeki `Models` klasörüne .cs dosyası olarak kaydedilir.

- **Senkronizasyon**
  - Uygulama başlatıldığında (Form1_Load) ve ComboBox açıldığında (DropDown event) veritabanı ile senkronizasyon sağlanır.
  - Uygulama çalışırken, veritabanına yeni ödeme yöntemi eklenirse dinamik olarak senkronize edilir.

- **Custom Attribute ile Validasyon**
  - **ZorunluAlanAttribute:** Formdan alınan verilerde, boş (null, boş string veya sadece boşluk) değerleri kontrol eder.
  - **SayiAlanAttribute:** Form verilerinde, girilen değerin sayı formatında olup olmadığını kontrol eder.
  - Bu attribute'ler, model (ör. `OdemeModel`) üzerinde uygulanır ve form gönderiminde custom validasyon metotları aracılığıyla kontrol edilir.

---
## Class Diagram

![ClassDiagram](https://github.com/user-attachments/assets/77017c03-ab7d-46a7-a40c-b6e422128656)

## Proje Yapısı
```
DefineX_Payment_System/ 
├── Form1.cs // Ana form: UI işlemleri ve senkronizasyon
├── DataAccess/ 
│ └── OdemeRepository.cs // Veritabanı erişimi; PaymentTypes tablosundan veri çekme 
├── Interfaces/ 
│ └── IOdeme.cs // Tüm ödeme yöntemlerinin uygulaması gereken arayüz
├── Attributes/
│ ├── ZorunluAlanAttribute.cs // Boş (null/boş string) kontrolü için custom attribute
│ └── SayiAlanAttribute.cs // Sayı formatı kontrolü için custom attribute
├── Models/ 
│ ├── KrediKartiOdeme.cs // Örnek olarak önceden tanımlı ödeme yöntemi 
│ └── (Dinamik oluşturulan ödeme yöntemi .cs dosyaları) 
└── Business/ 
└── OdemeFabrikasi.cs // Dinamik tip oluşturma, senkronizasyon ve dosya kaydetme işlemleri
```

### Ana Bileşenler

- **Form1.cs (UI)**
  - **Form1_Load:** Uygulama başlatıldığında, `OdemeRepository` aracılığıyla veritabanından ödeme yöntemleri çekilir; `OdemeFabrikasi.OdemeYontemleriniGetir` metodu kullanılarak sistemde olmayanlar için dinamik tip oluşturulur ve ComboBox güncellenir.
  - **btnGonder_Click:** Seçilen ödeme yöntemi ve girilen tutar kullanılarak ödeme işlemi gerçekleştirilir.
  - **cmbOdemeTipi_DropDown:** Kullanıcı ComboBox’ı açtığında tekrar veritabanı senkronizasyonu sağlanır.

- **OdemeFabrikasi.cs (Business)**
  - **DinamikTypeOlustur:** Belirtilen ödeme yöntemi bilgilerine göre, Reflection.Emit kullanarak dinamik tip oluşturur.
  - **YeniTypeDosyala:** Oluşturulan dinamik tipin kaynak kodunu (.cs dosyası) proje ana dizinindeki Models klasörüne kaydeder.
  - **OdemeYontemleriniGetir:** Veritabanından çekilen ödeme yöntemleriyle, mevcut sistemdeki tipleri karşılaştırır; eksik olanlar dinamik olarak oluşturulur ve sonuç olarak bir sözlük (Dictionary) döner.

- **OdemeRepository.cs (DataAccess)**
  - SQL veritabanı bağlantısı ile `PaymentTypes` tablosundan ödeme yöntemlerini çekerek, her yöntemi `Display_member` (öğenin görünen adı) ve `Display_value` (sınıf adı) olarak bir Dictionary içerisinde döner.

- **IOdeme.cs (Interfaces)**
  ```csharp
  public interface IOdeme
  {
      string OdemeYontemi { get; }
      string OdemeYap(decimal tutar);
  }
