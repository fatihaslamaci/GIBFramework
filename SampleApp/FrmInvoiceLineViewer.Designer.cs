namespace SampleApp
{
    partial class FrmInvoiceLineViewer
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnIptal = new System.Windows.Forms.Button();
            this.btnTamam = new System.Windows.Forms.Button();
            this.txtIskontoTutar = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.txtMiktar = new System.Windows.Forms.TextBox();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.txtMarka = new System.Windows.Forms.TextBox();
            this.txtIskontoOrani = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtAliciKodu = new System.Windows.Forms.TextBox();
            this.txtSaticiKodu = new System.Windows.Forms.TextBox();
            this.txtUreticiKodu = new System.Windows.Forms.TextBox();
            this.txtUrunAdi = new System.Windows.Forms.TextBox();
            this.label47 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.txtSatirNotu = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txtBirim = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.txtFiyat = new System.Windows.Forms.TextBox();
            this.txtAciklama = new System.Windows.Forms.TextBox();
            this.txtToplamTutar = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.txtKdvTutar = new System.Windows.Forms.TextBox();
            this.txtKdvOrani = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnIptal);
            this.panel1.Controls.Add(this.btnTamam);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 303);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(708, 43);
            this.panel1.TabIndex = 0;
            // 
            // btnIptal
            // 
            this.btnIptal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIptal.Location = new System.Drawing.Point(626, 8);
            this.btnIptal.Name = "btnIptal";
            this.btnIptal.Size = new System.Drawing.Size(75, 23);
            this.btnIptal.TabIndex = 3;
            this.btnIptal.Text = "Iptal";
            this.btnIptal.UseVisualStyleBackColor = true;
            this.btnIptal.Click += new System.EventHandler(this.btnIptal_Click);
            // 
            // btnTamam
            // 
            this.btnTamam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTamam.Location = new System.Drawing.Point(536, 8);
            this.btnTamam.Name = "btnTamam";
            this.btnTamam.Size = new System.Drawing.Size(75, 23);
            this.btnTamam.TabIndex = 2;
            this.btnTamam.Text = "Tamam";
            this.btnTamam.UseVisualStyleBackColor = true;
            // 
            // txtIskontoTutar
            // 
            this.txtIskontoTutar.Location = new System.Drawing.Point(359, 129);
            this.txtIskontoTutar.Name = "txtIskontoTutar";
            this.txtIskontoTutar.Size = new System.Drawing.Size(80, 20);
            this.txtIskontoTutar.TabIndex = 139;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(12, 113);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(36, 13);
            this.label20.TabIndex = 130;
            this.label20.Text = "Miktar";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(184, 113);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(29, 13);
            this.label36.TabIndex = 126;
            this.label36.Text = "Fiyat";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(12, 60);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(50, 13);
            this.label21.TabIndex = 128;
            this.label21.Text = "Açıklama";
            // 
            // txtMiktar
            // 
            this.txtMiktar.Location = new System.Drawing.Point(15, 129);
            this.txtMiktar.Name = "txtMiktar";
            this.txtMiktar.Size = new System.Drawing.Size(80, 20);
            this.txtMiktar.TabIndex = 133;
            // 
            // txtModel
            // 
            this.txtModel.Location = new System.Drawing.Point(506, 25);
            this.txtModel.Name = "txtModel";
            this.txtModel.Size = new System.Drawing.Size(191, 20);
            this.txtModel.TabIndex = 129;
            this.txtModel.Text = "Model Adi";
            // 
            // txtMarka
            // 
            this.txtMarka.Location = new System.Drawing.Point(259, 25);
            this.txtMarka.Name = "txtMarka";
            this.txtMarka.Size = new System.Drawing.Size(240, 20);
            this.txtMarka.TabIndex = 132;
            this.txtMarka.Text = "Marka";
            // 
            // txtIskontoOrani
            // 
            this.txtIskontoOrani.Location = new System.Drawing.Point(273, 129);
            this.txtIskontoOrani.Name = "txtIskontoOrani";
            this.txtIskontoOrani.Size = new System.Drawing.Size(80, 20);
            this.txtIskontoOrani.TabIndex = 138;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(256, 9);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(37, 13);
            this.label22.TabIndex = 127;
            this.label22.Text = "Marka";
            // 
            // txtAliciKodu
            // 
            this.txtAliciKodu.Location = new System.Drawing.Point(506, 184);
            this.txtAliciKodu.Name = "txtAliciKodu";
            this.txtAliciKodu.Size = new System.Drawing.Size(191, 20);
            this.txtAliciKodu.TabIndex = 145;
            this.txtAliciKodu.Text = "Alıcı Kodu";
            // 
            // txtSaticiKodu
            // 
            this.txtSaticiKodu.Location = new System.Drawing.Point(259, 184);
            this.txtSaticiKodu.Name = "txtSaticiKodu";
            this.txtSaticiKodu.Size = new System.Drawing.Size(240, 20);
            this.txtSaticiKodu.TabIndex = 144;
            this.txtSaticiKodu.Text = "Satıcı Kodu";
            // 
            // txtUreticiKodu
            // 
            this.txtUreticiKodu.Location = new System.Drawing.Point(15, 184);
            this.txtUreticiKodu.Name = "txtUreticiKodu";
            this.txtUreticiKodu.Size = new System.Drawing.Size(238, 20);
            this.txtUreticiKodu.TabIndex = 143;
            this.txtUreticiKodu.Text = "Üretici Kodu";
            // 
            // txtUrunAdi
            // 
            this.txtUrunAdi.Location = new System.Drawing.Point(15, 25);
            this.txtUrunAdi.Name = "txtUrunAdi";
            this.txtUrunAdi.Size = new System.Drawing.Size(238, 20);
            this.txtUrunAdi.TabIndex = 131;
            this.txtUrunAdi.Text = "Ürün Adı";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(503, 168);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(54, 13);
            this.label47.TabIndex = 124;
            this.label47.Text = "Alıcı Kodu";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(256, 168);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(61, 13);
            this.label45.TabIndex = 123;
            this.label45.Text = "Satıcı Kodu";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(12, 168);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(65, 13);
            this.label44.TabIndex = 114;
            this.label44.Text = "Üretici Kodu";
            // 
            // txtSatirNotu
            // 
            this.txtSatirNotu.Location = new System.Drawing.Point(359, 76);
            this.txtSatirNotu.Name = "txtSatirNotu";
            this.txtSatirNotu.Size = new System.Drawing.Size(338, 20);
            this.txtSatirNotu.TabIndex = 137;
            this.txtSatirNotu.Text = "Satır Notu";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(12, 9);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(48, 13);
            this.label23.TabIndex = 121;
            this.label23.Text = "Ürün Adı";
            // 
            // txtBirim
            // 
            this.txtBirim.Location = new System.Drawing.Point(101, 129);
            this.txtBirim.Name = "txtBirim";
            this.txtBirim.Size = new System.Drawing.Size(80, 20);
            this.txtBirim.TabIndex = 136;
            this.txtBirim.Text = "Adet";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(356, 60);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(54, 13);
            this.label24.TabIndex = 120;
            this.label24.Text = "Satır Notu";
            // 
            // txtFiyat
            // 
            this.txtFiyat.Location = new System.Drawing.Point(187, 129);
            this.txtFiyat.Name = "txtFiyat";
            this.txtFiyat.Size = new System.Drawing.Size(80, 20);
            this.txtFiyat.TabIndex = 135;
            // 
            // txtAciklama
            // 
            this.txtAciklama.Location = new System.Drawing.Point(15, 76);
            this.txtAciklama.Name = "txtAciklama";
            this.txtAciklama.Size = new System.Drawing.Size(338, 20);
            this.txtAciklama.TabIndex = 134;
            this.txtAciklama.Text = "Açıklama";
            // 
            // txtToplamTutar
            // 
            this.txtToplamTutar.Location = new System.Drawing.Point(617, 129);
            this.txtToplamTutar.Name = "txtToplamTutar";
            this.txtToplamTutar.Size = new System.Drawing.Size(80, 20);
            this.txtToplamTutar.TabIndex = 141;
            this.txtToplamTutar.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(503, 9);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(36, 13);
            this.label25.TabIndex = 119;
            this.label25.Text = "Model";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(614, 113);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(42, 13);
            this.label43.TabIndex = 118;
            this.label43.Text = "Toplam";
            // 
            // txtKdvTutar
            // 
            this.txtKdvTutar.Location = new System.Drawing.Point(531, 129);
            this.txtKdvTutar.Name = "txtKdvTutar";
            this.txtKdvTutar.Size = new System.Drawing.Size(80, 20);
            this.txtKdvTutar.TabIndex = 142;
            this.txtKdvTutar.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtKdvOrani
            // 
            this.txtKdvOrani.Location = new System.Drawing.Point(445, 129);
            this.txtKdvOrani.Name = "txtKdvOrani";
            this.txtKdvOrani.Size = new System.Drawing.Size(80, 20);
            this.txtKdvOrani.TabIndex = 140;
            this.txtKdvOrani.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(528, 113);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(57, 13);
            this.label30.TabIndex = 117;
            this.label30.Text = "KDV Tutar";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(98, 113);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(29, 13);
            this.label26.TabIndex = 116;
            this.label26.Text = "Birim";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(443, 113);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(40, 13);
            this.label29.TabIndex = 115;
            this.label29.Text = "KDV %";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(356, 113);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(70, 13);
            this.label31.TabIndex = 122;
            this.label31.Text = "İskonto Tutar";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(270, 113);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(50, 13);
            this.label27.TabIndex = 125;
            this.label27.Text = "İskonto%";
            // 
            // FrmInvoiceLineViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 346);
            this.Controls.Add(this.txtIskontoTutar);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label36);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.txtMiktar);
            this.Controls.Add(this.txtModel);
            this.Controls.Add(this.txtMarka);
            this.Controls.Add(this.txtIskontoOrani);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.txtAliciKodu);
            this.Controls.Add(this.txtSaticiKodu);
            this.Controls.Add(this.txtUreticiKodu);
            this.Controls.Add(this.txtUrunAdi);
            this.Controls.Add(this.label47);
            this.Controls.Add(this.label45);
            this.Controls.Add(this.label44);
            this.Controls.Add(this.txtSatirNotu);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.txtBirim);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.txtFiyat);
            this.Controls.Add(this.txtAciklama);
            this.Controls.Add(this.txtToplamTutar);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.label43);
            this.Controls.Add(this.txtKdvTutar);
            this.Controls.Add(this.txtKdvOrani);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.panel1);
            this.Name = "FrmInvoiceLineViewer";
            this.Text = "FrmInvoiceLineViewer";
            this.Load += new System.EventHandler(this.FrmInvoiceLineViewer_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtIskontoTutar;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtMiktar;
        private System.Windows.Forms.TextBox txtModel;
        private System.Windows.Forms.TextBox txtMarka;
        private System.Windows.Forms.TextBox txtIskontoOrani;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtAliciKodu;
        private System.Windows.Forms.TextBox txtSaticiKodu;
        private System.Windows.Forms.TextBox txtUreticiKodu;
        private System.Windows.Forms.TextBox txtUrunAdi;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.TextBox txtSatirNotu;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtBirim;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txtFiyat;
        private System.Windows.Forms.TextBox txtAciklama;
        private System.Windows.Forms.TextBox txtToplamTutar;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.TextBox txtKdvTutar;
        private System.Windows.Forms.TextBox txtKdvOrani;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Button btnIptal;
        private System.Windows.Forms.Button btnTamam;
    }
}