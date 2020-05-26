using GIBInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SahteEntegrator
{
    public partial class EFatura : IEFatura
    {
        public string ProviderId()
        {
            return "Sahte-Entegrator";
        }

        public SendResult SendInvoice(SendParameters SendParameters)
        {
            if(SendParameters==null)
            {
                throw new ArgumentNullException("SendParameters");
            }

            SendResult r = new SendResult();

            

            if ( Convert.ToDateTime(token).AddMinutes(ExpirationTimeMinute()) < DateTime.Now )
            {
                throw new Exception("Token süresi dolmuş");
            }

            r.IsSucceded = true;
            r.Message = "Gönderildi";
            r.ResultInvoices = new List<ResultInvoice>();

            foreach (var item in SendParameters.InvoicesInfo)
            {
                ResultInvoice rItem = new ResultInvoice();
                rItem.ETN = item.Invoices.UUID.Value;
                rItem.FaturaNo = item.Invoices.ID.Value;
                r.ResultInvoices.Add(rItem);
            }

            return r;

        }
    }
}
