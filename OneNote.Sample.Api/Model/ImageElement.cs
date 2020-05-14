using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace OneNote.Sample.Api
{
    public class ImageElement : Element, IElement, IPageChildElement, IOutlineChildElement
    {
        public ImageElement()
        {
            ElementType = ElementType.Image;
            Size = Size.Empty;
        }

        public ImageFormat ImageFormat { get; set; }

        public Size Size { get; set; }

        public string Src { get; set; }

        public byte[] Body { get; set; }

        public override string ToString()
        {
            var str = new StringBuilder(base.ToString());

            if (Size != Size.Empty)
            {
                str.Append($"size: {Size.Width}x{Size.Height}; ");
            }

            if (!string.IsNullOrEmpty(Src))
            {
                str.Append($"source: '{Src}'; ");
            }

            if (Body != null)
            {
                str.Append($"body: '{Body.Length}' bytes; ");
            }

            if (Size != Size.Empty)
            {
                str.Append($"size: {Size.Width}x{Size.Height}; ");
            }

            return str.ToString();
        }
    }
}
