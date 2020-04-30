namespace OneNote.Sample.Api
{
    public interface IElement
    {
        IElement NextSibling { get; set; }
        IElement PreviousSibling { get; set; }
    }
}