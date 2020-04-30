using System.Collections.Generic;
using System.Linq;

namespace OneNote.Sample.Api.Convertors
{
    public class SectionConvertor
    {
        public Section ConvertToLocal(Microsoft.Graph.OnenoteSection src, Notebook parentNotebook)
        {
            var dest = new Section();
            if (src == null) return dest;

            var pageConvertor = new PageConvertor();

            dest.Id = src.Id;
            dest.DisplayName = src.DisplayName;
            dest.IsDefault = src.IsDefault;
            dest.ParentNotebook = parentNotebook;
            dest.Pages = src.Pages == null ? new List<Page>() : src.Pages.Select(p => pageConvertor.ConvertToLocal(p, parentNotebook, dest)).ToList();

            return dest;
        }

        public Microsoft.Graph.OnenoteSection ConvertToOneNote(Section src)
        {
            var dest = new Microsoft.Graph.OnenoteSection();
            if (src == null) return dest;

            var pageConvertor = new PageConvertor();

            dest.Id = src.Id;
            dest.DisplayName = src.DisplayName;
            dest.IsDefault = src.IsDefault;

            dest.Pages = new Microsoft.Graph.OnenoteSectionPagesCollectionPage();
            foreach (var p in src.Pages)
            {
                dest.Pages.Add(pageConvertor.ConvertToOneNote(p));
            }

            return dest;
        }
    }
}