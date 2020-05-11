namespace OneNote.Sample.Api
{
    public interface IPageConverter<T> : IOneNoteConverter<T, Page>
    {
        Page ConvertToLocal(T src, Notebook parentNotebook, Document parentDocument);
    }
}