using System;
using System.Collections.Generic;
using GIBProviders.ServiceUyumsoft;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GIBFrameworkTest.Uyumsoft
{
    [TestClass]
    public class MukellefSorguTest
    {

       


        [TestMethod]
        public void TestMukellefBilgisi()
        {
            GIBProviders.Uyumsoft.IUyumsoftService mockUyumsoftService = new MockUyumsoftService();
            GIBInterface.IEFatura provider =  new GIBProviders.Uyumsoft.EFatura(mockUyumsoftService);
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
        }

        private List<GIBInterface.InvoiceInfo> CreateInvoiceInfoList()
        {

            List<GIBInterface.InvoiceInfo> r = new List<GIBInterface.InvoiceInfo>();

            r.Add(CreateInvoice());


            return r;
        }

        private GIBInterface.InvoiceInfo CreateInvoice()
        {
            GIBInterface.InvoiceInfo r = new GIBInterface.InvoiceInfo();

            r.AliciPostaKutusuEtiketi = "gp";
            r.Customer = new GIBInterface.CustomerInfo();
            r.Invoices = CreateInvoice2();
            r.LocalDocumentId = Guid.NewGuid().ToString();
  
            return r;
        }

        private GIBInterface.UBLTR.InvoiceType CreateInvoice2()
        {
            GIBInterface.UBLTR.InvoiceType r = new GIBInterface.UBLTR.InvoiceType();
            r.ID = new GIBInterface.UBLTR.IDType();
            r.ID.Value = "TST2020000000001";
            r.UUID = new GIBInterface.UBLTR.UUIDType();
            r.UUID.Value = Guid.NewGuid().ToString();
            return r;
        }
    }
}
