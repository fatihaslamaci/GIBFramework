namespace SampleApp
{
    partial class FrmInvoiceViewer
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
            this.btnXsltAdding = new System.Windows.Forms.Button();
            this.cbXsltFileName = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageHTML = new System.Windows.Forms.TabPage();
            this.webBrowserHTML = new System.Windows.Forms.WebBrowser();
            this.tabPageXML = new System.Windows.Forms.TabPage();
            this.webBrowserXML = new System.Windows.Forms.WebBrowser();
            this.tabPageTxt = new System.Windows.Forms.TabPage();
            this.txbUBLText = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnOk = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageHTML.SuspendLayout();
            this.tabPageXML.SuspendLayout();
            this.tabPageTxt.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnXsltAdding);
            this.panel1.Controls.Add(this.cbXsltFileName);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 54);
            this.panel1.TabIndex = 0;
            // 
            // btnXsltAdding
            // 
            this.btnXsltAdding.Location = new System.Drawing.Point(226, 25);
            this.btnXsltAdding.Name = "btnXsltAdding";
            this.btnXsltAdding.Size = new System.Drawing.Size(75, 23);
            this.btnXsltAdding.TabIndex = 6;
            this.btnXsltAdding.Text = "Görsel Ekle";
            this.btnXsltAdding.UseVisualStyleBackColor = true;
            this.btnXsltAdding.Click += new System.EventHandler(this.btnXsltAdding_Click);
            // 
            // cbXsltFileName
            // 
            this.cbXsltFileName.FormattingEnabled = true;
            this.cbXsltFileName.Location = new System.Drawing.Point(12, 27);
            this.cbXsltFileName.Name = "cbXsltFileName";
            this.cbXsltFileName.Size = new System.Drawing.Size(208, 21);
            this.cbXsltFileName.TabIndex = 5;
            this.cbXsltFileName.SelectedIndexChanged += new System.EventHandler(this.cbXsltFileName_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(211, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "XSLT dosyası (Gömülü xslt yok ise kullanılır)";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageHTML);
            this.tabControl1.Controls.Add(this.tabPageXML);
            this.tabControl1.Controls.Add(this.tabPageTxt);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 78);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 372);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPageHTML
            // 
            this.tabPageHTML.Controls.Add(this.webBrowserHTML);
            this.tabPageHTML.Location = new System.Drawing.Point(4, 22);
            this.tabPageHTML.Name = "tabPageHTML";
            this.tabPageHTML.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageHTML.Size = new System.Drawing.Size(792, 346);
            this.tabPageHTML.TabIndex = 0;
            this.tabPageHTML.Text = "HTML";
            this.tabPageHTML.UseVisualStyleBackColor = true;
            // 
            // webBrowserHTML
            // 
            this.webBrowserHTML.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowserHTML.Location = new System.Drawing.Point(0, 6);
            this.webBrowserHTML.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserHTML.Name = "webBrowserHTML";
            this.webBrowserHTML.ScriptErrorsSuppressed = true;
            this.webBrowserHTML.Size = new System.Drawing.Size(786, 292);
            this.webBrowserHTML.TabIndex = 1;
            // 
            // tabPageXML
            // 
            this.tabPageXML.Controls.Add(this.webBrowserXML);
            this.tabPageXML.Location = new System.Drawing.Point(4, 22);
            this.tabPageXML.Name = "tabPageXML";
            this.tabPageXML.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageXML.Size = new System.Drawing.Size(792, 346);
            this.tabPageXML.TabIndex = 1;
            this.tabPageXML.Text = "XML";
            this.tabPageXML.UseVisualStyleBackColor = true;
            // 
            // webBrowserXML
            // 
            this.webBrowserXML.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowserXML.Location = new System.Drawing.Point(3, 6);
            this.webBrowserXML.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserXML.Name = "webBrowserXML";
            this.webBrowserXML.ScriptErrorsSuppressed = true;
            this.webBrowserXML.Size = new System.Drawing.Size(786, 292);
            this.webBrowserXML.TabIndex = 2;
            // 
            // tabPageTxt
            // 
            this.tabPageTxt.Controls.Add(this.txbUBLText);
            this.tabPageTxt.Location = new System.Drawing.Point(4, 22);
            this.tabPageTxt.Name = "tabPageTxt";
            this.tabPageTxt.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTxt.Size = new System.Drawing.Size(792, 346);
            this.tabPageTxt.TabIndex = 2;
            this.tabPageTxt.Text = "TXT";
            this.tabPageTxt.UseVisualStyleBackColor = true;
            // 
            // txbUBLText
            // 
            this.txbUBLText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbUBLText.Location = new System.Drawing.Point(8, 6);
            this.txbUBLText.Multiline = true;
            this.txbUBLText.Name = "txbUBLText";
            this.txbUBLText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txbUBLText.Size = new System.Drawing.Size(776, 292);
            this.txbUBLText.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnOk);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 404);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 46);
            this.panel2.TabIndex = 3;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(12, 11);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(77, 23);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "Tamam";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // FrmInvoiceViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmInvoiceViewer";
            this.Text = "FrmInvoiceViewer";
            this.Load += new System.EventHandler(this.FrmInvoiceViewer_Load);
            this.Shown += new System.EventHandler(this.FrmInvoiceViewer_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPageHTML.ResumeLayout(false);
            this.tabPageXML.ResumeLayout(false);
            this.tabPageTxt.ResumeLayout(false);
            this.tabPageTxt.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageHTML;
        private System.Windows.Forms.TabPage tabPageXML;
        private System.Windows.Forms.TabPage tabPageTxt;
        private System.Windows.Forms.WebBrowser webBrowserHTML;
        private System.Windows.Forms.ComboBox cbXsltFileName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.WebBrowser webBrowserXML;
        private System.Windows.Forms.TextBox txbUBLText;
        private System.Windows.Forms.Button btnXsltAdding;
    }
}