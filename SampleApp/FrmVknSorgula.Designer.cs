namespace SampleApp
{
    partial class FrmVknSorgula
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
            this.txbVknTckn = new System.Windows.Forms.TextBox();
            this.txbSonuc = new System.Windows.Forms.TextBox();
            this.lblVkn = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSorgula = new System.Windows.Forms.Button();
            this.tbUnvan = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnUnvanAra = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txbVknTckn
            // 
            this.txbVknTckn.Location = new System.Drawing.Point(15, 28);
            this.txbVknTckn.Name = "txbVknTckn";
            this.txbVknTckn.Size = new System.Drawing.Size(192, 20);
            this.txbVknTckn.TabIndex = 0;
            this.txbVknTckn.Text = "6290456105";
            // 
            // txbSonuc
            // 
            this.txbSonuc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbSonuc.Location = new System.Drawing.Point(12, 102);
            this.txbSonuc.Multiline = true;
            this.txbSonuc.Name = "txbSonuc";
            this.txbSonuc.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txbSonuc.Size = new System.Drawing.Size(706, 324);
            this.txbSonuc.TabIndex = 1;
            this.txbSonuc.WordWrap = false;
            // 
            // lblVkn
            // 
            this.lblVkn.AutoSize = true;
            this.lblVkn.Location = new System.Drawing.Point(12, 12);
            this.lblVkn.Name = "lblVkn";
            this.lblVkn.Size = new System.Drawing.Size(122, 13);
            this.lblVkn.TabIndex = 2;
            this.lblVkn.Text = "VKN veya TCKN giriniz :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Sorgu Sonucu";
            // 
            // btnSorgula
            // 
            this.btnSorgula.Location = new System.Drawing.Point(223, 26);
            this.btnSorgula.Name = "btnSorgula";
            this.btnSorgula.Size = new System.Drawing.Size(107, 23);
            this.btnSorgula.TabIndex = 4;
            this.btnSorgula.Text = "Vergi No Sorgula";
            this.btnSorgula.UseVisualStyleBackColor = true;
            this.btnSorgula.Click += new System.EventHandler(this.btnSorgula_Click);
            // 
            // tbUnvan
            // 
            this.tbUnvan.Location = new System.Drawing.Point(367, 30);
            this.tbUnvan.Name = "tbUnvan";
            this.tbUnvan.Size = new System.Drawing.Size(208, 20);
            this.tbUnvan.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(367, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Ünvan Ara : ";
            // 
            // btnUnvanAra
            // 
            this.btnUnvanAra.Location = new System.Drawing.Point(598, 27);
            this.btnUnvanAra.Name = "btnUnvanAra";
            this.btnUnvanAra.Size = new System.Drawing.Size(120, 23);
            this.btnUnvanAra.TabIndex = 7;
            this.btnUnvanAra.Text = "Unvan Ara";
            this.btnUnvanAra.UseVisualStyleBackColor = true;
            this.btnUnvanAra.Click += new System.EventHandler(this.btnUnvanAra_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(443, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Not : ilk sorgulama yavaş gelecekdir. aynı günde yapılan diğer sorgulamalar çok h" +
    "ızlı olacaktır";
            // 
            // FrmVknSorgula
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 438);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnUnvanAra);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbUnvan);
            this.Controls.Add(this.btnSorgula);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblVkn);
            this.Controls.Add(this.txbSonuc);
            this.Controls.Add(this.txbVknTckn);
            this.Name = "FrmVknSorgula";
            this.Text = "FrmVknSorgula";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txbVknTckn;
        private System.Windows.Forms.TextBox txbSonuc;
        private System.Windows.Forms.Label lblVkn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSorgula;
        private System.Windows.Forms.TextBox tbUnvan;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnUnvanAra;
        private System.Windows.Forms.Label label3;
    }
}