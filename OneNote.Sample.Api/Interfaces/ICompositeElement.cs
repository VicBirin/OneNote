using System.Collections.Generic;

namespace OneNote.Sample.Api
{
    public interface ICompositeElement
    {
        List<T> GetChildElements<T>() where T : Element;
    }
}