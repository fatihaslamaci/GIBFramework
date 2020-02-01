using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace GIBInterface.EFaturaPaketi
{
    public partial class UserList
    {
        public static UserList Create(string xml)
        {
            XmlSerializer serialize = new XmlSerializer(typeof(UserList));
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                stream.Seek(0, SeekOrigin.Begin);
                return serialize.Deserialize(stream) as UserList;
            }
        }

        public String CreateXml()
        {

            XmlSerializer serializer = new XmlSerializer(typeof(UserList));
            using (MemoryStream mstr = new MemoryStream())
            {
                serializer.Serialize(mstr, this);
                return Encoding.UTF8.GetString(mstr.ToArray());
            }
        }

    }
}
