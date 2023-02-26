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
           
            var  OrnekMakbuz = OrnekMakbuzHazirla();
            
            var response = eSMM.Send(OrnekMakbuz);


            if (response.Basarili)
            {
                Console.WriteLine(string.Format("Makbuz gönderildi. Makbuz No:{0}, ETTN: {1}", response.MakbuzResponseList[0].MakbuzNo, response.MakbuzResponseList[0].ETTN));
            }
            else
            {
                Console.WriteLine(response.Mesaj);
            }

            Console.ReadKey();
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
            r.Notes.Add("Yalnız:Yüz Türk Lirası");
            r.Notes.Add("Bu belgeyi hazırlamak için https://github.com/fatihaslamaci/GIBFramework kütüphanesi kullanılmıştır.");

            r.email = new List<string>();
            r.email.Add("yunus.simsek@uyumsoft.com");


            r.eArsivVeri = new GIBInterface.EFaturaPaketi.eSMM.eArsivVeri();
            r.eArsivVeri.Item = new GIBInterface.EFaturaPaketi.eSMM.eArsivVeriSerbestMeslekMakbuz();


            r.eArsivVeri.Item.aliciBilgileri = new GIBInterface.EFaturaPaketi.eSMM.aliciType();
            r.eArsivVeri.Item.aliciBilgileri.Item = new GIBInterface.EFaturaPaketi.eSMM.aliciTypeGercekKisi();
            ((GIBInterface.EFaturaPaketi.eSMM.aliciTypeGercekKisi)r.eArsivVeri.Item.aliciBilgileri.Item).adiSoyadi = "Yunus Şimşek";
            ((GIBInterface.EFaturaPaketi.eSMM.aliciTypeGercekKisi)r.eArsivVeri.Item.aliciBilgileri.Item).tckn = "12345678901";
            r.eArsivVeri.Item.paraBirimi = GIBInterface.EFaturaPaketi.eSMM.eArsivVeriSerbestMeslekMakbuzParaBirimi.USD;
            r.eArsivVeri.Item.kur = 5.51m;
            r.eArsivVeri.Item.kurSpecified = true;
            r.eArsivVeri.Item.belgeTarihi = DateTime.Now.Date;
            //voucherObject.dosyaAdi = "";
            r.eArsivVeri.Item.belgeZamani = DateTime.Now;
            r.eArsivVeri.Item.ETTN = Guid.NewGuid().ToString();
            r.eArsivVeri.Item.gonderimSekli = GIBInterface.EFaturaPaketi.eSMM.eArsivVeriSerbestMeslekMakbuzGonderimSekli.ELEKTRONIK;
            r.eArsivVeri.Item.malHizmetBilgisi = new GIBInterface.EFaturaPaketi.eSMM.eArsivVeriSerbestMeslekMakbuzMalHizmet[]{
               new  GIBInterface.EFaturaPaketi.eSMM.eArsivVeriSerbestMeslekMakbuzMalHizmet{
                   ad ="Avukatlık Ücreti",
                   burutUcret= 119.00m,
                   vergiBilgisi = new GIBInterface.EFaturaPaketi.eSMM.vergiBilgiType{
                       vergi= new GIBInterface.EFaturaPaketi.eSMM.vergiBilgiTypeVergi[]{
                                    new GIBInterface.EFaturaPaketi.eSMM.vergiBilgiTypeVergi {
                                        matrah=100.00m,
                                        vergiKodu = GIBInterface.EFaturaPaketi.eSMM.vergiBilgiTypeVergiVergiKodu.Item0015,
                                        vergiTutari= 18.00m, vergiOrani =18.00m 
                                    }
                       }
                   }
                   }};
            r.eArsivVeri.Item.odenecekTutar = 118.00m;
            r.eArsivVeri.Item.paraBirimi = GIBInterface.EFaturaPaketi.eSMM.eArsivVeriSerbestMeslekMakbuzParaBirimi.USD;
            r.eArsivVeri.Item.toplamTutar = 118.00m;
            r.eArsivVeri.Item.vergiBilgisi =
                new GIBInterface.EFaturaPaketi.eSMM.vergiBilgiType
                {
                    vergi = new GIBInterface.EFaturaPaketi.eSMM.vergiBilgiTypeVergi[] {
                        new GIBInterface.EFaturaPaketi.eSMM.vergiBilgiTypeVergi {
                            vergiKodu = GIBInterface.EFaturaPaketi.eSMM.vergiBilgiTypeVergiVergiKodu.Item0003,
                            vergiOrani = 18,
                            vergiTutari = 18m
                        }
                    }
                };
            return r;
        }

    }
}
