using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIBInterface
{


    public interface IEFatura
    {
        string ProviderId();
        SendResult SendInvoice(SendParameters SendParameters);
    }

    public interface IFaturaDurumuSorgula
    {
        List<QueryStatusResponse> InvoiceStatus(List<QueryStatusParameters> SendParameters);
    }

    public class QueryStatusParameters
    {
        public long RecordId { get; set; } // Database Kayıt ID si
        public Guid InvoiceUUID { get; set; }
    }

    public class QueryStatusResponse
    {
        public Guid InvoiceUUID { get; set; }
        public QueryStatus InvoiceStatus { get; set; }
        public string Message { get; set; }

    }


    public class QueryStatusResponseData: QueryStatusResponse
    {
        public long RecordId { get; set; }

    }



    public enum QueryStatus
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

        public string AliciPostaKutusuEtiketi { get; set; }
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



    public class SendInvoiceListDataFind
    {
    }

    public class SendInvoiceData
    {
        public int Id { get; set; }
        public string ETN { get; set; }
        public string InvoiceXML { get; set; }
        public bool Send_isSucceded { get; set; }
        public string Send_Message { get; set; }
        public string Send_Error { get; set; }
        public string Send_ErrorDetail { get; set; }
        public string Send_returnETN { get; set; }
        public string Send_returnFaturaNo { get; set; }
        public int Query_Status { get; set; }
        public string Query_Message { get; set; }
    }

}
