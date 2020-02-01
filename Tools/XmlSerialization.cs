using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Tools
{
    public static class XmlSerialization
    {
        public static string XMLSerialize<T>(T value)
        {
            XmlSerializer _xmlserializer = new XmlSerializer(typeof(T));
            MemoryStream stream = new MemoryStream();
            _xmlserializer.Serialize(stream, value);
            string xmlString = Encoding.UTF8.GetString(stream.ToArray());
            stream.Close();
            return xmlString;
        }

        public static T XMLDeserialize<T>(string XMLString)
        {
            XmlSerializer MyDeserializer = new XmlSerializer(typeof(T));
            StringReader SR = new StringReader(XMLString);
            System.Xml.XmlReader XR = new System.Xml.XmlTextReader(SR);
            T obj;
            obj = (T)MyDeserializer.Deserialize(XR);
            return obj;
        }

        public static T XMLDeserializeStream<T>(Stream XML)
        {
            XmlSerializer MyDeserializer = new XmlSerializer(typeof(T));
            System.Xml.XmlReader XR = new System.Xml.XmlTextReader(XML);
            T obj;
            obj = (T)MyDeserializer.Deserialize(XR);
            return obj;
        }
    }
}
