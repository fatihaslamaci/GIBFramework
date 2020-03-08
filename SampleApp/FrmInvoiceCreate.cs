using GIBInterface.UBLTR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

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

            if (User==null)
            {
                MessageBox.Show("Girdiğiniz VKN/TCKN için Mükellef bulunamadı: " + txbVKN.Text);
                return;
            }


            invoice.AccountingCustomerParty.Party.PartyIdentification[0].ID.Value = User.Identifier;
            
            if (User.Identifier.Length == 11)//TCKN
            {
                invoice.AccountingCustomerParty.Party.Person = new PersonType();
                invoice.AccountingCustomerParty.Party.Person.FirstName = new FirstNameType();
                invoice.AccountingCustomerParty.Party.Person.FirstName.Value = User.Title.Split(' ')[0];

                invoice.AccountingCustomerParty.Party.Person.FamilyName = new FamilyNameType();
                invoice.AccountingCustomerParty.Party.Person.FamilyName.Value = User.Title.Split(' ')[1];
                invoice.AccountingCustomerParty.Party.PartyIdentification[0].ID.schemeID = "TCKN";

            }
            else//VKN
            {
                invoice.AccountingCustomerParty.Party.PartyName = new PartyNameType();
                invoice.AccountingCustomerParty.Party.PartyName.Name = new NameType1();
                invoice.AccountingCustomerParty.Party.PartyName.Name.Value = User.Title;
                invoice.AccountingCustomerParty.Party.PartyIdentification[0].ID.schemeID = "VKN";
            }




            invoice.IssueDate.Value = DateTime.Now.Date;

            GIBInterface.InvoiceInfo item = new GIBInterface.InvoiceInfo();
            item.Customer = new GIBInterface.CustomerInfo();
            item.Customer.Alias = User.Documents[0].Alias[0].Name[0];
            item.Customer.VknTckn = User.Identifier;
            item.Customer.Title = User.Title;


            invoice.UUID.Value = Guid.NewGuid().ToString();
            invoice.ID.Value = "BFF2020000000003";
            item.LocalDocumentId = "BFF2020000000003";

            item.Invoices = invoice;

            prm.InvoicesInfo.Add(item);


            FrmInvoiceViewer frm = new FrmInvoiceViewer();
            frm.EFatura = EFatura;
            frm.Invoice = item.Invoices;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                var rslt = EFatura.SendInvoice(prm);
                if (string.IsNullOrWhiteSpace(rslt.Message))
                {
                    if (rslt.IsSucceded)
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

        private void btnMukellefAra_Click(object sender, EventArgs e)
        {
            using (FrmMukellefAra frm = new FrmMukellefAra())
            {
                frm.EFatura = EFatura;
                if (frm.ShowDialog()== DialogResult.OK)
                {
                    txbVKN.Text = frm.SelectedUser.Identifier;   
                }
            }

        }
    }
}
