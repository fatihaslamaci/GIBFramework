using GIBInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIBProviders.Logo
{
    public partial class EFatura : IEFatura
    {
        public string ProviderId()
        {
            return "Logo";
        }

        public SendResult SendInvoice(SendParameters SendParameters)
        {
            
            
            throw new NotImplementedException();

        }
    }
}
