using System.Collections.Generic;
using System.Drawing;
using System.IO;

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
            ReadSource();
            ReadSize();
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
            if (properties.ContainsKey("src"))
            {
                var src = properties["src"];
                var client = new GraphResourceFactory();
                var imageId = src.Split('/').GetValue(7).ToString();

                using (var stream = client.GetItem(imageId))
                {
                    var image = Image.FromStream(stream);
                    element.ImageFormat = image.RawFormat;

                    var bytesStream = new MemoryStream();
                    image.Save(bytesStream, image.RawFormat);

                    bytesStream.Position = 0;
                    element.Body = new byte[bytesStream.Length];
                    bytesStream.Read(element.Body, 0, (int)bytesStream.Length);
                }
            }
        }

        public void ReadSize()
        {
            var size = ParseSize(properties);
            element.Size = size;
        }

        public void ReadSource()
        {
            if (properties.ContainsKey("src"))
            {
                element.Src = properties["src"];
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
