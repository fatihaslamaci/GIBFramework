using GIBInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIBProviders.Logo
{
    public partial class EFatura : ILogin
    {
        internal string SessionID = "";
        internal ServiceLogo.LoginType loginType = new ServiceLogo.LoginType();

        public int ExpirationTimeMinute()
        {
            return 20;
        }

        public void LoadToken(string Token)
        {
            SessionID = Token;
        }

        public bool Login()
        {
            loginType.userName = Settings["UserName"];
            loginType.passWord = Settings["Password"];
            loginType.version = "1";
            return service.Login(loginType, out SessionID);

        }

        public string Token()
        {
            return SessionID;
        }
    }
}
