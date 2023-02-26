using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Uyumsoft.ESMM
{
    public class ImpServiceUyumsoftVoucher : IServiceUyumsoftVoucher, IDisposable
    {

        internal ServiceUyumsoftVoucher.VoucherIntegrationClient _service;

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public ServiceUyumsoftVoucher.VoucherIntegrationClient service
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

                    _service = new ServiceUyumsoftVoucher.VoucherIntegrationClient(binding, endpoint);
                    var UserName = settings.Settings["UserName"];
                    var Password = settings.Settings["Password"];
                    _service.ClientCredentials.UserName.UserName = UserName;
                    _service.ClientCredentials.UserName.Password = Password;
                }
                return _service;

                
            }

        }

        GIBInterface.ISettings settings;
        public ImpServiceUyumsoftVoucher(GIBInterface.ISettings settings)
        {
            this.settings = settings;
        }


        public ServiceUyumsoftVoucher.DocumentIdentityResponse SendVoucher(ServiceUyumsoftVoucher.VoucherSource[] vouchers)
        {
            return service.SendVoucher(vouchers);
        }


    }
}
