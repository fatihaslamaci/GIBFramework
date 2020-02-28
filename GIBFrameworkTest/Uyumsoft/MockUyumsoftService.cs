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
            InvoiceIdentitiesResponse r = new InvoiceIdentitiesResponse();
            r.IsSucceded = true;
            r.Value = new InvoiceIdentity[invoices.Count()];
            

            int i = 0;

            foreach (var item in invoices)
            {
                r.Value[i] = new InvoiceIdentity();
                r.Value[i].Id = item.Invoice.UUID.Value;
                r.Value[i].Number = item.Invoice.ID.Value;
                i++;
            }


            return r;
        }
    }
}
