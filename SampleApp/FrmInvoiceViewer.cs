using GIBFramework;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SampleApp
{
    public partial class FrmInvoiceViewer : Form
    {
        public EFatura EFatura { get; set; }

        private string XsltFileDir = ".\\xslt";

        private string _xsltFileName;
        private string XsltFileName { get { return GetXsltFileName(); } set { SetXsltFileName(value); } }

        public GIBInterface.UBLTR.InvoiceType Invoice { get; set; }

        private string xslt;


        private void SetXsltFileName(string value)
        {
            if (value != _xsltFileName)
            {
                _xsltFileName = value;
                xslt = System.IO.File.ReadAllText(value, UTF8Encoding.UTF8);
                if (Invoice != null)
                {
                    ShowInvoice();
                }
            }
            _xsltFileName = value;
        }

        private void ShowInvoice()
        {
            GIBFramework.InvoiceTransform it = new GIBFramework.InvoiceTransform();
            webBrowserHTML.DocumentText = it.InvoiceToHTML(Invoice, xslt);
        }

        private string GetXsltFileName()
        {
            return _xsltFileName;
        }


        public FrmInvoiceViewer()
        {
            InitializeComponent();
        }

        private void FrmInvoiceViewer_Shown(object sender, EventArgs e)
        {
            ShowInvoice();
        }

        private void cbXsltFileName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbXsltFileName.SelectedItem != null)
            {
                XsltFileName = ((FileInfo)cbXsltFileName.SelectedItem).FullName;
            }
        }

        private void FrmInvoiceViewer_Load(object sender, EventArgs e)
        {
            {
                DirectoryInfo d = new DirectoryInfo(XsltFileDir);
                cbXsltFileName.Items.AddRange(d.GetFiles("*.xslt"));
                cbXsltFileName.SelectedIndex = 0;

            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
