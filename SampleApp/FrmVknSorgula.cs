using System;
using System.Windows.Forms;

namespace SampleApp
{
    public partial class FrmVknSorgula : Form
    {
        public GIBFramework.EFatura EFatura { get; set; }
        public FrmVknSorgula()
        {
            InitializeComponent();
        }

        private void btnSorgula_Click(object sender, EventArgs e)
        {
            var User = EFatura.MukellefBilgisi(txbVknTckn.Text);
            txbSonuc.Text = Newtonsoft.Json.JsonConvert.SerializeObject(User, Newtonsoft.Json.Formatting.Indented);

        }

        private void btnUnvanAra_Click(object sender, EventArgs e)
        {

            if (tbUnvan.Text.Length >= 3)
            {
                var UserList = EFatura.MukellefAra(tbUnvan.Text);

                txbSonuc.Visible = false;
                txbSonuc.Clear();
                foreach (var item in UserList)
                {
                    txbSonuc.AppendText(item.Identifier + "\t" + item.Title + "\r\n");
                }
                txbSonuc.Visible = true;
            }
            else
            {
                MessageBox.Show("Lütfen en az 3 harf giriniz");
            }

        }
    }
}
