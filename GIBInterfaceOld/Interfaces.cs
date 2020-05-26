﻿using System.Collections.Generic;

namespace GIBInterfaceOld
{


    public interface ISettings
    {
        Dictionary<string, string> DefaultSettings();
        Dictionary<string, string> Settings { get; set; }

    }


    public interface IManipulatedInvoice
    {
        GIBInterfaceOld.UBLTR.InvoiceType Manipulated(GIBInterfaceOld.UBLTR.InvoiceType invoice);
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
