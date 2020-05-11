using System.Collections.Generic;
using System.Linq;

namespace OneNote.Sample.Api.Convertors
{
    public class GraphNotebookConverter : INotebookConverter<Microsoft.Graph.Notebook>
    {
        public Notebook ConvertToLocal(Microsoft.Graph.Notebook src)
        {
            var dest = new Notebook();
            if (src == null) return dest;

            var sectionConvertor = new GraphDocumentConverter();

            dest.Id = src.Id;
            dest.DisplayName = src.DisplayName;
            dest.Created = src.CreatedDateTime;
            dest.CreatedBy = src.CreatedBy.User.DisplayName;
            dest.IsDefault = src.IsDefault;
            dest.IsShared = src.IsShared;
            dest.LastModifiedBy = src.LastModifiedBy.User.DisplayName;
            dest.LastModified = src.LastModifiedDateTime;
            dest.Link = src.Links.OneNoteWebUrl.Href;
            dest.Sections = src.Sections == null ? new List<Document>() : src.Sections.Select(s => sectionConvertor.ConvertToLocal(s, dest)).ToList();

            return dest;
        }

        public Microsoft.Graph.Notebook ConvertToOneNote(Notebook src)
        {
            var sectionConvertor = new GraphDocumentConverter();

            Microsoft.Graph.Notebook dest = new Microsoft.Graph.Notebook
            {
                Id = src.Id,
                DisplayName = src.DisplayName,
                IsDefault = src.IsDefault,
                IsShared = src.IsShared,
            };

            dest.SectionGroups = new Microsoft.Graph.NotebookSectionGroupsCollectionPage();

            dest.Sections = new Microsoft.Graph.NotebookSectionsCollectionPage();
            foreach (var s in src.Sections)
            {
                dest.Sections.Add(sectionConvertor.ConvertToOneNote(s));
            }

            return dest;
        }
    }
}