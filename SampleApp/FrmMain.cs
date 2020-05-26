using System;
using System.Windows.Forms;

namespace SampleApp
{
    public partial class FrmMain : Form
    {

        private GIBFrameworkOld.EFatura EFatura { get; set; }
        private MyDataLayer.MyDataLayer myDataLayer { get; set; }

        public FrmMain()
        {
            InitializeComponent();
        }
        private void FrmMain_Load(object sender, EventArgs e)
        {
            myDataLayer = new MyDataLayer.MyDataLayer();

            using (FrmProviderSelect frm = new FrmProviderSelect())
            {
                frm.EFatura = EFatura;
                frm.dataLayer = myDataLayer;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    EFatura = frm.EFatura;
                }
                else
                {
                    Close();
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            FrmVknSorgula frm = new FrmVknSorgula();
            frm.EFatura = EFatura;
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmInvoiceCreate frm = new FrmInvoiceCreate();
            frm.EFatura = EFatura;
            frm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmFaturaListesi frm = new FrmFaturaListesi();
            frm.EFatura = EFatura;
            frm.ShowDialog();
        }
    }
}
