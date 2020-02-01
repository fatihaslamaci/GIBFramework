using GIBInterface;
using GIBInterface.EFaturaPaketi;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace GIBProviders.Uyumsoft
{
    public class EFatura : IEFatura, IMukellefListesi, ISettings
    {
        internal ServiceUyumsoft.IntegrationClient _service;

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
        private ServiceUyumsoft.IntegrationClient service
        {
            get
            {
                if (_service == null)
                {
                    var Uri = Settings["Uri"];


                    var binding = new BasicHttpBinding()
                    {
                        Name = "BindingName",
                        MaxBufferSize = 2147483647,
                        MaxReceivedMessageSize = 2147483647,

                    };
                    binding.Security = new BasicHttpSecurity();
                    binding.Security.Mode = BasicHttpSecurityMode.TransportWithMessageCredential;

                    var endpoint = new EndpointAddress(Uri);

                    _service = new ServiceUyumsoft.IntegrationClient(binding, endpoint);
                    var UserName = Settings["UserName"];
                    var Password = Settings["Password"];
                    _service.ClientCredentials.UserName.UserName = UserName;
                    _service.ClientCredentials.UserName.Password = Password;
                }
                return _service;
            }

        }

        public Dictionary<string, string> DefaultSettings()
        {
            return new Dictionary<string, string>()
            {
                {"UserName","Kullanıcı Adını Buraya Giriniz"},
                {"Password","Şifreyi Buraya Giriniz"},
                {"Uri","https://efatura-test.uyumsoft.com.tr/services/Integration" },

            };
        }

        public UserList GetUserList()
        {
            UserList r = new UserList();
            var list = service.GetSystemUsersCompressedList(ServiceUyumsoft.AliasType.InvoiceReceiverbox);
            using (Stream stream2 = new MemoryStream(ZipHelper.UnzipToStream(list.Value)))
            {
                r = Tools.XmlSerialization.XMLDeserializeStream<UserList>(stream2);
            }
            return r;
        }

    }
}
