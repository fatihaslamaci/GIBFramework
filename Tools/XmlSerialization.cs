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
                //stream.Close();
                //stream.Dispose();
            }
            return xmlString;
        }

        public static T XMLDeserialize<T>(string XMLString)
        {
            XmlSerializer MyDeserializer = new XmlSerializer(typeof(T));
            T obj;
            using (StringReader SR = new StringReader(XMLString))
            {
                using (System.Xml.XmlReader XR = GetXR(SR))
                {
                    obj = (T)MyDeserializer.Deserialize(XR);
                }
            }
            return obj;
        }

        private static System.Xml.XmlReader GetXR(StringReader SR)
        {
            System.Xml.XmlReaderSettings settings = new System.Xml.XmlReaderSettings() { XmlResolver = null };
            return System.Xml.XmlReader.Create(SR, settings);
        }



        public static T XMLDeserializeStream<T>(Stream XML)
        {
            XmlSerializer MyDeserializer = new XmlSerializer(typeof(T));
            T obj;
            //using (System.Xml.XmlReader XR = new System.Xml.XmlTextReader(XML))
            using (System.Xml.XmlReader XR = GetXR1(XML))
            {
                obj = (T)MyDeserializer.Deserialize(XR);
            }
            return obj;
        }

        private static System.Xml.XmlReader GetXR1(Stream XML)
        {
            System.Xml.XmlReaderSettings settings = new System.Xml.XmlReaderSettings() { XmlResolver = null };
            return System.Xml.XmlReader.Create(XML, settings);

            //return new System.Xml.XmlTextReader(XML);
        }
    }
}
