namespace GIBProviders.Uyumsoft
{
    public interface IUyumsoftService
    {
        GIBProviders.ServiceUyumsoft.ByteArrayResponse GetSystemUsersCompressedList(GIBProviders.ServiceUyumsoft.AliasType type);
        GIBProviders.ServiceUyumsoft.InvoiceIdentitiesResponse SendInvoice(GIBProviders.ServiceUyumsoft.InvoiceInfo[] invoices);
        GIBProviders.ServiceUyumsoft.InvoiceStatusResponse QueryOutboxInvoiceStatus(string[] invoiceIds);

    }
}
