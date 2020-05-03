namespace OneNote.Sample.Api
{
    public interface IOutlineElement : IElement
    {
        ElementType ElementType { get; }
        string XPath { get; set; }
    }
}