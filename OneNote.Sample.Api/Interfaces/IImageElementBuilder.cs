namespace OneNote.Sample.Api
{
    public interface IImageElementBuilder : IElementBuilder
    {
        void ReadSize();
        void ReadSource();
        void ReadBody();
    }
}
