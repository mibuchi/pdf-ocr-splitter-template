import fitz  # PyMuPDF
import pytesseract
import os
import re
import csv
import psycopg2
from PIL import Image
from io import BytesIO

pytesseract.pytesseract.tesseract_cmd = r'C:\Program Files\Tesseract-OCR\tesseract.exe'

def ocr_page_text(page):
    pix = page.get_pixmap(dpi=300)
    img = Image.open(BytesIO(pix.tobytes("png")))
    return pytesseract.image_to_string(img, lang='jpn')

def process_pdf(pdf_path, output_dir):
    os.makedirs(output_dir, exist_ok=True)
    doc = fitz.open(pdf_path)
    denno_pages = {}

    for i, page in enumerate(doc):
        text = ocr_page_text(page)
        match = re.search(r'DEN\d{8}', text)
        if match:
            denno = match.group()
            denno_pages.setdefault(denno, []).append(i)

    for denno, pages in denno_pages.items():
        new_doc = fitz.open()
        for i in pages:
            new_doc.insert_pdf(doc, from_page=i, to_page=i)
        new_doc.save(os.path.join(output_dir, f"{denno}.pdf"))

    with open(os.path.join(output_dir, "data.csv"), "w", newline='', encoding="utf-8") as f:
        writer = csv.writer(f)
        writer.writerow(["denno", "content"])

        conn = psycopg2.connect("host=localhost dbname=your_db user=your_user password=your_password")
        cur = conn.cursor()

        for denno, pages in denno_pages.items():
            content = ""
            for i in pages:
                content += ocr_page_text(doc[i])
            writer.writerow([denno, content])

            cur.execute("INSERT INTO orders (denno, content) VALUES (%s, %s)", (denno, content))

        conn.commit()
        cur.close()
        conn.close()

if __name__ == "__main__":
    process_pdf("input/orders.pdf", "output")
