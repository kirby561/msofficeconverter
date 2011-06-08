using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Word;

namespace mswordconverter
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length < 2 || args.Length > 3)
            {
                System.Console.WriteLine("Error: no filenames specified");
                System.Console.WriteLine("Usage: mswordconverter inputfile outputfile <saveformat>");
                System.Console.WriteLine("saveformat is optional, will try to autodetect format and fall back to word default if not given.");
                System.Console.WriteLine("can be one of: doc, docx, rtf, txt, html, odt, pdf, xps, xml, unicode");
                System.Console.WriteLine("format support depends on whether support is present in office itself");
                return;
            }
            //read filenames
            object input = args[0];
            string output = args[1];

            //select output format
            object format = WdSaveFormat.wdFormatDocumentDefault;
            string formatString;
            if (args.Length == 3)
            {
                formatString = args[2];
            }
            else
            {
                formatString = output.Substring(output.LastIndexOf(".") + 1);
            }
            if (formatString == "doc")
                format = WdSaveFormat.wdFormatDocument;
            if (formatString == "docx")
                format = WdSaveFormat.wdFormatXMLDocument;
            if (formatString == "rtf")
                format = WdSaveFormat.wdFormatRTF;
            if (formatString == "txt")
                format = WdSaveFormat.wdFormatText;
            if (formatString == "html")
                format = WdSaveFormat.wdFormatHTML;
            if (formatString == "odt")
                format = WdSaveFormat.wdFormatOpenDocumentText;
            if (formatString == "pdf")
                format = WdSaveFormat.wdFormatPDF;
            if (formatString == "xps")
                format = WdSaveFormat.wdFormatXPS;
            if (formatString == "xml")
                format = WdSaveFormat.wdFormatXML;
            if (formatString == "unicode")
                format = WdSaveFormat.wdFormatUnicodeText;
            
            
            Application app;
            try
            {
                app = new Application();
            }
            catch(Exception ex)
            {
                System.Console.WriteLine("Unable to open Microsoft Word");
                System.Console.WriteLine("Error: " + ex.Message);
                return;
            }
            Document doc;
            try
            {
                doc = app.Documents.Open(input, false, true, false);
            }
            catch(Exception ex)
            {
                System.Console.WriteLine("Unable to open file " + input);
                System.Console.WriteLine("Error: " + ex.Message);
                app.Quit(false);
                return;
            }
            if (doc != null)
            {
                try
                {
                    doc.SaveAs2(output, format);
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine("Save to " + output + " failed");
                    System.Console.WriteLine("Error: " + ex.Message);
                }
            }
            else
            {
                System.Console.WriteLine("unable to open file");
            }

            doc.Close(false);
            app.Quit(false);
        }
    }
}
