using System.Collections.Generic;

namespace OneNote.Sample.Api
{
    /// <summary>
    /// OneNote Section model
    /// </summary>
    public class Document : CompositeElement<Page>, INotebookChildElement
    {
        public string Id { get; internal set; }
        public string DisplayName { get; set; }
        public bool? IsDefault { get; internal set; }
        public Notebook ParentNotebook { get; set; }
        public List<Page> Pages { get; internal set; }

        public Document() : base(ElementType.Document)
        {
            Pages = new List<Page>();
        }
    }
}