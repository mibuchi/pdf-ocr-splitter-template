using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Tesseract;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace PdfOcrSplitterWpf
{
    public class PdfProcessor
    {
        public string Process(string pdfPath, string outputDir)
        {
            Directory.CreateDirectory(outputDir);
            var dennoPages = new Dictionary<string, List<int>>();

            using (var document = PdfDocument.Open(pdfPath))
            {
                int pageIndex = 1;
                foreach (Page page in document.GetPages())
                {
                    string ocrText = PerformOcr(page);

                    Match m = Regex.Match(ocrText, @"DEN\d{8}");
                    if (m.Success)
                    {
                        string denno = m.Value;
                        if (!dennoPages.ContainsKey(denno))
                        {
                            dennoPages[denno] = new List<int>();
                        }
                        dennoPages[denno].Add(pageIndex);
                    }
                    pageIndex++;
                }
            }

            foreach (var pair in dennoPages)
            {
                string denno = pair.Key;
                List<int> pages = pair.Value;

                string outputPdfPath = Path.Combine(outputDir, $"{denno}.pdf");

                using (var doc = PdfReader.Open(pdfPath, PdfDocumentOpenMode.Import))
                using (var outDoc = new PdfDocument())
                {
                    foreach (int page in pages)
                    {
                        outDoc.AddPage(doc.Pages[page - 1]);
                    }
                    outDoc.Save(outputPdfPath);
                }
            }

            return $"処理完了: {dennoPages.Count}件の伝票を分割";
        }

        private string PerformOcr(Page page)
        {
            var image = page.Render(300, 300);
            using (var engine = new TesseractEngine(@"./tessdata", "jpn", EngineMode.Default))
            using (var pix = PixConverter.ToPix(image))
            using (var pageResult = engine.Process(pix))
            {
                return pageResult.GetText();
            }
        }
    }
}
