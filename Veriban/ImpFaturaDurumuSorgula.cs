using GIBInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veriban
{
    public partial class VeribanEFatura : IFaturaDurumuSorgula
    {
        public List<QueryStatusResponse> InvoiceStatus(List<QueryStatusParameters> SendParameters)
        {
            throw new NotImplementedException();
        }
    }
}
