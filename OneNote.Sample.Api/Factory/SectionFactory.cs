using Microsoft.Graph;
using OneNote.Sample.Api.Convertors;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneNote.Sample.Api
{
    /// <summary>
    /// Handles OneNote sections CRUD operations
    /// </summary>
    public class SectionFactory : OneNoteFactory<Document>
    {
        private GraphServiceClient client;
        private GraphDocumentConverter sectionConvertor;

        public SectionFactory()
        {
            client = GraphClientFactory.GetGraphServiceClient();
            sectionConvertor = new GraphDocumentConverter();
        }

        public override Document AddItem(Document item, string parentId)
        {
            var oneNoteNode = sectionConvertor.ConvertToOneNote(item);
            var result = AddItemAsync(oneNoteNode, parentId);
            return sectionConvertor.ConvertToLocal(result.Result, null);
        }

        public override List<Document> GetAllItems(string parentId = null)
        {
            var result = new List<Document>();

            var sections = LoadAllItemsAsync(parentId).Result;
            foreach (var item in sections)
            {
                var section = sectionConvertor.ConvertToLocal(item, null);
                result.Add(section);
            }
            return result;
        }

        public override Document GetItem(string sectionId)
        {
            var item = LoadItemAsync(sectionId).Result;
            var section = sectionConvertor.ConvertToLocal(item, null);
            return section;
        }

        private async Task<OnenoteSection> LoadItemAsync(string sectionId)
        {
            var item = await client.Me.Onenote.Sections[sectionId].Request().GetAsync();
            return item;
        }
        private async Task<OnenoteSection> AddItemAsync(OnenoteSection item, string parentId)
        {
            var result = await client.Me.Onenote.Notebooks[parentId].Sections.Request().AddAsync(item);
            return result;
        }

        private async Task<OnenoteSection[]> LoadAllItemsAsync(string parentId)
        {
            if (string.IsNullOrEmpty(parentId))
            {
                var items = await client.Me.Onenote.Sections.Request().GetAsync();
                return items.ToArray();
            }
            else
            {
                var items = await client.Me.Onenote.Notebooks[parentId].Sections.Request().GetAsync();
                return items.ToArray();
            }
        }

        public override bool DeleteItem(string itemId) => throw new System.NotImplementedException();
    }
}
