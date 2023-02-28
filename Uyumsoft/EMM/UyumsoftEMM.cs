using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Uyumsoft.EMM
{
    public partial class Sender
    {

        ServiceUyumsoftProducerReceiptIntegration.ProducerReceiptIntegrationClient  _serviceClient = null;
        private ServiceUyumsoftProducerReceiptIntegration.ProducerReceiptIntegrationClient serviceClient
        {
            get
            {
                if (_serviceClient == null)
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

                    _serviceClient = new ServiceUyumsoftProducerReceiptIntegration.ProducerReceiptIntegrationClient(binding, endpoint);
                    var UserName = Settings["UserName"];
                    var Password = Settings["Password"];
                    serviceClient.ClientCredentials.UserName.UserName = UserName;
                    serviceClient.ClientCredentials.UserName.Password = Password;
                }

                return _serviceClient;
            }
        }

    }
}
