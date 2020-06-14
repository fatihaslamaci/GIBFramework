using GIBInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veriban
{
    public partial class VeribanEFatura : ISettings
    {
        Dictionary<string, string> _Settings;
        public Dictionary<string, string> Settings
        {
            get
            {
                if (_Settings == null)
                    return DefaultSettings();
                else
                    return _Settings;
            }
            set
            {
                _Settings = value;
            }
        }

        public Dictionary<string, string> DefaultSettings()
        {
            return new Dictionary<string, string>()
            {
                {"UserName","Kullanıcı Adını Buraya Giriniz"},
                {"Password","Şifreyi Buraya Giriniz"},
                {"Uri","http://transfertest.veriban.com.tr/TransferDocumentService.svc" },

                {"ftp_UserName","FTP Kullanıcı Adını Buraya Giriniz"},
                {"ftp_Password","FTP Şifreyi Buraya Giriniz"},
                {"ftp_Adress","ftp://ftp.veriban.com.tr/GIBUserList.xml" }

            };
        }
    }
}
