using Microsoft.Graph;
using OneNote.Sample.Api.Convertors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OneNote.Sample.Api
{

    /// <summary>
    /// Handles Page CRUD operations
    /// </summary>
    public class GraphPageFactory : OneNoteFactory<Page>
    {
        private GraphServiceClient client;
        private GraphPageConverter pageConvertor;

        public GraphPageFactory()
        {
            client = GraphClientFactory.GetGraphServiceClient();
            pageConvertor = new GraphPageConverter();
        }

        public override Page AddItem(Page item, string parentId = null)
        {
            var onenotePage = AddItemAsync(item, parentId).Result;

            // wait for content sync
            var content = LoadPageContent(onenotePage).Result;
            onenotePage.Content = content;
            
            return pageConvertor.ConvertToLocal(onenotePage, null, null);
        }

        public override List<Page> GetAllItems(string parentId = null)
        {
            var result = new List<Page>();

            var pages = LoadAllItemsAsync(parentId).Result;
            foreach (var item in pages)
            {
                var page = pageConvertor.ConvertToLocal(item, null, null);
                result.Add(page);
            }
            return result;
        }

        public override Page GetItem(string pageId)
        {
            var item = LoadItemAsync(pageId).Result;
            var page = pageConvertor.ConvertToLocal(item, null, null);
            return page;
        }


        private async Task<OnenotePage> LoadItemAsync(string pageId)
        {
            var page = await client.Me.Onenote.Pages[pageId].Request().GetAsync();
            var content = await LoadPageContent(page);
            page.Content = content;
            return page;
        }

        private async Task<OnenotePage> AddItemAsync(Page item, string parentId)
        {
            //var content = new MultipartFormDataContent("MyPartBoundary198374");

            //var stringContent = new StringContent(item.Source.ParsedText, Encoding.UTF8, "text/html");
            //content.Add(stringContent, "Presentation");

            //var stream = new MemoryStream();
            //item.Source.Save(stream, Encoding.UTF8);
            //stream.Position = 0;
            //var bytesArr = new byte[stream.Length];
            //stream.Read(bytesArr, 0, bytesArr.Length);

            //var binaryContent = new ByteArrayContent(bytesArr);
            //binaryContent.Headers.Add("Content-Type", "image/jpeg");
            //binaryContent.Headers.Add("Content-Disposition", "form-data; name=\"ImageBody\"");
            //content.Add(binaryContent, "ImageBody");

            // wait for creating page before loading html content
            //var page = await client.Me.Onenote.Sections[parentId].Pages.Request().AddAsync(content);

            //return page;
            return null;
        }

        private async Task<Stream> LoadPageContent(OnenotePage page)
        {
            // load page content
            try
            {
                return await client.Me.Onenote.Pages[page.Id].Content.Request().WithMaxRetry(10).GetAsync();
            }
            catch(Exception ex)
            {
                Thread.Sleep(5000);
                return await client.Me.Onenote.Pages[page.Id].Content.Request().WithMaxRetry(10).GetAsync();
            }
        }

        private async Task<OnenotePage[]> LoadAllItemsAsync(string parentId)
        {
            if (string.IsNullOrEmpty(parentId))
            {
                var items = await client.Me.Onenote.Pages.Request().GetAsync();
                return items.ToArray();
            }
            else
            {
                var items = await client.Me.Onenote.Sections[parentId].Pages.Request().GetAsync();
                return items.ToArray();
            }
        }

        public override bool DeleteItem(string itemId)
        {
            try
            {
                client.Me.Onenote.Pages[itemId].Request().DeleteAsync().GetAwaiter().GetResult();
                return true;
            }
            catch(Exception ex)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Cannot delete page {itemId}: {ex.Message}");
                Console.ResetColor();
                return false;
            }
        }
    }
}
