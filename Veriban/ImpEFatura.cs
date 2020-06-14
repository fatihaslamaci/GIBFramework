using GIBInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veriban
{
    public partial class EFatura : IEFatura
    {
        public string ProviderId()
        {
            return "Veriban";
        }

        public SendResult SendInvoice(SendParameters SendParameters)
        {
            throw new NotImplementedException();
        }

    }
}
