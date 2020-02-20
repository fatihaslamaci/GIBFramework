using System;
using System.ServiceModel;

namespace GIBProviders.Logo
{
    public partial class EFatura
    {
        internal ServiceLogo.PostBoxServiceClient _service;
        


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
