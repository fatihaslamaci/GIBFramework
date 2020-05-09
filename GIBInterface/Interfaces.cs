using System.Collections.Generic;

namespace GIBInterface
{


    public interface ISettings
    {
        Dictionary<string, string> DefaultSettings();
        Dictionary<string, string> Settings { get; set; }

    }


    public interface IManipulatedInvoice
    {
        GIBInterface.UBLTR.InvoiceType Manipulated(GIBInterface.UBLTR.InvoiceType invoice);
    }

    public interface IEArsiv
    {

    }

    public interface IMukellefListesi
    {
        EFaturaPaketi.UserList GetUserList();
    }

    public interface ILogin
    {
        bool Login();
        int ExpirationTimeMinute();
        string Token();
        void LoadToken(string Token);
        string TokenId();
    }


}
