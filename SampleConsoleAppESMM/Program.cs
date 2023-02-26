using GIBInterface;
using System;
using System.Collections.Generic;
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

            //Entgeratör seçiyoruz
            Entegrator = new Uyumsoft.ESMM.ESMM();

            //Seçilen Entegratör ile GIB Framework oluşturulur
            var eSMM = new GIBFramework.ESMM.ESMM(Entegrator);

            //Entegratöre özel varsayılan ayarlar var ise consola yazıyoruz (Username, Pasword, Servis adresi gibi)
            Console.WriteLine("Entegratöre özel varsayılan ayarlar");
            Console.WriteLine(eSMM.DefaultSettingsJson());

            //Entegratore özel Ayarları Json formatında giriyoruz
            eSMM.SettingsJson = @"{ 
                    'UserName': 'Uyumsoft', 
                    'Password': 'Uyumsoft', 
                    'Uri': 'https://efatura-test.uyumsoft.com.tr/services/Integration',
                    'VergiNumarasi':'1111111111'
               }".Replace("'", "\"");

            //Serbest Meslek Makbuz Gönderim örneği.
            var request = eSMM.Send(new GIBInterface.ESMM.SendParametersESMM());

            Console.ReadKey();


        }
    }
}
