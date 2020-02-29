using GIBInterface;
using GIBInterface.EFaturaPaketi;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace GIBProviders.Veriban
{
    public class EFatura : IEFatura, ISettings, IMukellefListesi, ILogin
    {
        private ServiceVeriban.TransferDocumentServiceClient _service;
        private ServiceVeriban.TransferDocumentServiceClient service
        {
            get
            {
                if (_service == null)
                {
                    var Uri = Settings["Uri"];
                    var encoding = new MtomMessageEncodingBindingElement();
                    encoding.MessageVersion = MessageVersion.Soap12;
                    var transport = new HttpTransportBindingElement();
                    var customBinding = new CustomBinding(encoding, transport);
                    _service = new ServiceVeriban.TransferDocumentServiceClient(customBinding, new EndpointAddress(new Uri(Uri)));

                }
                return _service;
            }

        }

        internal string SessionID = "";

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

        public EFatura()
        {

        }

        public UserList GetUserList()
        {
            FtpInfo ftpInfo = new FtpInfo();
            ftpInfo.UserName = Settings["ftp_UserName"];
            ftpInfo.Password = Settings["ftp_Password"];
            ftpInfo.Adress = Settings["ftp_Adress"];

            return MukellefListesi.GetUserList(ftpInfo, "");
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

        public SendResult SendInvoice(SendParameters SendParameters)
        {
            throw new NotImplementedException();
        }

        public string ProviderId()
        {
            return "Veriban";
        }
    }
}
