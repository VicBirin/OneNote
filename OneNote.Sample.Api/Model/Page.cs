using System;
using System.Collections.Generic;

namespace OneNote.Sample.Api
{
    /// <summary>
    /// OneNote Page model
    /// </summary>
    public class Page : CompositeElement<IPageChildElement>
    {
        public Page(ElementType elementType) : base(elementType)
        {
            UserTags = new List<string>();
        }

        public string Id { get; internal set; }
        public int? Level { get; internal set; }
        public string Title { get; set; }
        public int? Order { get; internal set; }
        public DateTimeOffset? LastModifiedDateTime { get; internal set; }
        public IEnumerable<string> UserTags { get; set; }
        public DateTimeOffset? CreatedTime { get; internal set; }
        public Document Document { get; internal set; }
    }
}
