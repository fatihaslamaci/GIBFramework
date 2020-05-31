using System;

namespace Uyumsoft
{
    public interface IUyumsoftService
    {
        ServiceUyumsoft.ByteArrayResponse GetSystemUsersCompressedList(ServiceUyumsoft.AliasType type);
        ServiceUyumsoft.InvoiceIdentitiesResponse SendInvoice(ServiceUyumsoft.InvoiceInfo[] invoices);
        ServiceUyumsoft.InvoiceStatusResponse QueryOutboxInvoiceStatus(string[] invoiceIds);

    }
}
