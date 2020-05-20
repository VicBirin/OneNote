using System;
using System.Linq;

namespace OneNote.Sample.Api.Convertors
{
    public class GraphDocumentConverter : IDocumentConverter<Microsoft.Graph.OnenoteSection>
    {
        public Document ConvertToLocal(Microsoft.Graph.OnenoteSection src, Notebook parentNotebook)
        {
            var doc = new Document();
            if (src == null)
            {
                return doc;
            }

            var pageConvertor = new GraphPageConverter();

            doc.Id = src.Id;
            doc.DisplayName = src.DisplayName;
            doc.IsDefault = src.IsDefault;
            doc.ParentNotebook = parentNotebook;
            if (src.Pages != null)
            {
                Array.ForEach(src.Pages.Select(p => pageConvertor.ConvertToLocal(p, parentNotebook, doc)).ToArray(), doc.AddChildElement);
            }

            return doc;
        }

        public Microsoft.Graph.OnenoteSection ConvertToOneNote(Document src)
        {
            var dest = new Microsoft.Graph.OnenoteSection();
            if (src == null)
            {
                return dest;
            }

            var pageConvertor = new GraphPageConverter();

            dest.Id = src.Id;
            dest.DisplayName = src.DisplayName;
            dest.IsDefault = src.IsDefault;

            dest.Pages = new Microsoft.Graph.OnenoteSectionPagesCollectionPage();
            foreach (var p in src)
            {
                dest.Pages.Add(pageConvertor.ConvertToOneNote(p));
            }

            return dest;
        }
    }
}