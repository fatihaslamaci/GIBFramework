using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace GIBInterface.UBLTR
{
    public partial class CreditNoteType
    {

        public static CreditNoteType Create(string xml)
        {
            //Bu replace işlemi yapmayınca deserialize olmuyor
            xml = xml.Replace("<ext:ExtensionContent/>", "<ext:ExtensionContent></ext:ExtensionContent>");

            XmlSerializer serialize = new XmlSerializer(typeof(CreditNoteType));
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                stream.Seek(0, SeekOrigin.Begin);
                return serialize.Deserialize(stream) as CreditNoteType;
            }
        }

        private static XmlSerializerNamespaces _creditNoteNamespace;
        public static XmlSerializerNamespaces CreditNoteNamespaces
        {
            get
            {
                if (_creditNoteNamespace == null)
                {
                    _creditNoteNamespace = new XmlSerializerNamespaces();
                    _creditNoteNamespace.Add("", "urn:oasis:names:specification:ubl:schema:xsd:CreditNote-2");
                    _creditNoteNamespace.Add("ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");
                    _creditNoteNamespace.Add("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                    _creditNoteNamespace.Add("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                    _creditNoteNamespace.Add("cctc", "urn:un:unece:uncefact:documentation:2");
                    _creditNoteNamespace.Add("ds", "http://www.w3.org/2000/09/xmldsig#");
                    _creditNoteNamespace.Add("qdt", "urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2");
                    _creditNoteNamespace.Add("ubltr", "urn:oasis:names:specification:ubl:schema:xsd:TurkishCustomizationExtensionComponents");
                    _creditNoteNamespace.Add("udt", "urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2");
                    _creditNoteNamespace.Add("xades", "http://uri.etsi.org/01903/v1.3.2#");
                    _creditNoteNamespace.Add("n4", "http://www.altova.com/samplexml/other-namespace");

                    _creditNoteNamespace.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");

                }
                return _creditNoteNamespace;
            }
        }
        public string CreateXml()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CreditNoteType));
            using (MemoryStream mstr = new MemoryStream())
            {
                serializer.Serialize(mstr, this, CreditNoteNamespaces);
                return Encoding.UTF8.GetString(mstr.ToArray());
            }
        }

        public byte[] CreateBytes()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CreditNoteType));
            using (MemoryStream mstr = new MemoryStream())
            {
                serializer.Serialize(mstr, this, CreditNoteNamespaces);
                return mstr.ToArray();
            }
        }

    }
}