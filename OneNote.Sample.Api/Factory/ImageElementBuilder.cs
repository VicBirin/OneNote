using System.Collections.Generic;
using System.Drawing;

namespace OneNote.Sample.Api
{
    public class ImageElementBuilder : IImageElementBuilder
    {
        private readonly ImageElement element;
        private Dictionary<string, string> properties;

        public ImageElementBuilder()
        {
            element = new ImageElement();
        }

        public void BuildElement(Dictionary<string, string> properties)
        {
            this.properties = properties;
            ReadBody();
        }

        public void BuildElement(Dictionary<string, string> properties, CompositeElement<IOutlineChildElement> parent)
        {
            if (parent != null)
            {
                parent.AddChildElement(element as IOutlineChildElement);
                element.ParentElement = parent;
            }
            BuildElement(properties);
        }

        public Element GetElement()
        {
            return element;
        }

        public void ReadBody()
        {
            // to add later
        }

        public void ReadImageFormat()
        {
            // to add later
        }

        public void ReadSize()
        {
            var size = ParseSize(properties);
            element.Size = size;
        }

        public void ReadSource()
        {
            if (properties.ContainsKey("Src"))
            {
                element.Src = properties["Src"];
            }
        }

        private Size ParseSize(Dictionary<string, string> properties)
        {
            Size size = Size.Empty;
            if (properties.TryGetValue("width", out string width))
            {
                size.Width = int.Parse(width);
            }

            if (properties.TryGetValue("height", out string height))
            {
                size.Height = int.Parse(height);
            }

            return size;
        }
    }
}
