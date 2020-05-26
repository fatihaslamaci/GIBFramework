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
            Entegrator = new GIBProviders.SahteEntegrator.EFatura();

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

            //Fatura Gönderim örneği.
            //EFatura.SendInvoice(new SendParameters());

            //Fatura Durum Sorgulama örneği
            //EFatura.FaturaDurumSorgula(new List<QueryStatusParameters>());

            Console.ReadKey();
        }
    }
}
