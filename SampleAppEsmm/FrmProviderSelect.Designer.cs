namespace SampleAppEsmm
{
    partial class FrmProviderSelect
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.cbProviders = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.btnTamam = new System.Windows.Forms.Button();
            this.tbSettings = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "1) Entegrator Seçiniz";
            // 
            // cbProviders
            // 
            this.cbProviders.FormattingEnabled = true;
            this.cbProviders.Location = new System.Drawing.Point(16, 25);
            this.cbProviders.Name = "cbProviders";
            this.cbProviders.Size = new System.Drawing.Size(198, 21);
            this.cbProviders.TabIndex = 3;
            this.cbProviders.SelectedIndexChanged += new System.EventHandler(this.cbProviders_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(480, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "2) Entegrator Ayarlarını Yapınız (Kullanıcı Adı ve Şifre gibi bilgileri Entegratö" +
    "r Firmadan öğrenebilirsiniz )";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btnTamam);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 429);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(722, 43);
            this.panel1.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(97, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(177, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Varsayılan Ayarlar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnTamam
            // 
            this.btnTamam.Location = new System.Drawing.Point(16, 8);
            this.btnTamam.Name = "btnTamam";
            this.btnTamam.Size = new System.Drawing.Size(75, 23);
            this.btnTamam.TabIndex = 0;
            this.btnTamam.Text = "Tamam";
            this.btnTamam.UseVisualStyleBackColor = true;
            this.btnTamam.Click += new System.EventHandler(this.btnTamam_Click);
            // 
            // tbSettings
            // 
            this.tbSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSettings.Location = new System.Drawing.Point(16, 87);
            this.tbSettings.Multiline = true;
            this.tbSettings.Name = "tbSettings";
            this.tbSettings.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbSettings.Size = new System.Drawing.Size(694, 323);
            this.tbSettings.TabIndex = 8;
            this.tbSettings.WordWrap = false;
            // 
            // FrmProviderSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 472);
            this.Controls.Add(this.tbSettings);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbProviders);
            this.Name = "FrmProviderSelect";
            this.Text = "FrmProviderSelect";
            this.Shown += new System.EventHandler(this.FrmProviderSelect_Shown);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbProviders;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tbSettings;
        private System.Windows.Forms.Button btnTamam;
        private System.Windows.Forms.Button button1;
    }
}