using GIBInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIBProviders.SahteEntegrator
{
    public partial class EFatura : ILogin
    {
        internal string token = "";

        public int ExpirationTimeMinute()
        {
            return 1;
        }

        public void LoadToken(string Token)
        {
            token = Token;
        }

        public bool Login()
        {
            var userName = Settings["UserName"];
            var passWord = Settings["Password"];

            if ((userName=="İstanbul")&&(passWord=="1453"))
            {
                token = DateTime.Now.ToString("yyyy-MM-dd hh:mm");
                return true;
            }
            else
            {
                return false;
            }

        }

        public string Token()
        {
            return token;
        }

        public string TokenId()
        {
            return "SahteEnt_" + DateTime.Now.ToString("yyyy-MM-dd hh:mm"); ;
        }
    }
}
