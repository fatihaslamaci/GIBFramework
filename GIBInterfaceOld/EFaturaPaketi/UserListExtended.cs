using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace GIBInterfaceOld.EFaturaPaketi
{
    public partial class UserList
    {
        public static UserList Create(string xml)
        {
            UserList r = new UserList();
            XmlSerializer serialize = new XmlSerializer(typeof(UserList));
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                //stream.Seek(0, SeekOrigin.Begin);
                //r = (serialize.Deserialize(stream)) as UserList;
                using (XmlReader reader = XmlReader.Create(stream, new XmlReaderSettings() { XmlResolver = null }))
                {
                    r = serialize.Deserialize(reader) as UserList;
                }

            }

                        


            return r;
        }

        public String CreateXml()
        {
            string r = string.Empty;

            XmlSerializer serializer = new XmlSerializer(typeof(UserList));
            using (MemoryStream mstr = new MemoryStream())
            {
                serializer.Serialize(mstr, this);
                r = Encoding.UTF8.GetString(mstr.ToArray());
            }
            return r;
        }

    }
}
