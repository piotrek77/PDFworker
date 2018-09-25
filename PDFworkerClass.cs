using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace PDFworker
{
    class PDFworkerClass
    {




        // źródło kodu
        // https://stackoverflow.com/questions/3579058/rotating-pdf-in-c-sharp-using-itextsharp

        public static void RotatePages(string inputFile, string outputFile,
  int start, int end)
        {
            // get input document
            PdfReader inputPdf = new PdfReader(inputFile);

            // retrieve the total number of pages
            int pageCount = inputPdf.NumberOfPages;

            if (end < start || end > pageCount)
            {
                end = pageCount;
            }

            // load the input document
            Document inputDoc =
                new Document(inputPdf.GetPageSizeWithRotation(1));

            // create the filestream
            using (FileStream fs = new FileStream(outputFile, FileMode.Create))
            {
                // create the output writer
                PdfWriter outputWriter = PdfWriter.GetInstance(inputDoc, fs);
                inputDoc.Open();

                PdfContentByte cb1 = outputWriter.DirectContent;

                // copy pages from input to output document
                for (int i = start; i <= end; i++)
                {
                    inputDoc.SetPageSize(inputPdf.GetPageSizeWithRotation(1));
                    inputDoc.NewPage();

                    PdfImportedPage page =
                        outputWriter.GetImportedPage(inputPdf, i);
                    int rotation = inputPdf.GetPageRotation(i);


                    if (rotation == 90 || rotation == 270)
                    {
                        cb1.AddTemplate(page, 0, -1f, 1f, 0, 0,
                            inputPdf.GetPageSizeWithRotation(i).Height);

                    }
                    else
                    {
                        cb1.AddTemplate(page, -1f, 0, 0, -1f,
                inputPdf.GetPageSizeWithRotation(i).Width,
                inputPdf.GetPageSizeWithRotation(i).Height);
                    }

                }

                inputDoc.Close();
            }
        }



        public static void Test(string inputFile, string outputFile)
        {
            Console.WriteLine("Test");
            String formFile = inputFile;
            String newFile = outputFile;

            PdfReader reader = new PdfReader(formFile);
            PdfDictionary dict = reader.GetPageN(1);
            foreach (var v in dict)
            {
                Console.WriteLine("{0}", v);
            }
            PdfObject obj = dict.GetDirectObject(PdfName.CONTENTS);

            if (obj is PRStream stream)
            {
                byte[] data = PdfReader.GetStreamBytes(stream);

                string dd = System.Text.Encoding.ASCII.GetString(data);

                Console.WriteLine(dd);

                stream.SetData(Encoding.ASCII.GetBytes(dd));

            }

            //PdfStamper stamper = new PdfStamper(reader, new FileStream(newFile, FileMode.Create));

            //AcroFields fields = reader.AcroFields;   //  stamper.AcroFields;



            //foreach(var v in fields.Fields)
            //{
            //    Console.WriteLine("{0}", v);
            //}
            // set form fields

            //fields.SetField("{TO}", "John Doe");

            //fields.SetField("{FROM}", "2 Milky Way, London");

            //stamper.FormFlattening = true;

            //stamper.Close();

            reader.Close();
        }


        public static void CopyPages(string inputFile, string outputFile,
  int start, int end)
        {
            PdfReader inputPdf = new PdfReader(inputFile);
            PdfReader.unethicalreading = true;
            // retrieve the total number of pages
            int pageCount = inputPdf.NumberOfPages;

            if (end < start || end > pageCount)
            {
                end = pageCount;
            }


            using (var output = new MemoryStream())
            {
                var document = new Document();
                var writer = new PdfCopy(document, output);
                document.Open();
                foreach (var file in new[] { inputFile })
                {
                    var reader = new PdfReader(file);
                    PdfReader.unethicalreading = true;
                    PdfImportedPage page;
                    for (int p = start; p <= end; p++)
                    {
                        page = writer.GetImportedPage(reader, p);
                        writer.AddPage(page);
                    }
                }
                document.Close();
                File.WriteAllBytes(outputFile, output.ToArray());
            }


        }









    }




    
    
















}
