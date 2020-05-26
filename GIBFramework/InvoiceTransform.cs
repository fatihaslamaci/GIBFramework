using GIBInterface.UBLTR;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Xsl;

namespace GIBFramework
{
    public class InvoiceTransform
    {
        public string InvoiceToHTML(InvoiceType invoice, string alternativeXslt)
        {
            string r = "";

            var xslt = string.Empty;

            var doc = XSLT_EmbeddedDocumentBinaryObject(invoice);

            if (doc != null)
            {
                using (var stream = new MemoryStream(doc))
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    using (var reader = new StreamReader(stream))
                    {
                        xslt = reader.ReadToEnd();
                    }
                }
            }
            else
            {
                xslt = alternativeXslt;
            }

            if (string.IsNullOrWhiteSpace(xslt))
            {
                r = invoice.CreateXml();
            }
            else
            {
                r = invoice.CreateXml();
                r = TransformXMLToHTML(r, xslt);
            }

            return r;

        }

        private static byte[] XSLT_EmbeddedDocumentBinaryObject(InvoiceType invoice)
        {
            byte[] r = null;
            if ((invoice.AdditionalDocumentReference != null) && (invoice.AdditionalDocumentReference.Length > 0))
            {
                DocumentReferenceType doc = invoice.AdditionalDocumentReference[0];
                AttachmentType attacment = doc.Attachment;
                if ((attacment != null) && (attacment.EmbeddedDocumentBinaryObject != null))
                {
                    string fileName = attacment.EmbeddedDocumentBinaryObject.filename;
                    if (Path.GetExtension(fileName) == ".xslt")
                    {
                        return attacment.EmbeddedDocumentBinaryObject.Value;
                    }
                }
            }

            return r;
        }


        public static string TransformXMLToHTML(string inputXml, string xsltString)
        {

            XslCompiledTransform transform = new XslCompiledTransform();
            using (XmlReader reader = XmlReader.Create(new StringReader(xsltString)))
            {
                transform.Load(reader);
            }
            StringWriter results = new StringWriter();
            using (XmlReader reader = XmlReader.Create(new StringReader(inputXml)))
            {
                transform.Transform(reader, null, results);
            }
            return results.ToString();
        }

    }
}
