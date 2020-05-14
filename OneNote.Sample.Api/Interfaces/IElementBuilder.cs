
using System.Collections.Generic;

namespace OneNote.Sample.Api
{
    public interface IElementBuilder
    {
        Element GetElement();
        void BuildElement(Dictionary<string, string> properties);
        void BuildElement(Dictionary<string, string> properties, CompositeElement<IOutlineChildElement> parent);
    }
}
