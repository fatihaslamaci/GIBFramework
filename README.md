# GIBFramework

WARNING: Henüz erken geliştirme aşamasındadır. 

Çalıştığım firmada e fatura ve e arşiv entegrasyonu için azımsanmayacak kadar mesai harcadım.  
Her entegrator firma için farklı kodlama yapmak gerekiyor ve bu işlem oldukça iş gücü kaybına sebep olmakta. 
Aynı iş gücü kaybının diğer yazılım firmalarının da harcadığı göz önüne alındığında, 
Ülke ekonomisi için büyük bir kayıp olduğunu tahmin etmek hiçte zor değil. 
Bu sorunu aşmak için tüm firmaların özgürce kullanabileceği, açık kaynak kodlu hobi amaçlı bir proje başlatma kararı aldım. 
Proje, standart bir veri girişi ve çıkışı kabul edecek. 
Gelen verileri hedef entegrator ün talep ettiği verilere çevirecek. 
Entegratorden gelen veriler ise standart bir yapıya dönüştürülecek. 
Yani yazılım firmaları her entegrator için ayrı ayrı kodlama yapmak yerine sadece arayüze veri gönderip almaları yeterli olacak. 

Eğer entegratör firmalar kendi servisleri için Provider yazarlarsa zahmetsizce entegrasyon sağlanmış olacak bu sayede yazılım hatalarından dolayı entegratör firmanın verdiği destek sayılarıda azalacak. (Şuan için belli başlı birkaç entegratör için Provider ları ben kodlayacağım)

Proje C# ile yazılmakta. başarılı olması durumunda Java içinde kodlamayı düşünüyorum.


# Kullanım örneği

```
using GIBInterface;
using System;

namespace SampleDotNetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            //Not: Örneğin çalışabilmesi için referanslara "System.Data.SQLite" eklenmesi gerekmekte

            IEFatura Entegrator;
            

            //Test için Sahte entgeratör seçiyoruz
            Entegrator = new SahteEntegrator.EFatura();

            //Gerekirse Aşağıdaki gibi Entegratörlerden birinide seçebiliriz
            //Uyumsof
            //Entegrator = new GIBProviders.Uyumsoft.EFatura();
            //Veriban
            //Entegrator = new GIBProviders.Veriban.EFatura();
            //Logo
            //Entegrator = new GIBProviders.Logo.EFatura();

            //Seçilen Entegratör ile GIB Framework oluşturulur
            var EFatura = new GIBFramework.EFatura(Entegrator);

            //Entegratöre özel varsayılan ayarlar var ise consola yazıyoruz (Username, Pasword, Servis adresi gibi)
            Console.WriteLine("Entegratöre özel varsayılan ayarlar");
            Console.WriteLine(EFatura.DefaultSettingsJson());

            //Entegratore özel Ayarları Json formatında giriyoruz
            EFatura.SettingsJson = "{\"UserName\": \"İstanbul\", \"Password\": \"1453\"}";


            //VKN Sorgulama örneği
            var Mukellef = EFatura.MukellefBilgisi("2970610282");
            Console.WriteLine(Mukellef.Title);


            //Fatura Gönderim örneği.
            //EFatura.SendInvoice(new SendParameters());

            //Fatura Durum Sorgulama örneği
            //EFatura.FaturaDurumSorgula(new List<QueryStatusParameters>());

            Console.ReadKey();
        }
    }
}
```

Projeyi destekliyorsanız lütfen Yıldız vermeyi unutmayınız.
