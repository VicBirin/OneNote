using System.Collections.Generic;

namespace OneNote.Sample.Api
{
    /// <summary>
    /// Base factory class for loading and creation of OneNote objects
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class OneNoteFactory<T>
    {
        public abstract T GetItem(string id);
        public abstract List<T> GetAllItems(string parentId = null);
        public abstract T AddItem(T item, string parentId = null);
        public abstract bool DeleteItem(string itemId);
    }
}  