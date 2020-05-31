using GIBInterface;
using GIBInterface.EFaturaPaketi;
using System.IO;
using Tools;

namespace Uyumsoft
{
    public partial class EFatura : IMukellefListesi
    {
        public UserList GetUserList()
        {
            UserList r = new UserList();
            var list = service.GetSystemUsersCompressedList(ServiceUyumsoft.AliasType.InvoiceReceiverbox);
            using (Stream stream2 = new MemoryStream(ZipHelper.UnzipToStream(list.Value)))
            {
                r = Tools.XmlSerialization.XMLDeserializeStream<UserList>(stream2);
            }
            return r;
        }
    }
}
