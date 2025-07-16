using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;

namespace PdfOcrSplitterWpf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSelectPdf_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog { Filter = "PDF files (*.pdf)|*.pdf" };
            if (dialog.ShowDialog() == true)
                txtPdfPath.Text = dialog.FileName;
        }

        private async void btnRun_Click(object sender, RoutedEventArgs e)
        {
            string path = txtPdfPath.Text;
            if (!File.Exists(path))
            {
                MessageBox.Show("PDFファイルを指定してください");
                return;
            }

            txtLog.AppendText("処理開始...\n");

            await Task.Run(() =>
            {
                var processor = new PdfProcessor();
                var log = processor.Process(path, "output");
                Dispatcher.Invoke(() => txtLog.AppendText(log + "\n"));
            });

            txtLog.AppendText("完了しました。\n");
        }
    }
}
