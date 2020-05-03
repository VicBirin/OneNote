using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace OneNote.Sample.Api
{
    public class ImageElement : Element, IElement, IPageChildElement, IOutlineElementChild
    {
        public ImageElement(ElementType elementType)
        {
            ElementType = elementType;
            Attributes = new Dictionary<string, string>();
            Styles = new Dictionary<string, string>();
        }

        public Size Size
        {
            get
            {
                Size size = Size.Empty;
                if (Attributes.TryGetValue("width", out string width))
                {
                    size.Width = int.Parse(width);
                }

                if (Attributes.TryGetValue("height", out string height))
                {
                    size.Height = int.Parse(height);
                }

                return size;
            }
            set
            {
                Attributes["width"] = value.Width.ToString();
                Attributes["height"] = value.Height.ToString();
            }
        }

        public string Src
        {
            get
            {
                Attributes.TryGetValue("src", out string src);
                return src;
            }
            set { Attributes["src"] = value; }
        }

        public override string ToString()
        {
            var str = new StringBuilder($"Type: {ElementType}: ");

            if (Size != Size.Empty)
            {
                str.Append($"size: {Size.Width}x{Size.Height}; ");
            }

            if (!string.IsNullOrEmpty(Src))
            {
                str.Append($"source: {Src}; ");
            }

            return str.ToString();
        }
    }
}
