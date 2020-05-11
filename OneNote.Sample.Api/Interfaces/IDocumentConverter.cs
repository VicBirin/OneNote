namespace OneNote.Sample.Api
{
    public interface IDocumentConverter<T> : IOneNoteConverter<T, Document>
    {
        Document ConvertToLocal(T src, Notebook parentNotebook);
    }
}