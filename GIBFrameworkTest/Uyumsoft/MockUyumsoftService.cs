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
            throw new NotImplementedException();
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
