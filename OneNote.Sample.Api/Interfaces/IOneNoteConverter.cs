namespace OneNote.Sample.Api
{
    public interface IOneNoteConverter<T1, T2>
    {
        T1 ConvertToOneNote(T2 src);
    }
}
