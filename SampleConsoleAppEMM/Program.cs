using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleConsoleAppEMM
{
    class Program
    {
        static void Main(string[] args)
        {
            GIBInterface.EMM.IEMM Entegrator;


            //Müstahsil Makbuzu için Entgeratör seçiyoruz
            Entegrator = new Uyumsoft.EMM.Sender();
            //TODO :Diğer Entegratörler eklenecek. Örnek
            //Entegrator = new Veriban.ESMM.Sender();


            //Seçilen Entegratör ile Müstahsil Makbuzu için GIB Framework oluşturulur
            var eMM = new GIBFramework.EMM.EMM(Entegrator);

            //Entegratöre özel varsayılan ayarlar var ise consola yazıyoruz (Username, Pasword, Servis adresi gibi)
            Console.WriteLine("Entegratöre özel varsayılan ayarlar");
            Console.WriteLine(eMM.DefaultSettingsJson());

            //Entegratore özel Ayarları Json formatında giriyoruz
            eMM.SettingsJson = @"{ 
                    'UserName': 'Uyumsoft', 
                    'Password': 'Uyumsoft', 
                    'Uri': 'https://efatura.uyumsoft.com.tr/Services/ProducerReceiptIntegration',
               }".Replace("'", "\"");

            //Serbest Meslek Makbuz Gönderim örneği.

            var OrnekMakbuz = OrnekMustahsilMakbuzuHazirla();

            XML_TO_HTML_TRANSFORM(OrnekMakbuz.CreditNotes[0]);




            var response = eMM.Send(OrnekMakbuz);


            if (response.Basarili)
            {
            }
            else
            {
                Console.WriteLine(response.Mesaj);
            }

            Console.ReadKey();
        }

        private static GIBInterface.EMM.SendParametersEMM OrnekMustahsilMakbuzuHazirla()
        {
            var r = new GIBInterface.EMM.SendParametersEMM();
            r.CreditNotes = new List<GIBInterface.UBLTR.CreditNoteType>();
            var makbuz = CreateVoucher();
            r.CreditNotes.Add(makbuz);
            return r;

        }

        public static GIBInterface.UBLTR.CreditNoteType XML_TO_MustahsilMakbuzu(string fileName)
        {
            var xml = File.ReadAllText(fileName, Encoding.UTF8);
            //xml deserialize işlemi
            return GIBInterface.UBLTR.CreditNoteType.Create(xml);

        }

        public static void XML_TO_HTML_TRANSFORM(GIBInterface.UBLTR.CreditNoteType eArsivVeri)
        {

            string TempFolder = "C:\\TempMakbuz";
            string TempFileName = TempFolder + "\\OrnekMustahsilMakbuz.html";
            Directory.CreateDirectory(TempFolder);

            var xslt = File.ReadAllText("tasarim.xslt", Encoding.UTF8);

            // Xml serialize örneği
            var xml = eArsivVeri.CreateXml();

            var html = GIBFramework.InvoiceTransform.TransformXMLToHTML(xml, xslt);


            File.WriteAllText(TempFileName, html, Encoding.UTF8);

            //Sisteme Tanımlı varsayılan uygulama ile html dosyasını aç
            Process.Start(new ProcessStartInfo { FileName = TempFileName, UseShellExecute = true });
        }


        public static GIBInterface.UBLTR.CreditNoteType CreateVoucher()
        {


            GIBInterface.UBLTR.CreditNoteType r = XML_TO_MustahsilMakbuzu("MustahsilMakbuzu.XML");

            r.UUID.Value = Guid.NewGuid().ToString();
            r.ID.Value = "";
            r.IssueDate.Value = DateTime.Now.Date;
            r.IssueTime.Value = DateTime.Now;


            return r;
        }


    }
}
