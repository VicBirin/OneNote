namespace OneNote.Sample.Api
{
    public interface INotebookElement
    {
        string Id { get; }
        string DisplayName { get; set; }
    }
}
