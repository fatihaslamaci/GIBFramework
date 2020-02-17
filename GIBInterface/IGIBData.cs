using GIBInterface.EFaturaPaketi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIBInterface
{
    public interface IGIBData
    {
        void GIBUserListSave(EFaturaPaketi.UserList userList);
        bool BugunMukelefSorgulandi();
        User SQLiteUserFind(string VergiNo);
        List<User> SQLiteUserFindByUnvan(string Unvan);
        SendParameters SendInvoiceInsert(SendParameters sendParameters, string providerId);
        void SendInvoiceUpdate(SendParameters sendParameters, SendResult r);

        List<SendInvoiceData> SendInvoiceList(SendInvoiceListDataFind val, string providerId);

        void DurumSorgulamaYaz(List<QueryStatusResponse> val);



    }

}
