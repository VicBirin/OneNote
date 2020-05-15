using Microsoft.Graph;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace OneNote.Sample.Api
{
    public class GraphResourceFactory : OneNoteFactory<Stream>
    {
        private GraphServiceClient client;

        public GraphResourceFactory()
        {
            client = GraphClientFactory.GetGraphServiceClient();
        }

        public override Stream AddItem(Stream item, string parentId = null)
        {
            throw new System.NotImplementedException();
        }

        public override bool DeleteItem(string itemId)
        {
            throw new System.NotImplementedException();
        }

        public override List<Stream> GetAllItems(string parentId = null)
        {
            throw new System.NotImplementedException();
        }

        public override Stream GetItem(string imageId)
        {
            var item = LoadItemAsync(imageId).Result;
            return item;
        }

        private async Task<Stream> LoadItemAsync(string imageId)
        {
            var image = await client.Me.Onenote.Resources[imageId].Content.Request().GetAsync();
            return image;
        }
    }
}
