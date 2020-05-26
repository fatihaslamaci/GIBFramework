using GIBInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SahteEntegrator
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
                token = DateTime.Now.AddMinutes(ExpirationTimeMinute()).ToString("yyyy-MM-dd HH:mm:ss");
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
            return "SahteEnt_" + Settings["UserName"]+"_"+Settings["Password"];
        }
    }
}
