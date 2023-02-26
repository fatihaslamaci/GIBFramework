using GIBInterface.UBLTR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace GIBInterface.EFaturaPaketi.eSMM
{
    public partial class eArsivVeri
    {
        public static eArsivVeri Create(string xml)
        {
            
            XmlSerializer serialize = new XmlSerializer(typeof(eArsivVeri));
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                stream.Seek(0, SeekOrigin.Begin);
                return serialize.Deserialize(stream) as eArsivVeri;
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
                    //_InvoiceNamespaces.Add("", "http://earsiv.efatura.gov.tr");
                    //_InvoiceNamespaces.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
                    //_InvoiceNamespaces.Add("xsd", "http://www.w3.org/2001/XMLSchema");
                    //_InvoiceNamespaces.Add("schemaLocation", "http://earsiv.efatura.gov.tr eArsivVeri.xsd");
                }
                return _InvoiceNamespaces;
            }
        }
        public string CreateXml()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(eArsivVeri));
            using (MemoryStream mstr = new MemoryStream())
            {
                serializer.Serialize(mstr, this, InvoiceNamespaces);
                return Encoding.UTF8.GetString(mstr.ToArray());
            }
        }

        public byte[] CreateBytes()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(eArsivVeri));
            using (MemoryStream mstr = new MemoryStream())
            {
                serializer.Serialize(mstr, this, InvoiceNamespaces);
                return mstr.ToArray();
            }
        }

    }
}
