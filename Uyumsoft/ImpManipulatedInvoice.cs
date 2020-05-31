using GIBInterface;

namespace Uyumsoft
{
    public partial class EFatura : IManipulatedInvoice
    {
        public GIBInterface.UBLTR.InvoiceType Manipulated(GIBInterface.UBLTR.InvoiceType invoice)
        {

            string xml = invoice.CreateXml();
            GIBInterface.UBLTR.InvoiceType r = GIBInterface.UBLTR.InvoiceType.Create(xml);
            r.AccountingSupplierParty.Party.PartyIdentification[0].ID.Value = Settings["VergiNumarasi"];
            return r;
        }
    }
}
