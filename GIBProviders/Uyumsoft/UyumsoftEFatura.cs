using System;

namespace GIBProviders.Uyumsoft
{
    public partial class EFatura : IDisposable
    {
        internal IUyumsoftService service;
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (service != null)
                {
                    if (service is IDisposable)
                    {
                        (service as IDisposable).Dispose();
                    }
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }



        public EFatura(IUyumsoftService service)
        {
            this.service = service;
        }

        public EFatura()
        {
            service = new ImpUyumsoftService(this);
        }

        /*
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
        */

    }
}
