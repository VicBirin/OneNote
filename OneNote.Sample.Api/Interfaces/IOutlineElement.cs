using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;

namespace OneNote.Sample.Api
{
    public interface IOutlineElement : IElement
    {
        ElementType ElementType { get; }
        Font Font { get; set; }
        float FontSize { get; set; }
        Color Color { get; set; }
        Margins Margins { get; set; }
        string Text { get; set; }
        string XPath { get; set; }

        Dictionary<string, string> Attributes { get; set; }
        Dictionary<string, string> Styles { get; set; }
    }
}