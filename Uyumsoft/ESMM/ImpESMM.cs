using GIBInterface.EFaturaPaketi.eSMM;
using GIBInterface.ESMM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Uyumsoft.ServiceUyumsoftVoucher;

namespace Uyumsoft.ESMM
{
    public partial class ESMM : IESMM
    {
        public string ProviderId()
        {
            return "UyumsoftESMM";
        }

        public SendResultESMM Send(SendParametersESMM SendParameters)
        {


            ServiceUyumsoftVoucher.VoucherSource[] vouchers = getVoucher(SendParameters);

            ServiceUyumsoftVoucher.DocumentIdentityResponse r = service.SendVoucher(vouchers);



            return null;


        }

        private VoucherSource[] getVoucher(SendParametersESMM sendParameters)
        {

            List<VoucherSource> r = new List<VoucherSource>();

            foreach (var item in sendParameters.sendParametersESMMitems)
            {
                VoucherSource rr = new VoucherSource();

                rr.Notification = GetNotification(item);
                rr.AdditionalData = GetAdditionalData(item);
                rr.eArsivVeriSerbestMeslekMakbuz = GeteArsivVeriSerbestMeslekMakbuz(item);

                r.Add(rr);
            }


            return r.ToArray();
        }

        private ServiceUyumsoftVoucher.eArsivVeriSerbestMeslekMakbuz GeteArsivVeriSerbestMeslekMakbuz(SendParametersESMMitem item)
        {
            ServiceUyumsoftVoucher.eArsivVeriSerbestMeslekMakbuz r = new ServiceUyumsoftVoucher.eArsivVeriSerbestMeslekMakbuz();

            var a = item.eArsivVeri.Item;


            r.makbuzNo = a.makbuzNo;
            r.ETTN = a.ETTN;
            r.gonderimSekli = (ServiceUyumsoftVoucher.eArsivVeriSerbestMeslekMakbuzGonderimSekli)a.gonderimSekli;
            r.dosyaAdi = a.dosyaAdi;
            r.belgeTarihi = a.belgeTarihi;
            r.belgeZamani = a.belgeZamani.ToString();
            //r.belgeZamaniSpecified = a.belgeZamaniSpecified;
            r.toplamTutar = a.toplamTutar;
            r.odenecekTutar = a.odenecekTutar;
            r.paraBirimi = (ServiceUyumsoftVoucher.eArsivVeriSerbestMeslekMakbuzParaBirimi)a.paraBirimi;
            r.kur = a.kur;
            r.kurSpecified = a.kurSpecified;
            r.vergiBilgisi = GetVergiBilgisi(a.vergiBilgisi);
            r.aliciBilgileri = GetAliciBilgileri(a.aliciBilgileri);
            r.ynOkcFisBilgisi = getynOkcFisBilgisi(a.ynOkcFisBilgisi);
            r.malHizmetBilgisi = GetMalHizmet(a.malHizmetBilgisi);


            return r;
        }

        private ArrayOfEArsivVeriSerbestMeslekMakbuzMalHizmetMalHizmet[] GetMalHizmet(eArsivVeriSerbestMeslekMakbuzMalHizmet[] malHizmetBilgisi)
        {
            if (malHizmetBilgisi == null)
                return null;

            List<ArrayOfEArsivVeriSerbestMeslekMakbuzMalHizmetMalHizmet> r = new List<ArrayOfEArsivVeriSerbestMeslekMakbuzMalHizmetMalHizmet>();

            foreach (var item in malHizmetBilgisi)
            {
                ArrayOfEArsivVeriSerbestMeslekMakbuzMalHizmetMalHizmet rr = new ArrayOfEArsivVeriSerbestMeslekMakbuzMalHizmetMalHizmet();

                rr.ad = item.ad;
                rr.vergiBilgisi = GetVergiBilgisi(item.vergiBilgisi);
                rr.burutUcret = item.burutUcret;
                r.Add(rr);
            }

            return r.ToArray();


        }

        private ServiceUyumsoftVoucher.eArsivVeriSerbestMeslekMakbuzYnOkcFisBilgisi[] getynOkcFisBilgisi(GIBInterface.EFaturaPaketi.eSMM.eArsivVeriSerbestMeslekMakbuzYnOkcFisBilgisi[] ynOkcFisBilgisi)
        {
            if (ynOkcFisBilgisi == null)
                return null;

            List<ServiceUyumsoftVoucher.eArsivVeriSerbestMeslekMakbuzYnOkcFisBilgisi> r = new List<ServiceUyumsoftVoucher.eArsivVeriSerbestMeslekMakbuzYnOkcFisBilgisi>();

            foreach (var item in ynOkcFisBilgisi)
            {
                ServiceUyumsoftVoucher.eArsivVeriSerbestMeslekMakbuzYnOkcFisBilgisi rr = new ServiceUyumsoftVoucher.eArsivVeriSerbestMeslekMakbuzYnOkcFisBilgisi();
                rr.okcSeriNo = item.okcSeriNo;
                rr.zNo = item.zNo;
                rr.fisNo = item.fisNo;
                rr.fisTip = (ServiceUyumsoftVoucher.eArsivVeriSerbestMeslekMakbuzYnOkcFisBilgisiFisTip)item.fisTip;
                rr.fisTarih = item.fisTarih;
                rr.fisZaman = item.fisZaman.ToString();
                r.Add(rr);
            }



            return r.ToArray();

        }

        private ServiceUyumsoftVoucher.aliciType GetAliciBilgileri(GIBInterface.EFaturaPaketi.eSMM.aliciType aliciBilgileri)
        {
            if (aliciBilgileri == null)
                return null;

            ServiceUyumsoftVoucher.aliciType r = new ServiceUyumsoftVoucher.aliciType();

            r.adres = GetAdres(aliciBilgileri.adres);
            r.Item = GetItem(aliciBilgileri.Item);

            return r;
        }

        private object GetItem(object item)
        {
            if (item == null)
                return null;

            if (item is GIBInterface.EFaturaPaketi.eSMM.aliciTypeGercekKisi)
            {
                return GetItem(item as GIBInterface.EFaturaPaketi.eSMM.aliciTypeGercekKisi);
            }
            else if (item is GIBInterface.EFaturaPaketi.eSMM.aliciTypeTuzelKisi)
            {
                return GetItem(item as GIBInterface.EFaturaPaketi.eSMM.aliciTypeTuzelKisi);
            }
            else
            {
                throw new Exception("'aliciBilgileri.Item' Class Tipi 'aliciTypeGercekKisi' veya 'aliciTypeTuzelKisi' tipinden olmalıdır.");
            }

        }


        private ServiceUyumsoftVoucher.aliciTypeGercekKisi GetItem(GIBInterface.EFaturaPaketi.eSMM.aliciTypeGercekKisi item)
        {
            if (item == null)
                return null;

            ServiceUyumsoftVoucher.aliciTypeGercekKisi r = new ServiceUyumsoftVoucher.aliciTypeGercekKisi();
            r.tckn = item.tckn;
            r.adiSoyadi = item.adiSoyadi;
            return r;
        }


        private ServiceUyumsoftVoucher.aliciTypeTuzelKisi GetItem(GIBInterface.EFaturaPaketi.eSMM.aliciTypeTuzelKisi item)
        {
            if (item == null)
                return null;

            ServiceUyumsoftVoucher.aliciTypeTuzelKisi r = new ServiceUyumsoftVoucher.aliciTypeTuzelKisi();
            r.vkn = item.vkn;
            r.unvan = item.unvan;
            return r;
        }



        private ServiceUyumsoftVoucher.aliciTypeAdres GetAdres(GIBInterface.EFaturaPaketi.eSMM.aliciTypeAdres adres)
        {
            if (adres == null)
                return null;

            ServiceUyumsoftVoucher.aliciTypeAdres r = new ServiceUyumsoftVoucher.aliciTypeAdres();

            r.caddeSokak = adres.caddeSokak;
            r.binaAd = adres.binaAd;
            r.binaNo = adres.binaNo;
            r.kapiNo = adres.kapiNo;
            r.kasabaKoy = adres.kasabaKoy;
            r.semt = adres.semt;
            r.sehir = adres.sehir;
            r.postaKod = adres.postaKod;
            r.ulke = (ServiceUyumsoftVoucher.aliciTypeAdresUlke)adres.ulke;
            r.vDaire = adres.vDaire;

            return r;

        }

        private ServiceUyumsoftVoucher.vergiBilgiType GetVergiBilgisi(GIBInterface.EFaturaPaketi.eSMM.vergiBilgiType vergiBilgisi)
        {
            ServiceUyumsoftVoucher.vergiBilgiType r = new ServiceUyumsoftVoucher.vergiBilgiType();

            r.vergilerToplami = vergiBilgisi.vergilerToplami;
            r.vergi = GetvergiBilgiTypeVergi(vergiBilgisi.vergi);
            r.tevkifat = GetTevkifat(vergiBilgisi.tevkifat);

            return r;
        }

        private ServiceUyumsoftVoucher.vergiBilgiTypeTevkifat[] GetTevkifat(GIBInterface.EFaturaPaketi.eSMM.vergiBilgiTypeTevkifat[] tevkifat)
        {
            if (tevkifat == null)
                return null;

            List<ServiceUyumsoftVoucher.vergiBilgiTypeTevkifat> r = new List<ServiceUyumsoftVoucher.vergiBilgiTypeTevkifat>();

            ServiceUyumsoftVoucher.vergiBilgiTypeTevkifat rr = new ServiceUyumsoftVoucher.vergiBilgiTypeTevkifat();

            foreach (var a in tevkifat)
            {
                rr.tevkifatKodu = a.tevkifatKodu;
                rr.tevkifatTutari = a.tevkifatTutari;
                rr.tevkifatOrani = a.tevkifatOrani;
                //rr.tevkifatOraniSpecified = a.tevkifatOraniSpecified;
            }

            r.Add(rr);


            return r.ToArray();

        }

        private ServiceUyumsoftVoucher.vergiBilgiTypeVergi[] GetvergiBilgiTypeVergi(GIBInterface.EFaturaPaketi.eSMM.vergiBilgiTypeVergi[] vergi)
        {
            if (vergi == null)
                return null;

            List<ServiceUyumsoftVoucher.vergiBilgiTypeVergi> r = new List<ServiceUyumsoftVoucher.vergiBilgiTypeVergi>();

            ServiceUyumsoftVoucher.vergiBilgiTypeVergi rr = new ServiceUyumsoftVoucher.vergiBilgiTypeVergi();

            foreach (var a in vergi)
            {
                rr.matrah = a.matrah;
                rr.vergiKodu = (ServiceUyumsoftVoucher.vergiBilgiTypeVergiVergiKodu)a.vergiKodu;
                rr.vergiTutari = a.vergiTutari;
                rr.vergiOrani = a.vergiOrani;
                rr.vergiOraniSpecified = a.vergiOraniSpecified;
            }

            r.Add(rr);


            return r.ToArray();
        }

        private AdditionalData[] GetAdditionalData(SendParametersESMMitem item)
        {

            List<AdditionalData> r = new List<AdditionalData>();
            int i = 0;

            if (item.Notes != null)
            {
                foreach (var note in item.Notes)
                {
                    AdditionalData rr = new AdditionalData();
                    rr.Id = (i++).ToString();
                    rr.Name = "Not";
                    rr.Value = note;
                    r.Add(rr);
                }
            }

            if (item.Kur > 0)
            {
                AdditionalData rr = new AdditionalData();
                rr.Id = (i++).ToString();
                rr.Name = "Kur";
                rr.Value = item.Kur.ToString();
                r.Add(rr);
            }

            return r.ToArray();
        }

        private DocumentNotificationInfo GetNotification(SendParametersESMMitem item)
        {

            if ((item.email == null) || (item.email.Count == 0))
                return null;

            DocumentNotificationInfo r = new DocumentNotificationInfo();

            List<DocumentMailingEntry> MailingList = new List<DocumentMailingEntry>();

            foreach (var item2 in item.email)
            {
                var Mailing = new DocumentMailingEntry();
                Mailing.EnableNotification = true;
                Mailing.Subject = "x nolu makbuzunuz";
                Mailing.To = item2;

                Mailing.Attachment = new DocumentMailingAttachmentInfo();
                Mailing.Attachment.Pdf = true;
                Mailing.Attachment.Xml = true;

                MailingList.Add(Mailing);
            }

            r.Mailing = MailingList.ToArray();


            return r;
        }
    }
}
