using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace GIBInterface.UBLTR
{
    public partial class InvoiceType
    {

        public static InvoiceType Create(string xml)
        {
            //Bu replace işlemi yapmayınca deserialize olmuyor
            xml = xml.Replace("<ext:ExtensionContent/>", "<ext:ExtensionContent></ext:ExtensionContent>");

            XmlSerializer serialize = new XmlSerializer(typeof(InvoiceType));
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                stream.Seek(0, SeekOrigin.Begin);
                return serialize.Deserialize(stream) as InvoiceType;
            }
        }

        private static XmlSerializerNamespaces _InvoiceNamespaces;
        public static XmlSerializerNamespaces InvoiceNamespaces
        {
            get
            {
                if (_InvoiceNamespaces == null)
                {
                    _InvoiceNamespaces = new XmlSerializerNamespaces();
                    _InvoiceNamespaces.Add("", "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2");
                    _InvoiceNamespaces.Add("ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");
                    _InvoiceNamespaces.Add("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                    _InvoiceNamespaces.Add("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                    _InvoiceNamespaces.Add("cctc", "urn:un:unece:uncefact:documentation:2");
                    _InvoiceNamespaces.Add("ds", "http://www.w3.org/2000/09/xmldsig#");
                    _InvoiceNamespaces.Add("qdt", "urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2");
                    _InvoiceNamespaces.Add("ubltr", "urn:oasis:names:specification:ubl:schema:xsd:TurkishCustomizationExtensionComponents");
                    _InvoiceNamespaces.Add("udt", "urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2");
                    _InvoiceNamespaces.Add("xades", "http://uri.etsi.org/01903/v1.3.2#");
                    _InvoiceNamespaces.Add("n4", "http://www.altova.com/samplexml/other-namespace");

                    _InvoiceNamespaces.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");

                }
                return _InvoiceNamespaces;
            }
        }
        public String CreateXml()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(InvoiceType));
            using (MemoryStream mstr = new MemoryStream())
            {
                serializer.Serialize(mstr, this, InvoiceNamespaces);
                return Encoding.UTF8.GetString(mstr.ToArray());
            }
        }

        public byte[] CreateBytes()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(InvoiceType));
            using (MemoryStream mstr = new MemoryStream())
            {
                serializer.Serialize(mstr, this, InvoiceNamespaces);
                return mstr.ToArray();
            }
        }

    }
}