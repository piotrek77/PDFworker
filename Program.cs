using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFworker
{
    class Program
    {



        static void ShowHelp()
        {
            Console.WriteLine("obracanie stron\nPDFworker /r sourcefile.pdf destinationfile.pdf startPage endPage");
            Console.WriteLine("kopiowanie stron\nPDFworker /c sourcefile.pdf destinationfile.pdf startPage endPage");
        }
        static void Main(string[] args)
        {

            if (args.Length==0)
            {
                ShowHelp();
                Environment.Exit(0);
            }

            int startPage = 0;
            int endPage = 0;

            switch (args[0])
            {
                case "/r":
                case "-r":


                    if (args.Length<5)
                    {
                        ShowHelp();
                        break;
                    }

                    startPage = Convert.ToInt32(args[3]);
                    endPage = Convert.ToInt32(args[4]);
                
                    PDFworkerClass.RotatePages(args[1], args[2], startPage, endPage);


                    break;

                case "/c":
                case "-c":

                    if (args.Length < 5)
                    {
                        ShowHelp();
                        break;
                    }

                    startPage = Convert.ToInt32(args[3]);
                    endPage = Convert.ToInt32(args[4]);

                    PDFworkerClass.CopyPages(args[1], args[2], startPage, endPage);
                    break;
                default:
                    ShowHelp();
                    break;

            }








        }

    }
}
