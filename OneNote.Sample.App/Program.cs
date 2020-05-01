using System;

namespace OneNote.Sample.App
{
    using Api;
    using OneNote.Sample.App.Properties;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class Program
    {
        private static void Main()
        {
            RunTask().Wait();
        }

        /// <summary>
        /// Main program logic
        /// </summary>
        /// <returns></returns>
        private static async Task RunTask()
        {
            Console.WriteLine("Welcome to the OneNote Sample Console Application!\n");

            try
            {
                Thread.Sleep(1000);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Connecting to OneNote and creating sample page ...");
                Console.ResetColor();

                Notebook notebook = OpenOrCreateNotebook();
                Section section = OpenOrCreateSection(notebook);

                var page = OpenOrCreatePage(section);

                // display page info
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Title: " + page.Title);
                Console.WriteLine(new string('-', 50));
                Console.WriteLine("Document Blocks:");
                Console.WriteLine();

                PrintChildElements(page.GetChildElements<OutlineElement>());

                Console.ReadKey();
            }
            catch (ArgumentNullException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message + "\nPlease follow the Readme instructions for configuring this application.");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Request failed with the following message: {0}", ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Error detail: {0}", ex.InnerException.Message);
                }
                Console.ResetColor();
                Console.ReadKey();
                return;
            }
        }

        private static void PrintChildElements(List<OutlineElement> elements)
        {
            foreach (var element in elements)
            {
                Console.WriteLine(element);
                if (element.GetChildElements<OutlineElement>().Count > 0)
                {
                    PrintChildElements(element.GetChildElements<OutlineElement>());
                }
            }
        }

        /// <summary>
        /// Creates OneNote page
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        private static Page OpenOrCreatePage(Section section)
        {
            Console.WriteLine("Loading pages ...");

            string title = "Microsoft Graph API example";

            var pageFactory = new PageFactory();
            var allPages = pageFactory.GetAllItems(section.Id);
            var page = allPages.FirstOrDefault(p => p.Title.Equals(title));
            if (page != null)
            {
                return pageFactory.GetItem(page.Id);
            }


            // create sample html page document and add some fields
            var sampleDoc = new HtmlAgilityPack.HtmlDocument();
            sampleDoc.LoadHtml(Resources.SamplePageHtml);
            var body = sampleDoc.DocumentNode.SelectSingleNode("//body");
            var textField = sampleDoc.CreateElement("div");
            textField.SetAttributeValue("id", "dynamicElement");
            textField.InnerHtml = "Dynamically added DIV element #";
            body.AppendChild(textField);

            //save html document as HTML
            string htmlContent;
            using (var htmlStream = new MemoryStream())
            {
                sampleDoc.Save(htmlStream, Encoding.UTF8);
                htmlStream.Position = 0;

                byte[] buffer = new byte[htmlStream.Length];
                htmlStream.Read(buffer, 0, buffer.Length);
                htmlContent = Encoding.UTF8.GetString(buffer);
            }


            //MemoryStream stream = ReadImage();
            //page = pageFactory.AddItem(new Page { Content = htmlContent, StreamContent = stream, Title = title }, section.Id);

            Console.WriteLine("Created page 'Microsoft Graph API example'...");

            return page;
        }

        /// <summary>
        /// Creates OneNote section
        /// </summary>
        /// <param name="notebook"></param>
        /// <returns></returns>
        private static Section OpenOrCreateSection(Notebook notebook)
        {
            Console.WriteLine("Loading notebook sections ...");
            var sectionFactory = new SectionFactory();
            List<Section> sections = sectionFactory.GetAllItems();
            var section = sections.FirstOrDefault(s => s.DisplayName.Equals("Sample API Section"));
            if (section == null)
            {
                section = sectionFactory.AddItem(new Section { DisplayName = "Sample API Section" }, notebook.Id);
                Console.WriteLine($"Created notebook section: {section.DisplayName}");
            }

            return section;
        }

        /// <summary>
        /// Creates OneNote notebook
        /// </summary>
        /// <returns></returns>
        private static Notebook OpenOrCreateNotebook()
        {
            // get notebooks
            Console.WriteLine("loading notebooks ...");
            var notebookFactory = new NotebookFactory();
            List<Notebook> notebooks = notebookFactory.GetAllItems();
            var notebook = notebooks.FirstOrDefault(n => n.DisplayName == "Sample Notebook");

            // no notebooks, creating new one
            if (notebook == null)
            {
                notebook = notebookFactory.AddItem(new Notebook { DisplayName = "Sample Notebook" });
                Console.WriteLine($"Created notebook: {notebook.DisplayName}");
            }

            return notebook;
        }

        /// <summary>
        /// Read sample page image from resource file
        /// </summary>
        /// <returns></returns>
        private static MemoryStream ReadImage()
        {
            var stream = new MemoryStream();
            Resources.Autumn.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            stream.Position = 0;
            return stream;
        }
    }
}
