using GIBInterface;
using GIBInterface.EFaturaPaketi;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Tools;

namespace GIBProviders.Logo
{
    public partial class EFatura : IMukellefListesi
    {
        public GIBInterface.EFaturaPaketi.UserList GetUserList()
        {
            GIBInterface.EFaturaPaketi.UserList r = new GIBInterface.EFaturaPaketi.UserList();

            var list = service.getUserList(loginType, ServiceLogo.UserListType.PKLIST);

            UserList rr;
            using (Stream stream2 = new MemoryStream(ZipHelper.UnzipToStream(list.binaryData.Value)))
            {
                rr = Tools.XmlSerialization.XMLDeserializeStream<UserList>(stream2);
            }
            List<GIBInterface.EFaturaPaketi.User> users = new List<User>();

            foreach (var Grupitem in rr.User.GroupBy(x => x.Identifier))
            {
                GIBInterface.EFaturaPaketi.User user = new User();
                user.Identifier = Grupitem.Key; //Identifier;
                var grup = Grupitem.First();
                user.Title = grup.Title;
                user.FirstCreationTime = grup.FirstCreationTime.ToString("yyyy-MM-dd");
                user.Documents = new DocumentType[1];
                user.Documents[0] = new DocumentType();
                user.Documents[0].type = DocType.Invoice;
                user.Documents[0].Alias = new AliasType[Grupitem.Count()];

                int i = 0;
                foreach (var item in Grupitem)
                {
                    user.Documents[0].Alias[i] = new AliasType();
                    user.Documents[0].Alias[i].Name = new string[1];
                    user.Documents[0].Alias[i].CreationTime = item.AliasCreationTime.ToString("yyyy-MM-dd");
                    user.Documents[0].Alias[i].Name[0] = item.Alias;
                    i++;
                }
                users.Add(user);
            }
            r.User = users.ToArray();
            return r;
        }
    }
}
