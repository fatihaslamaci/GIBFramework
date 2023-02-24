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
            webBrowserXML.DocumentText = Invoice.CreateXml();
            txbUBLText.Text = Invoice.CreateXml();
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

        private void btnXsltAdding_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog1 = new OpenFileDialog()
            {
                InitialDirectory = @"C:\",
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "xslt",
                Filter = "xslt files (*.xslt)|*.xslt",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            })
            {

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    var res = openFileDialog1.FileName;
                }

                try
                {
                    byte[] readText = File.ReadAllBytes(openFileDialog1.FileName);
                    using (var fs = new FileStream(".\\xslt\\" + Path.GetFileName(openFileDialog1.FileName), FileMode.Create, FileAccess.Write))
                    {
                        fs.Write(readText, 0, readText.Length);

                    }
                    cbXsltFileName.Items.Clear();
                    FrmInvoiceViewer_Load(sender, e);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception caught in process: {0}", ex.Message);

                }
            }
        }

        private void btnEsmmXsltTest_Click(object sender, EventArgs e)
        {

            var xml = File.ReadAllText("C:\\esmm\\SELF_EMPLOYMENT_RECEIPT.XML", Encoding.UTF8);
            var xslt = File.ReadAllText("C:\\esmm\\xsltCustom.xslt", Encoding.UTF8);


            var earsivVeri =  GIBInterface.EFaturaPaketi.eSMM.eArsivVeri.Create(xml);

            //var earsivVeri =  new GIBInterface.EFaturaPaketi.eSMM.eArsivVeri();

            //earsivVeri.baslik = new GIBInterface.EFaturaPaketi.eSMM.baslikType();
            //earsivVeri.Item = new GIBInterface.EFaturaPaketi.eSMM.eArsivVeriSerbestMeslekMakbuz();
            //earsivVeri.Item.malHizmetBilgisi = new GIBInterface.EFaturaPaketi.eSMM.eArsivVeriSerbestMeslekMakbuzMalHizmet[1];



            var xml2 = earsivVeri.CreateXml();


            webBrowserHTML.DocumentText = GIBFramework.InvoiceTransform.TransformXMLToHTML(xml2, xslt);
            webBrowserXML.DocumentText = xml;
            txbUBLText.Text =xml;


        }
    }
}
