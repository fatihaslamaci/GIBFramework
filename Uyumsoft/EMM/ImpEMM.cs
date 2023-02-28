using GIBInterface.EMM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uyumsoft.EMM
{
    public partial class Sender : IEMM
    {
        public string ProviderId()
        {
            return "UyumsoftEMM";
        }

        public SendResultEMM Send(SendParametersEMM SendParameters)
        {
            throw new NotImplementedException();
        }
    }
}
