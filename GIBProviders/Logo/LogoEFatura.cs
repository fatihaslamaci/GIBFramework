using System;
using System.ServiceModel;

namespace GIBProviders.Logo
{
    public partial class EFatura : IDisposable
    {
        internal ServiceLogo.PostBoxServiceClient _service;

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

        private ServiceLogo.PostBoxServiceClient service
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
                    binding.Security.Mode = BasicHttpSecurityMode.Transport;

                    var endpoint = new EndpointAddress(Uri);

                    _service = new ServiceLogo.PostBoxServiceClient(binding, endpoint);


                }
                return _service;
            }
        }

    }
}
