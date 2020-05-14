using System.Drawing;
using System.Text;

namespace OneNote.Sample.Api
{
    public class OutlineElement : CompositeElement<IOutlineChildElement>, IOutlineElement, IPageChildElement, IElement, IOutlineChildElement
    {
        public OutlineElement(ElementType elementType) : base(elementType)
        {
            IsComposite = true;
        }

        public string Text { get; set; }

        public TextStyle TextStyle { get; set; }

        public string Position { get; set; }

        public Margins Margins { get; set; }

        public Size Size { get; set; }

        public override string ToString()
        {
            var str = new StringBuilder($"Type: {ElementType}: ");
            if (!string.IsNullOrEmpty(Text))
            {
                str.Append($"'{Text.Trim('\r', '\n').Trim()}'; ");
            }

            if (TextStyle != null)
            {
                str.Append($"font: {TextStyle.FontName}, {TextStyle.FontSize}pt;");
            }

            if (TextStyle != null && TextStyle.FontColor != Color.Empty)
            {
                str.Append($"color: {TextStyle.FontColor.Name}; ");
            }

            if (Margins.Top > 0)
            {
                str.Append($"top {Margins.Top}pt; ");
            }

            if (Margins.Bottom > 0)
            {
                str.Append($"top {Margins.Bottom}pt; ");
            }

            if (Margins.Left > 0)
            {
                str.Append($"top {Margins.Left}pt; ");
            }

            if (Margins.Right > 0)
            {
                str.Append($"top {Margins.Right}pt; ");
            }

            if (TextStyle != null && !string.IsNullOrEmpty(TextStyle.Url))
            {
                str.Append($"href: {TextStyle.Url}; ");
            }

            if (!string.IsNullOrEmpty(Position))
            {
                str.Append($"position: {Position}; ");
            }

            return str.ToString();
        }
    }
}
