using GIBInterfaceOld.EFaturaPaketi;
using System;
using System.Collections.Generic;

namespace GIBInterfaceOld
{
    public interface IGIBData
    {
        void GIBUserListSave(EFaturaPaketi.UserList userList);
        bool BugunMukellefSorgulandi();
        User SQLiteUserFind(string VergiNo);
        List<User> SQLiteUserFindByUnvan(string Unvan);
        SendParameters SendInvoiceInsert(SendParameters sendParameters, string providerId);
        void SendInvoiceUpdate(SendParameters sendParameters, SendResult r);

        List<SendInvoiceData> SendInvoiceList(SendInvoiceListDataFind val, string providerId);

        void DurumSorgulamaYaz(List<QueryStatusResponseData> val);

        TokenType GetToken(string TokenId);
        int InsertToken(TokenType val);

    }

    public class TokenType
    {
      public int Id { get; set; }
      public string TokenId { get; set; }
      public string Token { get; set; }
      public DateTime CreationTime { get; set; }
}


}
