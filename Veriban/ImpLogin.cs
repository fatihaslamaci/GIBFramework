using GIBInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veriban
{
    public partial class VeribanEFatura : ILogin
    {
        internal string SessionID = "";

        public bool Login()
        {
            var UserName = Settings["UserName"];
            var Password = Settings["Password"];
            return service.Login(UserName, Password, out SessionID);
        }

        public int ExpirationTimeMinute()
        {
            return 10;
        }

        public string Token()
        {
            return SessionID;
        }

        public void LoadToken(string Token)
        {
            SessionID = Token;
        }

        public string TokenId()
        {
            //TODO :Token ID yi md5 yapılacak, ve sondaki saat silinecek
            return "Veriban_" + Settings["UserName"] + "_" + Settings["Password"] + "_" + DateTime.Now.ToString("yyyy-MM-dd hh:mm"); ;
        }

    }
}
