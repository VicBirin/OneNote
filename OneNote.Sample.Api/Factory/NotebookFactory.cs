using Microsoft.Graph;
using OneNote.Sample.Api.Convertors;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneNote.Sample.Api
{
    /// <summary>
    /// Handles Notebook CRUD operations
    /// </summary>
    public class NotebookFactory : OneNoteFactory<Notebook>
    {
        private GraphServiceClient client;
        private GraphNotebookConverter notebookConvertor;

        public NotebookFactory()
        {
            client = GraphClientFactory.GetGraphServiceClient();
            notebookConvertor = new GraphNotebookConverter();
        }

        /// <summary>
        /// Requests notebook from server byId
        /// </summary>
        /// <param name="notebookId"></param>
        /// <returns></returns>
        public override Notebook GetItem(string notebookId)
        {
            var item = LoadItemAsync(notebookId).Result;
            var notebook = notebookConvertor.ConvertToLocal(item);
            return notebook;
        }

        /// <summary>
        /// Requests all notebooks from server
        /// </summary>
        /// <returns></returns>
        public override List<Notebook> GetAllItems(string parentId = null)
        {
            var result = new List<Notebook>();

            var notebooks = LoadAllItemsAsync().Result;
            foreach (var item in notebooks)
            {
                var notebook = notebookConvertor.ConvertToLocal(item);
                result.Add(notebook);
            }
            return result;
        }

        /// <summary>
        /// Adds a new notebook into collection
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public override Notebook AddItem(Notebook item, string parentId = null)
        {
            var oneNoteNode = notebookConvertor.ConvertToOneNote(item);
            var result = AddItemAsync(oneNoteNode);
            return notebookConvertor.ConvertToLocal(result.Result);
        }

        private async Task<Microsoft.Graph.Notebook> LoadItemAsync(string notebookId)
        {
            var item = await client.Me.Onenote.Notebooks[notebookId].Request().GetAsync();
            return item;
        }
        private async Task<Microsoft.Graph.Notebook> AddItemAsync(Microsoft.Graph.Notebook item)
        {
            var result = await client.Me.Onenote.Notebooks.Request().AddAsync(item);
            return result;
        }
        private async Task<IOnenoteNotebooksCollectionPage> LoadAllItemsAsync()
        {
            var items = await client.Me.Onenote.Notebooks.Request().GetAsync();
            return items;
        }

        public override bool DeleteItem(string itemId) => throw new System.NotImplementedException();
    }
}
