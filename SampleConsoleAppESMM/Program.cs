using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleConsoleAppESMM
{
    class Program
    {
        static void Main(string[] args)
        {
            GIBInterface.ESMM.IESMM Entegrator;


            //Serbest Meslek Makbuzu için Entgeratör seçiyoruz
            Entegrator = new Uyumsoft.ESMM.Sender();
            //TODO :Diğer Entegratörler eklenecek. Örnek
            //Entegrator = new Veriban.ESMM.Sender();


            //Seçilen Entegratör ile Serbest Meslek Makbuzu için GIB Framework oluşturulur
            var eSMM = new GIBFramework.ESMM.ESMM(Entegrator);

            //Entegratöre özel varsayılan ayarlar var ise consola yazıyoruz (Username, Pasword, Servis adresi gibi)
            Console.WriteLine("Entegratöre özel varsayılan ayarlar");
            Console.WriteLine(eSMM.DefaultSettingsJson());

            //Entegratore özel Ayarları Json formatında giriyoruz
            eSMM.SettingsJson = @"{ 
                    'UserName': 'Uyumsoft', 
                    'Password': 'Uyumsoft', 
                    'Uri': 'https://efatura-test.uyumsoft.com.tr/services/VoucherIntegration',
               }".Replace("'", "\"");

            //Serbest Meslek Makbuz Gönderim örneği.

            var OrnekMakbuz = OrnekMakbuzHazirla();

            //HTML Transform örneği
            XML_TO_HTML_TRANSFORM(OrnekMakbuz.sendParametersESMMitems[0].eArsivVeri);


            var response = eSMM.Send(OrnekMakbuz);


            if (response.Basarili)
            {
                Console.WriteLine(string.Format("Makbuz gönderildi. Makbuz No:{0}, ETTN: {1}", response.MakbuzResponseList[0].MakbuzNo, response.MakbuzResponseList[0].ETTN));
                OrnekMakbuz.sendParametersESMMitems[0].eArsivVeri.Item.makbuzNo = response.MakbuzResponseList[0].MakbuzNo;
                XML_TO_HTML_TRANSFORM(OrnekMakbuz.sendParametersESMMitems[0].eArsivVeri);
            }
            else
            {
                Console.WriteLine(response.Mesaj);
            }


            Console.ReadKey();
        }


        public static GIBInterface.EFaturaPaketi.eSMM.eArsivVeri XML_TO_eArsivVeri(string fileName)
        {
            var xml = File.ReadAllText(fileName, Encoding.UTF8);
            //xml deserialize işlemi
            return GIBInterface.EFaturaPaketi.eSMM.eArsivVeri.Create(xml);

        }

        public static void XML_TO_HTML_TRANSFORM(GIBInterface.EFaturaPaketi.eSMM.eArsivVeri eArsivVeri)
        {

            string TempFolder = "C:\\TempMakbuz";
            string TempFileName = TempFolder + "\\OrnekMakbuz.html";
            Directory.CreateDirectory(TempFolder);

            var xslt = File.ReadAllText("tasarim.xslt", Encoding.UTF8);

            // Xml serialize örneği
            var xml = eArsivVeri.CreateXml();

            var html = GIBFramework.InvoiceTransform.TransformXMLToHTML(xml, xslt);

            
            File.WriteAllText(TempFileName, html, Encoding.UTF8);

            //Sisteme Tanımlı varsayılan uygulama ile html dosyasını aç
            Process.Start(new ProcessStartInfo{FileName = TempFileName, UseShellExecute = true});
        }


        private static GIBInterface.ESMM.SendParametersESMM OrnekMakbuzHazirla()
        {
            var r = new GIBInterface.ESMM.SendParametersESMM();
            r.sendParametersESMMitems = new List<GIBInterface.ESMM.SendParametersESMMitem>();
            var makbuz = CreateVoucher();
            r.sendParametersESMMitems.Add(makbuz);
            return r;
        }

        public static GIBInterface.ESMM.SendParametersESMMitem CreateVoucher()
        {
            var r = new GIBInterface.ESMM.SendParametersESMMitem();


            r.Notes = new List<string>();
            r.Notes.Add("Ödeme süresi 7 gündür");
            r.Notes.Add("Yalnız:Dokuz Yüz On İki Türk Lirası");
            r.Notes.Add("Bu belgeyi hazırlamak için https://github.com/fatihaslamaci/GIBFramework kütüphanesi kullanılmıştır.");

            r.email = new List<string>();
            r.email.Add("fatihaslamaci@gmail.com");

            //xml deserialize yaparak veri oluşturuldu.
            r.eArsivVeri = XML_TO_eArsivVeri("SerbestMeslekMakbuzu.xml");

            r.eArsivVeri.Item.ETTN = Guid.NewGuid().ToString();
            r.eArsivVeri.Item.belgeTarihi = DateTime.Now.Date;
            r.eArsivVeri.Item.belgeZamani = DateTime.Now;
            r.eArsivVeri.Item.makbuzNo = "";

            return r;
        }

    }
}
