using GIBProviders.ServiceUyumsoft;
using System;
using System.ServiceModel;

namespace GIBProviders.Uyumsoft
{
    public class ImpUyumsoftService : IUyumsoftService, IDisposable
    {

        internal ServiceUyumsoft.IntegrationClient _service;

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_service != null)
                {
                    _service.Close();
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private ServiceUyumsoft.IntegrationClient service
        {
            get
            {
                if (_service == null)
                {
                    var Uri = settings.Settings["Uri"];


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
                    var UserName = settings.Settings["UserName"];
                    var Password = settings.Settings["Password"];
                    _service.ClientCredentials.UserName.UserName = UserName;
                    _service.ClientCredentials.UserName.Password = Password;
                }
                return _service;
            }

        }

        GIBInterface.ISettings settings;
        public ImpUyumsoftService(GIBInterface.ISettings settings)
        {
            this.settings = settings;
        }


        public ByteArrayResponse GetSystemUsersCompressedList(AliasType type)
        {
            return service.GetSystemUsersCompressedList(type);
        }

        public InvoiceStatusResponse QueryOutboxInvoiceStatus(string[] invoiceIds)
        {
            return service.QueryOutboxInvoiceStatus(invoiceIds);

        }

        public InvoiceIdentitiesResponse SendInvoice(InvoiceInfo[] invoices)
        {
            return service.SendInvoice(invoices);
        }


    }
}
