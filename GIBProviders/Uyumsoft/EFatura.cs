using GIBInterface;
using GIBInterface.EFaturaPaketi;
using GIBProviders.ServiceUyumsoft;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
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

        public SendResult SendInvoice(SendParameters SendParameters)
        {
            GIBProviders.ServiceUyumsoft.InvoiceInfo[] InvoiceInfo = new GIBProviders.ServiceUyumsoft.InvoiceInfo[0];
            var xml = SendParameters.InvoicesInfo[0].Invoices.CreateXml();
            InvoiceInfo[0].Invoice = UyumsoftInvoiceDeserialize(xml);
            var response = service.SendInvoice(InvoiceInfo);
            if (response.IsSucceded)
            {
                var a = string.Format("Fatura Gönderildi || GUID:{0} || Number:{1} ", response.Value[0].Id.ToString(), response.Value[0].Number.ToString());
            }
            else
            {
               //TODO
            }

            return null;
        }

        public static GIBProviders.ServiceUyumsoft.InvoiceType UyumsoftInvoiceDeserialize(string xml)
        {
            //Bu replace işlemi yapmayınca deserialize olmuyor
            //xml = xml.Replace("<ext:ExtensionContent/>", "<ext:ExtensionContent></ext:ExtensionContent>");

            XmlSerializer serialize = new XmlSerializer(typeof(GIBProviders.ServiceUyumsoft.InvoiceType));
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                stream.Seek(0, SeekOrigin.Begin);
                return serialize.Deserialize(stream) as GIBProviders.ServiceUyumsoft.InvoiceType;
            }
        }


    }
}
