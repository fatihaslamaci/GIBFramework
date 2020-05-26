using GIBFrameworkOld;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SampleApp
{
    public partial class FrmFaturaListesi : Form
    {
        public EFatura EFatura { get; set; }
        public FrmFaturaListesi()
        {
            InitializeComponent();

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.DoubleClick += dataGridView1_DoubleClick;

            dataGridView1.Columns.Add("id", "id");//0
            dataGridView1.Columns.Add("ETN", "ETN");//1
            dataGridView1.Columns.Add("send_isSucceded", "send_isSucceded");//2
            dataGridView1.Columns.Add("send_Message", "send_Message");//3
            dataGridView1.Columns.Add("send_Error", "send_Error");//4
            dataGridView1.Columns.Add("send_ErrorDetail", "send_ErrorDetail");//5
            dataGridView1.Columns.Add("send_returnETN", "send_returnETN");//6
            dataGridView1.Columns.Add("send_returnFaturaNo", "send_returnFaturaNo");//7
            dataGridView1.Columns.Add("query_Status", "query_Status");//8
            dataGridView1.Columns.Add("query_Message", "query_Message");//9


            dataGridView1.Columns[0].Width = 30;
            dataGridView1.Columns[2].Width = 60;
            dataGridView1.Columns[7].Width = 120;

        }

        private void FrmFaturaListesi_Shown(object sender, EventArgs e)
        {
            GIBInterface.SendInvoiceListDataFind val = new GIBInterface.SendInvoiceListDataFind();
            foreach (var item in EFatura.SendInvoiceList(val))
            {
                var row = new DataGridViewRow();
                row.CreateCells(dataGridView1);
                row.Cells[0].Value = item.Id;
                row.Cells[1].Value = item.ETN;
                row.Cells[2].Value = item.Send_isSucceded;
                row.Cells[3].Value = item.Send_Message;
                row.Cells[4].Value = item.Send_Error;
                row.Cells[5].Value = item.Send_ErrorDetail;
                row.Cells[6].Value = item.Send_returnETN;
                row.Cells[7].Value = item.Send_returnFaturaNo;

                row.Cells[8].Value = item.Query_Status;
                row.Cells[9].Value = item.Query_Message;

                row.Tag = item;
                dataGridView1.Rows.Add(row);
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv != null && dgv.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgv.SelectedRows[0];
                if (row != null || row.Cells[0].Value != null)
                {
                    GIBInterface.SendInvoiceData sendInvoiceData = (GIBInterface.SendInvoiceData)row.Tag;

                    FrmInvoiceViewer frm = new FrmInvoiceViewer();
                    frm.EFatura = EFatura;
                    frm.Invoice = GIBInterface.UBLTR.InvoiceType.Create(sendInvoiceData.InvoiceXML);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                    }
                }
            }
        }

        private void btnDurumSorgula_Click(object sender, EventArgs e)
        {
            var dgv = dataGridView1;
            if (dgv != null && dgv.SelectedRows.Count > 0)
            {


                List<GIBInterface.QueryStatusParameters> val = new List<GIBInterface.QueryStatusParameters>();
                foreach (DataGridViewRow item in dgv.SelectedRows)
                {
                    GIBInterface.SendInvoiceData sendInvoiceData = (GIBInterface.SendInvoiceData)item.Tag;

                    GIBInterface.QueryStatusParameters rr = new GIBInterface.QueryStatusParameters();

                    rr.InvoiceUUID = new Guid(sendInvoiceData.ETN);
                    rr.RecordId = sendInvoiceData.Id;

                    val.Add(rr);
                }

                EFatura.FaturaDurumSorgula(val);

            }
        }
    }
}