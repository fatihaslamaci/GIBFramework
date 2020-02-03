using GIBInterface.UBLTR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Xsl;

namespace SampleApp
{
    public partial class FrmInvoiceCreate : Form
    {
        public GIBFramework.EFatura EFatura { get; set; }
        private string SampleFileDir = ".\\OrnekFaturalar";
        private string XsltFileDir = ".\\xslt";

        private string _sampleFileName;
        private string SampleFileName { get { return GetSampleFileName(); } set { SetSampleFileName(value); } }

        private string _xsltFileName;
        private string XsltFileName { get { return GetXsltFileName(); } set { SetXsltFileName(value); } }



        InvoiceType invoice;
        private string XML;
        private string xslt;

        private void SetSampleFileName(string value)
        {
            if (value != _sampleFileName)
            {
                _sampleFileName = value;
                XML = System.IO.File.ReadAllText(value, UTF8Encoding.UTF8);
                tbXml.Text = XML;
                invoice = InvoiceType.Create(XML);
                ShowInvoice(invoice);
            }
            _sampleFileName = value;
        }

        private string GetSampleFileName()
        {
            return _sampleFileName;
        }

        private void SetXsltFileName(string value)
        {
            if (value != _xsltFileName)
            {
                _xsltFileName = value;
                xslt = System.IO.File.ReadAllText(value, UTF8Encoding.UTF8);
                if (invoice != null)
                {
                    ShowInvoice(invoice);
                }
            }
            _xsltFileName = value;
        }

        private string GetXsltFileName()
        {
            return _xsltFileName;
        }



        public FrmInvoiceCreate()
        {
            InitializeComponent();
        }

        private void FrmInvoiceCreate_Load(object sender, EventArgs e)
        {

        }

        private void FrmInvoiceCreate_Shown(object sender, EventArgs e)
        {
            {
                DirectoryInfo d = new DirectoryInfo(XsltFileDir);
                cbXsltFileName.Items.AddRange(d.GetFiles("*.xslt"));
                cbXsltFileName.SelectedIndex = 0;
            }

            {
                DirectoryInfo d = new DirectoryInfo(SampleFileDir);
                cbFileName.Items.AddRange(d.GetFiles("*.xml"));
                cbFileName.SelectedIndex = 0;
            }
            
        }

        private void cbFileName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFileName.SelectedItem != null)
            {
                SampleFileName = ((FileInfo)cbFileName.SelectedItem).FullName;
            }
        }

        private void cbXsltFileName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbXsltFileName.SelectedItem != null)
            {
                XsltFileName = ((FileInfo)cbXsltFileName.SelectedItem).FullName;
            }
        }

        public void ShowInvoice(InvoiceType invoice)
        {
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
                xslt = this.xslt;
            }

            if (string.IsNullOrWhiteSpace(xslt))
            {
                var xml = invoice.CreateXml();
                webBrowser1.DocumentText = xml;
            }
            else
            {
                var xml = invoice.CreateXml();
                webBrowser1.DocumentText = TransformXMLToHTML(xml, xslt);
            }


        }


        private static byte[] XSLT_EmbeddedDocumentBinaryObject(InvoiceType invoice)
        {
            byte[] r = null;
            if ((invoice.AdditionalDocumentReference != null) && (invoice.AdditionalDocumentReference.Count() > 0))
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

        private void btnGonder_Click(object sender, EventArgs e)
        {
            GIBInterface.SendParameters prm = new GIBInterface.SendParameters();
            prm.InvoicesInfo = new List<GIBInterface.InvoiceInfo>();

            var User = EFatura.MukellefBilgisi("1111111104");


            GIBInterface.InvoiceInfo item = new GIBInterface.InvoiceInfo();
            item.Customer = new GIBInterface.CustomerInfo();
            item.Customer.Alias = User.Documents[0].Alias[0].Name[0];
            item.Customer.VknTckn = invoice.AccountingCustomerParty.Party.PartyIdentification[0].ID.Value;
            item.Customer.Title = invoice.AccountingCustomerParty.Party.PartyName.Name.Value;


            item.Invoices = invoice;

            prm.InvoicesInfo.Add(item);


            EFatura.SendInvoice(prm);


        }
    }
}
