using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIBInterface
{


    public interface IEFatura
    {
        SendResult SendInvoice(SendParameters SendParameters);
    }

    public interface IFaturaDurumuSorgula
    {
        List<InvoiceStatusResponse> InvoiceStatus(List<InvoiceStatusParameters> SendParameters);
    }

    public class InvoiceStatusParameters
    {
        public Guid InvoiceUUID { get; set; }
    }

    public class InvoiceStatusResponse
    {
        public InvoiceStatus InvoiceStatus { get; set; }

    }


    public enum InvoiceStatus
    {
        IslemDevamEdiyor=0,
        BasariliSonuclandi=1,
        BasarisizSonuclandi=2
    }

    public class CustomerInfo
    {
        public string VknTckn { get; set; }

        public string Alias { get; set; }

        public string Title { get; set; }
    }

    public class InvoiceInfo
    {
        public long RecordId { get; set; } // DB ye kayıt sırasında otomatik verilir 
        public string LocalDocumentId { get; set; }
        public CustomerInfo Customer { get; set; }
        public UBLTR.InvoiceType Invoices { get; set; }
    }


    public class SendParameters
    {

        public List<InvoiceInfo> InvoicesInfo { get; set; }
    }

    public class SendResult
    {
        public bool IsSucceded { get; set; }
        public string Message { get; set; }
        public List<ResultInvoice> ResultInvoices { get; set; }
        public string Error { get; set; }
        public object ErrorDetail { get; set; }
    }

    public class ResultInvoice
    {
        public string ETN { get; set; }
        public string FaturaNo { get; set; }
    }


}
