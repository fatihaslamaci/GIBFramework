using System;

namespace UyumsoftDotNetStandart
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

    }
}
