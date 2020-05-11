using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneNote.Sample.Api
{
    /// <summary>
    /// Handles uploads to OneDrive
    /// </summary>
    public class GraphDriveFactory : OneNoteFactory<DriveItem>
    {
        private GraphServiceClient client;

        public GraphDriveFactory()
        {
            client = GraphClientFactory.GetGraphServiceClient();
        }

        public override DriveItem AddItem(DriveItem item, string parentId = null)
        {
            var result = AddItemAsync(item, parentId);
            return result.Result;
        }

        public override bool DeleteItem(string itemId) => throw new NotImplementedException();

        public override List<DriveItem> GetAllItems(string parentId = null) => throw new NotImplementedException();

        public override DriveItem GetItem(string id) => throw new NotImplementedException();

        private async Task<DriveItem> AddItemAsync(DriveItem item, string parentId)
        {
            var result = await client.Me.Drive.Items.Request().AddAsync(item);
            return result;
        }
    }
}
