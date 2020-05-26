using GIBFramework;
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
    public partial class FrmMukellefAra : Form
    {

        public EFatura EFatura { get; internal set; }
        public GIBInterface.EFaturaPaketi.User SelectedUser
        {
            get
            {
                GIBInterface.EFaturaPaketi.User r = null;
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DataGridViewRow row = dataGridView1.SelectedRows[0];
                    if (row != null)
                    {
                        r = (row.Tag as GIBInterface.EFaturaPaketi.User);
                    }
                }
                return r;
            }

        }
        public FrmMukellefAra()
        {
            InitializeComponent();
            btnTamam.Enabled = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.DoubleClick += dataGridView1_DoubleClick;


            dataGridView1.Columns.Add("VKN", "VKN");//0
            dataGridView1.Columns.Add("Unvan", "Unvan");//1

            dataGridView1.Columns[0].Width = 130;
        }



        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            DialogResultOK();

        }

        private void btnTamam_Click(object sender, EventArgs e)
        {
            DialogResultOK();

        }

        private void DialogResultOK()
        {
            if (SelectedUser != null)
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnUnvanAra_Click(object sender, EventArgs e)
        {
            Ara();
        }

        private void Ara()
        {

            var UserList = EFatura.MukellefAra(tbUnvan.Text);

            dataGridView1.Visible = false;
            dataGridView1.Rows.Clear();
            foreach (var item in UserList)
            {
                //txbSonuc.AppendText(item.Identifier + "\t" + item.Title + "\r\n");
                var row = new DataGridViewRow();
                row.CreateCells(dataGridView1);
                row.Cells[0].Value = item.Identifier;
                row.Cells[1].Value = item.Title;
                row.Tag = item;
                dataGridView1.Rows.Add(row);

            }
            dataGridView1.Visible = true;
            if (UserList.Count > 0)
            {
                dataGridView1.Focus();
            }

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (SelectedUser != null)
            {
                btnTamam.Enabled = true;
            }
            else
            {
                btnTamam.Enabled = false;
            }
        }

        private void tbUnvan_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Ara();
            }
        }

        private void dataGridView1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                DialogResultOK();
            }

        }
    }
}
