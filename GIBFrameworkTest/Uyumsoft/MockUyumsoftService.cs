using GIBProviders.ServiceUyumsoft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIBFrameworkTest.Uyumsoft
{
    public class MockUyumsoftService : GIBProviders.Uyumsoft.IUyumsoftService
    {
        public ByteArrayResponse GetSystemUsersCompressedList(AliasType type)
        {
            ByteArrayResponse r = new ByteArrayResponse();

            r.IsSucceded = true;
            r.Value = System.IO.File.ReadAllBytes(".\\Uyumsoft\\MockData\\UserPkList.zip");
            return r;


        }

        public InvoiceStatusResponse QueryOutboxInvoiceStatus(string[] invoiceIds)
        {
            throw new NotImplementedException();
        }

        public InvoiceIdentitiesResponse SendInvoice(InvoiceInfo[] invoices)
        {
            throw new NotImplementedException();
        }
    }
}
