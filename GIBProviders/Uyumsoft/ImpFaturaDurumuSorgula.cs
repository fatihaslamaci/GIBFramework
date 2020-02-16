using GIBInterface;
using System;
using System.Collections.Generic;

namespace GIBProviders.Uyumsoft
{
    public partial class EFatura : IFaturaDurumuSorgula
    {
        public List<InvoiceStatusResponse> InvoiceStatus(List<InvoiceStatusParameters> SendParameters)
        {
            List<InvoiceStatusResponse> r = new List<InvoiceStatusResponse>();
            var response = new GIBProviders.ServiceUyumsoft.InvoiceStatusResponse();
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
                    InvoiceStatusResponse rr = new InvoiceStatusResponse();
                    rr.InvoiceStatus = new InvoiceStatus();
                    rr.InvoiceStatus = GIBInterface.InvoiceStatus.IslemDevamEdiyor;
                    r.Add(rr);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Hata");
                //lblInvoiceStatus.Text = "Durum bilgisi alınamadı";
                throw ex;  
            }

            return r;
        }
    }
}
