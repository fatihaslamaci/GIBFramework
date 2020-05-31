using GIBInterface;
using System;
using System.Collections.Generic;

namespace Uyumsoft
{
    public partial class EFatura : IFaturaDurumuSorgula
    {
        public List<QueryStatusResponse> InvoiceStatus(List<QueryStatusParameters> SendParameters)
        {
            if (SendParameters is null)
            {
                return null;
            }

            List<QueryStatusResponse> r = new List<QueryStatusResponse>();
            var response = new ServiceUyumsoft.InvoiceStatusResponse();
            List<string> guid = new List<string>();

            foreach (var item in SendParameters)
            {
                guid.Add(item.InvoiceUUID.ToString());
            }
            try
            {
                response = service.QueryOutboxInvoiceStatus(guid.ToArray());

                foreach (var item in response.Value)
                {
                    QueryStatusResponse rr = new QueryStatusResponse();
                    rr.InvoiceUUID = new Guid(item.InvoiceId);
                    rr.Message = item.Message;

                    if (item.Status == ServiceUyumsoft.InvoiceStatus.Approved)
                    {
                        rr.InvoiceStatus = GIBInterface.QueryStatus.BasariliSonuclandi;
                    }
                    else if (item.Status == ServiceUyumsoft.InvoiceStatus.Declined)
                    {
                        rr.InvoiceStatus = QueryStatus.BasarisizSonuclandi;
                    }
                    else if (item.Status == ServiceUyumsoft.InvoiceStatus.Canceled)
                    {
                        rr.InvoiceStatus = QueryStatus.BasarisizSonuclandi;
                    }
                    else if (item.Status == ServiceUyumsoft.InvoiceStatus.Error)
                    {
                        rr.InvoiceStatus = QueryStatus.BasarisizSonuclandi;
                    }
                    else
                    {
                        rr.InvoiceStatus = QueryStatus.IslemDevamEdiyor;
                    }

                    r.Add(rr);
                }
            }
            catch (Exception ex)
            {
                //TODO
                throw ex;
            }

            return r;
        }
    }
}
