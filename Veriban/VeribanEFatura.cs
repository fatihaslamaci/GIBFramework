using GIBInterface;
using GIBInterface.EFaturaPaketi;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Veriban
{
    public partial class VeribanEFatura : IDisposable
    {
        private ServiceVeriban.TransferDocumentServiceClient _service;

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


        private ServiceVeriban.TransferDocumentServiceClient service
        {
            get
            {
                if (_service == null)
                {
                    var Uri = Settings["Uri"];
                    var encoding = new MtomMessageEncodingBindingElement();
                    encoding.MessageVersion = MessageVersion.Soap12;
                    var transport = new HttpTransportBindingElement();
                    var customBinding = new CustomBinding(encoding, transport);
                    _service = new ServiceVeriban.TransferDocumentServiceClient(customBinding, new EndpointAddress(new Uri(Uri)));

                }
                return _service;
            }

        }

       

       

        public VeribanEFatura()
        {

        }

    

     

      
    }
}
