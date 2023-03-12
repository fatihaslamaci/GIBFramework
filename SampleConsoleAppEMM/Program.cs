using System;
using System.Collections.Generic;
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

            var OrnekMakbuz = OrnekMakbuzHazirla();

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

        private static GIBInterface.EMM.SendParametersEMM OrnekMakbuzHazirla()
        {
            throw new NotImplementedException();
  
        }

      

    }
}
