using GIBInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SampleApp
{
    public partial class FrmMain : Form
    {

        private GIBFramework.EFatura EFatura { get; set;}

        public FrmMain()
        {
            InitializeComponent();
        }
        private void FrmMain_Load(object sender, EventArgs e)
        {
            FrmProviderSelect frm = new FrmProviderSelect();
            frm.EFatura = EFatura;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                EFatura = frm.EFatura;
              
            }
            else
            {
                Close();
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
    }
}
