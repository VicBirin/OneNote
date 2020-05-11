namespace OneNote.Sample.Api
{
    public interface INotebookConverter<T> : IOneNoteConverter<T, Notebook>
    {
        Notebook ConvertToLocal(T src);
    }
}