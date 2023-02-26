using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uyumsoft.ESMM
{
    public interface IServiceUyumsoftVoucher
    {
        ServiceUyumsoftVoucher.DocumentIdentityResponse SendVoucher(ServiceUyumsoftVoucher.VoucherSource[] vouchers);
    }
}
