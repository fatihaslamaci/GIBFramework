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
            SendResult r = new SendResult();
            GIBProviders.ServiceUyumsoft.InvoiceInfo[] InvoiceInfo = new GIBProviders.ServiceUyumsoft.InvoiceInfo[SendParameters.InvoicesInfo.Count];

            int i = 0;
            foreach (var item in SendParameters.InvoicesInfo)
            {
                var xml = item.Invoices.CreateXml();
                InvoiceInfo[i] = new ServiceUyumsoft.InvoiceInfo();
                InvoiceInfo[i].Invoice = UyumsoftInvoiceDeserialize(xml);
                InvoiceInfo[i].LocalDocumentId = item.LocalDocumentId;
                InvoiceInfo[i].TargetCustomer = new ServiceUyumsoft.CustomerInfo()
                {
                    VknTckn = item.Customer.VknTckn,
                    Alias = item.Customer.Alias,
                    Title = item.Customer.Title
                };



                i++;
            }
            var response = service.SendInvoice(InvoiceInfo);
            r.Message = response.Message;
            r.IsSucceded = response.IsSucceded;
            if (response.IsSucceded)
            {
                r.ResultInvoices = new List<ResultInvoice>();
                foreach (var item in response.Value)
                {
                    ResultInvoice ri = new ResultInvoice();
                    ri.FaturaNo = item.Number;
                    ri.ETN = item.Id;
                    r.ResultInvoices.Add(ri);
                }
            }

            return r;
        }

        public static GIBProviders.ServiceUyumsoft.InvoiceType UyumsoftInvoiceDeserialize(string xml)
        {
            xml = xml.Replace("<Invoice ", "<InvoiceType ");
            xml = xml.Replace("</Invoice>", "</InvoiceType>");

            var lines = xml.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.None);
            StringBuilder sb = new StringBuilder();
            {
                string s = "<InvoiceType xmlns:ubltr=\"urn:oasis:names:specification:ubl:schema:xsd:TurkishCustomizationExtensionComponents\" xmlns:cctc=\"urn:un:unece:uncefact:documentation:2\" xmlns:cbc=\"urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2\" xmlns:cac=\"urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2\" xmlns:qdt=\"urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2\" xmlns:ds=\"http://www.w3.org/2000/09/xmldsig#\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:ext=\"urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2\" xmlns:xades=\"http://uri.etsi.org/01903/v1.3.2#\" xmlns:udt=\"urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2\">";
                int i = 0;
                foreach (var item in lines)
                {
                    i++;
                    if (i == 2)
                    {
                        sb.Append(s);
                    }
                    else if (i > 7)
                    {
                        sb.Append(item);
                    }
                }
                xml = sb.ToString();
            }
            XmlSerializer serialize = new XmlSerializer(typeof(GIBProviders.ServiceUyumsoft.InvoiceType));
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                stream.Seek(0, SeekOrigin.Begin);
                return serialize.Deserialize(stream) as GIBProviders.ServiceUyumsoft.InvoiceType;
            }
        }


    }
}
