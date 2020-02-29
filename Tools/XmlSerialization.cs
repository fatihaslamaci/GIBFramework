using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Tools
{
    public static class XmlSerialization
    {
        public static string XMLSerialize<T>(T value)
        {
            string xmlString;
            XmlSerializer _xmlserializer = new XmlSerializer(typeof(T));
            using (MemoryStream stream = new MemoryStream())
            {
                _xmlserializer.Serialize(stream, value);
                xmlString = Encoding.UTF8.GetString(stream.ToArray());
                stream.Close();
            }
            return xmlString;
        }

        public static T XMLDeserialize<T>(string XMLString)
        {
            XmlSerializer MyDeserializer = new XmlSerializer(typeof(T));
            T obj;
            using (StringReader SR = new StringReader(XMLString))
            {
                using (System.Xml.XmlReader XR = new System.Xml.XmlTextReader(SR))
                {
                    obj = (T)MyDeserializer.Deserialize(XR);
                }
            }
            return obj;
        }

        public static T XMLDeserializeStream<T>(Stream XML)
        {
            XmlSerializer MyDeserializer = new XmlSerializer(typeof(T));
            T obj;
            using (System.Xml.XmlReader XR = new System.Xml.XmlTextReader(XML))
            {
                obj = (T)MyDeserializer.Deserialize(XR);
            }
            return obj;
        }
    }
}
