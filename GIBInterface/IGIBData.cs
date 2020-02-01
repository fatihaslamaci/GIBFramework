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

    }

}
