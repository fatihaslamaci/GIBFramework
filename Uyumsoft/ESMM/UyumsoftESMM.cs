using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uyumsoft.ESMM
{
    public partial class ESMM : IDisposable
    {
        internal IServiceUyumsoftVoucher service;
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



        public ESMM(IServiceUyumsoftVoucher service)
        {
            this.service = service;
        }

        public ESMM()
        {
            service = new ImpServiceUyumsoftVoucher(this);
        }
    }
}
