using HtmlAgilityPack;

namespace OneNote.Sample.Api
{
    public interface IGraphElementConverter<T> : IOneNoteConverter<T, Element>
    {
        Element ConvertToLocal(T src, CompositeElement<IOutlineChildElement> parent);
    }
}
