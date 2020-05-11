namespace OneNote.Sample.Api
{
    public interface IPageConvertor<T> : IOneNoteConverter<T, Page>
    {
        Page ConvertToLocal(T src, Notebook parentNotebook, Document parentDocument);
    }
}