using System;
using System.Windows.Forms;

namespace PdfOcrSplitter
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnSelectPdf_Click(object sender, EventArgs e)
        {
            using OpenFileDialog dlg = new OpenFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf",
                Title = "PDFファイルを選択"
            };

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtPdfPath.Text = dlg.FileName;
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            string pdfPath = txtPdfPath.Text;
            if (string.IsNullOrWhiteSpace(pdfPath) || !System.IO.File.Exists(pdfPath))
            {
                MessageBox.Show("有効なPDFファイルを選択してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var processor = new PdfProcessor();
            string outputDir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output");
            string result = processor.Process(pdfPath, outputDir);

            MessageBox.Show(result, "処理結果", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
