using GIBProviders.ServiceUyumsoft;
using System.Linq;

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
            InvoiceStatusResponse r = new InvoiceStatusResponse();

            r.IsSucceded = true;
            r.Value = new InvoiceStatusInfo[invoiceIds.Count()];

            int i = 0;
            foreach (var item in invoiceIds)
            {
                r.Value[i] = new InvoiceStatusInfo();
                r.Value[i].InvoiceId = item;
                r.Value[i].Status = InvoiceStatus.Approved;
                r.Value[i].StatusCode = 1000;
                i++;
            }

            return r;
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
