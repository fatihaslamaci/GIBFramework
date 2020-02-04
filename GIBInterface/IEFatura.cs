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


    public class CustomerInfo
    {
        public string VknTckn { get; set; }

        public string Alias { get; set; }

        public string Title { get; set; }
    }

    public class InvoiceInfo
    {
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
    }

    public class ResultInvoice
    {
        public string ETN { get; set; }
        public string FaturaNo { get; set; }
    }


}
