namespace PdfOcrSplitter
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtPdfPath;
        private System.Windows.Forms.Button btnSelectPdf;
        private System.Windows.Forms.Button btnRun;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtPdfPath = new System.Windows.Forms.TextBox();
            this.btnSelectPdf = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtPdfPath
            // 
            this.txtPdfPath.Location = new System.Drawing.Point(12, 15);
            this.txtPdfPath.Name = "txtPdfPath";
            this.txtPdfPath.Size = new System.Drawing.Size(360, 23);
            this.txtPdfPath.TabIndex = 0;
            // 
            // btnSelectPdf
            // 
            this.btnSelectPdf.Location = new System.Drawing.Point(378, 14);
            this.btnSelectPdf.Name = "btnSelectPdf";
            this.btnSelectPdf.Size = new System.Drawing.Size(90, 25);
            this.btnSelectPdf.TabIndex = 1;
            this.btnSelectPdf.Text = "PDF選択";
            this.btnSelectPdf.UseVisualStyleBackColor = true;
            this.btnSelectPdf.Click += new System.EventHandler(this.btnSelectPdf_Click);
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(12, 50);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(456, 30);
            this.btnRun.TabIndex = 2;
            this.btnRun.Text = "処理実行";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(480, 92);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.btnSelectPdf);
            this.Controls.Add(this.txtPdfPath);
            this.Name = "MainForm";
            this.Text = "PDF OCR Splitter";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
