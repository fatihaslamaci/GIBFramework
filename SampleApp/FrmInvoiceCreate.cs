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
                //tbXml.Text = XML;
                invoice = InvoiceType.Create(XML);
                invoice = EFatura.ManipulatedInvoice(invoice);
                GridDoldur(invoice);


            }
            _sampleFileName = value;
        }

        private string GetSampleFileName()
        {
            return _sampleFileName;
        }


        private void GridDoldur(InvoiceType invocice)
        {
            dataGridView1.Rows.Clear();

            if ((invoice != null) && (invoice.InvoiceLine != null))
            {

                dataGridView1.Visible = false;
                dataGridView1.Rows.Clear();
                foreach (var item in invoice.InvoiceLine)
                {
                    var row = new DataGridViewRow();
                    row.CreateCells(dataGridView1);
                    row.Cells[0].Value = item.Item.Name.Value; //Aciklama
                    row.Cells[1].Value = item.InvoicedQuantity.Value;//Miktar
                    row.Cells[2].Value = item.Price.PriceAmount.Value;//Birim Fiyat
                    row.Cells[3].Value = GetIskontoOran(item.AllowanceCharge);//Iskonto Oran
                    row.Cells[4].Value = GetIskontoTutar(item.AllowanceCharge);//Iskonto Tutar
                    row.Cells[5].Value = GetKdvOran(item.TaxTotal);//KDV Oran
                    row.Cells[6].Value = GetKdvTutar(item.TaxTotal);//KDV Tutar
                    row.Cells[7].Value = item.LineExtensionAmount.Value;//Mal Hizmet Tutarı

                    row.Tag = item;
                    dataGridView1.Rows.Add(row);

                }
                dataGridView1.Visible = true;
            }
        }


        private static decimal GetKdvOran(TaxTotalType TaxTotal)
        {
            if (TaxTotal != null)
            {
                return TaxTotal.TaxSubtotal[0].Percent.Value;
            }
            else
            {
                return 0;
            }
        }

        private static decimal GetKdvTutar(TaxTotalType TaxTotal)
        {
            if (TaxTotal != null)
            {
                return TaxTotal.TaxAmount.Value;
            }
            else
            {
                return 0;
            }
        }

        private static decimal GetIskontoOran(AllowanceChargeType[] allowanceCharge)
        {
            if ((allowanceCharge != null) && (allowanceCharge.Length > 0))
            {
                return allowanceCharge[0].MultiplierFactorNumeric.Value;
            }
            else
            {
                return 0;
            }
        }


        private static decimal GetIskontoTutar(AllowanceChargeType[] allowanceCharge)
        {
            if ((allowanceCharge != null) && (allowanceCharge.Length > 0))
            {
                return allowanceCharge[0].Amount.Value;
            }
            else
            {
                return 0;
            }
        }


        public FrmInvoiceCreate()
        {
            InitializeComponent();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.DoubleClick += dataGridView1_DoubleClick;

            AddColumn(dataGridView1, "Aciklama", "Aciklama", 0);
            AddColumn(dataGridView1, "Miktar", "Miktar", 80);
            AddColumn(dataGridView1, "BirimFiyat", "BirimFiyat", 80);
            AddColumn(dataGridView1, "IskontoOran", "IskontoOran", 80);
            AddColumn(dataGridView1, "IskontoTutar", "IskontoTutar", 80);
            AddColumn(dataGridView1, "KdvOran", "KdvOran", 80);
            AddColumn(dataGridView1, "KdvTutar", "KdvTutar", 80);
            AddColumn(dataGridView1, "MalHizmetTutar", "MalHizmetTutar", 80);

        }

        private int AddColumn(DataGridView dataGridView, string name, string text, int width)
        {
            int r = 0;
            r = dataGridView.Columns.Add(name, text);
            if (width > 0)
            {
                dataGridView.Columns[r].Width = width;
            }

            return r;
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





        private void btnGonder_Click(object sender, EventArgs e)
        {


            GIBInterface.SendParameters prm = new GIBInterface.SendParameters();
            prm.InvoicesInfo = new List<GIBInterface.InvoiceInfo>();

            var User = EFatura.MukellefBilgisi(txbVKN.Text);

            if (User == null)
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
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    txbVKN.Text = frm.SelectedUser.Identifier;
                }
            }

        }


        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                if (row != null)
                {
                    var r = (row.Tag as InvoiceLineType);
                    using (FrmInvoiceLineViewer frm = new FrmInvoiceLineViewer())
                    {
                        frm.InvoiceLine = r;
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            
                            GridDoldur(invoice);
                        }
                    }
                }
            }
        }
        private void btnYeniSatir_Click(object sender, EventArgs e)
        {
            using (FrmInvoiceLineViewer frm = new FrmInvoiceLineViewer())
            {
                frm.InvoiceLine = null;
                List<InvoiceLineType> l = new List<InvoiceLineType>();
                l.AddRange(invoice.InvoiceLine);
                if (frm.ShowDialog() == DialogResult.OK)
                {

                    l.Add(frm.InvoiceLine);
                    invoice.InvoiceLine = l.ToArray();

                    //Sıra Numarası veriyoruz 
                    int i = 0;
                    foreach (var item in invoice.InvoiceLine)
                    {
                        i++;
                        item.ID = new IDType();
                        item.ID.Value = i.ToString();
                    }

                    GridDoldur(invoice);
                }
            }
        }
    }
}
