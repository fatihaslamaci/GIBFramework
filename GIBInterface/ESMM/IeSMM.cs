using GIBInterface.EFaturaPaketi.eSMM;
using System;
using System.Collections.Generic;
using System.Text;

namespace GIBInterface.ESMM
{
    public interface IESMM
    {
        string ProviderId();
        SendResultESMM Send(SendParametersESMM SendParameters);
    }

    public class SendResultESMM
    {

    }

    public class SendParametersESMM
    {
        public List<SendParametersESMMitem> sendParametersESMMitems { get; set; }
        
    }

    public class SendParametersESMMitem
    {
        public eArsivVeri eArsivVeri { get; set; }
        public List<string>Notes { get; set; }
        public double Kur { get; set; }
        public List<string> email { get; set; }
    }

}
