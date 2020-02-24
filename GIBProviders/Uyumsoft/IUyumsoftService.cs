using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIBProviders.Uyumsoft
{
    public interface IUyumsoftService
    {
        GIBProviders.ServiceUyumsoft.ByteArrayResponse GetSystemUsersCompressedList(GIBProviders.ServiceUyumsoft.AliasType type);
        GIBProviders.ServiceUyumsoft.InvoiceIdentitiesResponse SendInvoice(GIBProviders.ServiceUyumsoft.InvoiceInfo[] invoices);
        GIBProviders.ServiceUyumsoft.InvoiceStatusResponse QueryOutboxInvoiceStatus(string[] invoiceIds);

    }
}
