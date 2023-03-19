using GIBInterface.ESMM;
using System;
using System.Collections.Generic;
using System.Text;

namespace GIBInterface.EMM
{
    public interface IEMM
    {
        string ProviderId();
        SendResultEMM Send(SendParametersEMM SendParameters);
    }


    public class SendResultEMM
    {
        public bool Basarili { get; set; }
        public string Mesaj { get; set; }
    }

    public class SendParametersEMM
    {
        public List<UBLTR.CreditNoteType> CreditNotes { get; set;}

    }

}
