using System;
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
            var user = eFatura.MukellefBilgisi("1111111111");


        }
    }
}
