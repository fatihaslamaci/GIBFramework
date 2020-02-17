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

        private string _sampleFileName;
        private string SampleFileName { get { return GetSampleFileName(); } set { SetSampleFileName(value); } }

        InvoiceType invoice;
        private string XML;

        private void SetSampleFileName(string value)
        {
            if (value != _sampleFileName)
            {
                _sampleFileName = value;
                XML = System.IO.File.ReadAllText(value, UTF8Encoding.UTF8);
                tbXml.Text = XML;
                invoice = InvoiceType.Create(XML);
                ShowInvoice(EFatura.ManipulatedInvoice(invoice));
            }
            _sampleFileName = value;
        }

        private string GetSampleFileName()
        {
            return _sampleFileName;
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

     

        public void ShowInvoice(InvoiceType invoice)
        {
            //GIBFramework.InvoiceTransform it = new GIBFramework.InvoiceTransform();
            //webBrowser1.DocumentText = it.InvoiceToHTML(invoice,this.xslt);
        }

        private void btnGonder_Click(object sender, EventArgs e)
        {
            GIBInterface.SendParameters prm = new GIBInterface.SendParameters();
            prm.InvoicesInfo = new List<GIBInterface.InvoiceInfo>();

            var User = EFatura.MukellefBilgisi(txbVKN.Text);

            invoice.AccountingCustomerParty.Party.PartyIdentification[0].ID.Value = User.Identifier;
            invoice.AccountingCustomerParty.Party.PartyName.Name.Value = User.Title;
            invoice.IssueDate.Value = DateTime.Now.Date;

            GIBInterface.InvoiceInfo item = new GIBInterface.InvoiceInfo();
            item.Customer = new GIBInterface.CustomerInfo();
            item.Customer.Alias = User.Documents[0].Alias[0].Name[0];
            item.Customer.VknTckn = invoice.AccountingCustomerParty.Party.PartyIdentification[0].ID.Value;
            item.Customer.Title = invoice.AccountingCustomerParty.Party.PartyName.Name.Value;


            invoice.UUID.Value = Guid.NewGuid().ToString();
            invoice.ID.Value = "BFF2020000000003";
            item.LocalDocumentId = "BFF2020000000003";

            item.Invoices = invoice;

            prm.InvoicesInfo.Add(item);


            FrmInvoiceViewer frm = new FrmInvoiceViewer();
            frm.EFatura = EFatura;
            frm.Invoice = item.Invoices;
            if (frm.ShowDialog()== DialogResult.OK)
            {
                var rslt = EFatura.SendInvoice(prm);
                if(string.IsNullOrWhiteSpace(rslt.Message))
                {
                    if(rslt.IsSucceded)
                    {
                        MessageBox.Show("Başarılı");
                    }
                    else
                    {
                        MessageBox.Show(rslt.Error);
                    }
                }
                else
                {
                    MessageBox.Show(rslt.Message);
                }
                
            }

        }
    }
}
