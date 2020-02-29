using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace GIBFrameworkTest.Uyumsoft
{
    [TestClass]
    public class MukellefSorguTest
    {
        [TestMethod]
        public void TestMukellefBilgisi()
        {
            GIBProviders.Uyumsoft.IUyumsoftService mockUyumsoftService = new MockUyumsoftService();
            GIBInterface.IEFatura provider = new GIBProviders.Uyumsoft.EFatura(mockUyumsoftService);
            GIBFramework.EFatura eFatura = new GIBFramework.EFatura(provider);
            var user = eFatura.MukellefBilgisi("6130438766");
            Assert.IsNotNull(user, "Mükellef Bulunamadı");
            user = eFatura.MukellefBilgisi("1111111111");
            Assert.IsNull(user, "Mükellef olmaması gerekirdi");
        }

        [TestMethod]
        public void TestSendInvoice()
        {
            GIBProviders.Uyumsoft.IUyumsoftService mockUyumsoftService = new MockUyumsoftService();
            GIBInterface.IEFatura provider = new GIBProviders.Uyumsoft.EFatura(mockUyumsoftService);
            GIBFramework.EFatura eFatura = new GIBFramework.EFatura(provider);

            GIBInterface.SendParameters val = new GIBInterface.SendParameters();
            val.InvoicesInfo = CreateInvoiceInfoList();

            GIBInterface.SendResult response = eFatura.SendInvoice(val);

            Assert.AreEqual(response.IsSucceded, true, "Fatura başarısız oldu");
            Assert.AreEqual(response.ResultInvoices.Count, 2, "Fatura sayısı 2 olmalı");

        }


        [TestMethod]
        public void TestDurumSorgula()
        {
            GIBProviders.Uyumsoft.IUyumsoftService mockUyumsoftService = new MockUyumsoftService();
            GIBInterface.IEFatura provider = new GIBProviders.Uyumsoft.EFatura(mockUyumsoftService);
            GIBFramework.EFatura eFatura = new GIBFramework.EFatura(provider);


            List<GIBInterface.QueryStatusParameters> val = new List<GIBInterface.QueryStatusParameters>();




            {
                GIBInterface.QueryStatusParameters item = new GIBInterface.QueryStatusParameters();
                item.RecordId = 0;
                item.InvoiceUUID = Guid.NewGuid();
                val.Add(item);
            }

            {
                GIBInterface.QueryStatusParameters item = new GIBInterface.QueryStatusParameters();
                item.RecordId = 0;
                item.InvoiceUUID = Guid.NewGuid();
                val.Add(item);
            }

            var res = eFatura.FaturaDurumSorgula(val);


            Assert.AreEqual(res.Count, 2, "2 fatura dönmeli");
            Assert.AreEqual(res[0].InvoiceStatus, GIBInterface.QueryStatus.BasariliSonuclandi);
            Assert.AreEqual(res[1].InvoiceStatus, GIBInterface.QueryStatus.BasariliSonuclandi);

        }


        private List<GIBInterface.InvoiceInfo> CreateInvoiceInfoList()
        {

            List<GIBInterface.InvoiceInfo> r = new List<GIBInterface.InvoiceInfo>();

            r.Add(CreateInvoice(".\\OrnekFaturalar\\02-TicariFaturaOrnegi.xml", "TST2020000000001"));
            r.Add(CreateInvoice(".\\OrnekFaturalar\\02-TicariFaturaOrnegi.xml", "TST2020000000002"));


            return r;
        }

        private GIBInterface.InvoiceInfo CreateInvoice(string FileName, string FaturaNo)
        {
            GIBInterface.InvoiceInfo r = new GIBInterface.InvoiceInfo();

            r.AliciPostaKutusuEtiketi = "gp";
            r.Customer = new GIBInterface.CustomerInfo();
            r.Invoices = LoadInvoiceXML(FileName, FaturaNo);
            r.LocalDocumentId = FaturaNo;


            return r;
        }

        private GIBInterface.UBLTR.InvoiceType LoadInvoiceXML(string FileName, string FaturaNo)
        {
            var xml = System.IO.File.ReadAllText(FileName);
            var r = GIBInterface.UBLTR.InvoiceType.Create(xml);
            r.ID.Value = FaturaNo;
            r.UUID.Value = Guid.NewGuid().ToString();
            return r;
        }
    }
}
