using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GIBInterface;
using Newtonsoft.Json;

namespace SampleApp
{
    public partial class FrmProviderSelect : Form
    {
        public GIBFramework.EFatura EFatura { get; set; }


        private IEFatura AktiveProvider { get; set; }

        public FrmProviderSelect()
        {
            InitializeComponent();
        }

        private void FrmProviderSelect_Shown(object sender, EventArgs e)
        {
            cbProviders.Items.Add("Uyumsoft");
            cbProviders.Items.Add("Veriban");
            cbProviders.Items.Add("Logo");
            cbProviders.Items.Add("EFinans");
        }

        private void cbProviders_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbProviders.SelectedIndex >= 0)
            {
                if (cbProviders.SelectedItem != null)
                {
                    if (cbProviders.SelectedItem.ToString() == "Uyumsoft")
                    {
                        AktiveProvider = new GIBProviders.Uyumsoft.EFatura();
                    }
                    if (cbProviders.SelectedItem.ToString() == "Veriban")
                    {
                        AktiveProvider = new GIBProviders.Veriban.EFatura();
                    }
                    if (cbProviders.SelectedItem.ToString() == "Logo")
                    {
                        //TODO : Logo create edilecek
                        AktiveProvider = null;
                    }
                    if (cbProviders.SelectedItem.ToString() == "EFinans")
                    {
                        //TODO : EFinans create edilecek
                        AktiveProvider = null;
                    }


                    EFatura = new GIBFramework.EFatura(AktiveProvider);

                    if (System.IO.File.Exists(GetSettingsFileName()))
                    {
                        tbSettings.Text = System.IO.File.ReadAllText(GetSettingsFileName());
                    }
                    else
                    {
                        tbSettings.Text = EFatura.SettingsJson;
                    }
                }
            }
        }

        private void btnTamam_Click(object sender, EventArgs e)
        {
            if (cbProviders.SelectedItem != null)
            {
                EFatura.SettingsJson = tbSettings.Text;
                System.IO.File.WriteAllText(GetSettingsFileName(), tbSettings.Text, UTF8Encoding.UTF8);
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Lütfen Entegratör seçiniz");
            }
        }

        private string GetSettingsFileName()
        {
            return "Settings_" + cbProviders.Text + ".json";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tbSettings.Text = EFatura.DefaultSettingsJson();
        }
    }
}
