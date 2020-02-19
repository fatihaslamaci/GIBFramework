using System;
using System.ServiceModel;

namespace GIBProviders.Logo
{
    public partial class EFatura
    {
        internal ServiceLogo.PostBoxServiceClient _service;
        public ServiceLogo.LoginType loginType = new ServiceLogo.LoginType();


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

                    loginType.userName = Settings["UserName"];
                    loginType.passWord = Settings["Password"];
                    loginType.version = "1";
                    var a = service.Login(loginType, out SessionID);
                    


                }
                return _service;
            }
        }

    }
}
