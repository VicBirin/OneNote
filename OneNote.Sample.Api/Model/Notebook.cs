using System;
using System.Collections.Generic;

namespace OneNote.Sample.Api
{
    /// <summary>
    /// OneNote notebook model
    /// </summary>
    public class Notebook : INotebookElement
    {
        public string Id { get; internal set; }
        public string DisplayName { get; set; }
        public DateTimeOffset? Created { get; internal set; }
        public string CreatedBy { get; internal set; }
        public bool? IsDefault { get; set; }
        public bool? IsShared { get; set; }
        public string LastModifiedBy { get; internal set; }
        public DateTimeOffset? LastModified { get; internal set; }
        public string Link { get; internal set; }
        public List<Section> Sections { get; internal set; }

        public Notebook()
        {
            Sections = new List<Section>();
        }
    }
}
