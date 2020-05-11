using HtmlAgilityPack;
using System.IO;
using System.Linq;
using System.Text;

namespace OneNote.Sample.Api.Convertors
{
    public class GraphPageConverter : IPageConvertor<Microsoft.Graph.OnenotePage>
    {
        public Page ConvertToLocal(Microsoft.Graph.OnenotePage src, Notebook parentNotebook, Document parentDocument)
        {
            var dest = new Page(ElementType.Page)
            {
                Document = parentDocument
            };

            if (src == null)
            {
                return dest;
            }

            dest.Id = src.Id;
            dest.Level = src.Level;
            dest.Title = src.Title;
            dest.Order = src.Order;
            dest.LastModifiedDateTime = src.LastModifiedDateTime;
            dest.UserTags = src.UserTags;
            dest.CreatedTime = src.CreatedDateTime;

            var content = ReadPageContent(src.Content);
            dest.Source.LoadHtml(content);

            dest[0] = (IPageChildElement)ReadDocumentBody(null, dest.Source.DocumentNode.SelectSingleNode("//body"));

            return dest;
        }

        public Microsoft.Graph.OnenotePage ConvertToOneNote(Page src)
        {
            if (src == null)
            {
                return new Microsoft.Graph.OnenotePage();
            }

            var dest = new Microsoft.Graph.OnenotePage
            {
                Id = src.Id,
                Title = src.Title,
                UserTags = src.UserTags,
                Content = WritePageContent(src.Source),
            };

            return dest;
        }

        private string ReadPageContent(Stream stream)
        {
            if (stream == null)
            {
                return string.Empty;
            }

            using (StreamReader r = new StreamReader(stream, Encoding.UTF8))
            {
                return r.ReadToEnd();
            }
        }

        private Stream WritePageContent(HtmlDocument doc)
        {
            var stream = new MemoryStream();
            doc.Save(stream, Encoding.UTF8);
            stream.Position = 0;
            return stream;
        }

        private Element ReadDocumentBody(Element parent, HtmlNode node)
        {
            if (node == null)
            {
                return null;
            }

            var elm = ParseElement(node);
            
            if (parent != null)
            {
                elm.ParentElement = (ICompositeElement)parent;
                ((OutlineElement)parent).AddChildElement((IOutlineChildElement)elm);
            }

            if (node.NextSibling != null) elm.NextSibling = ParseElement(node.NextSibling);
            if(node.PreviousSibling != null) elm.PreviousSibling = ParseElement(node.PreviousSibling);

            foreach (HtmlNode n in node.ChildNodes)
            {
                ReadDocumentBody(elm, n);
            }
            return elm;
        }

        private Element ParseElement(HtmlNode node)
        {
            var elm = CreateElement(node);
            if (node.Attributes != null)
            {
                foreach (var attr in node.Attributes)
                {
                    if (attr.Name == "style" && !string.IsNullOrEmpty(attr.Value))
                    {
                        var styles = attr.Value.Split(';');
                        foreach (var style in styles)
                        {
                            var pair = style.Split(':');
                            elm.Styles.Add(pair.First(), pair.Last());
                        }
                    }
                    else
                    {
                        elm.Attributes.Add(attr.Name, attr.Value);
                    }
                }
            }
            return elm;
        }

        private Element CreateElement(HtmlNode node)
        {
            Element elm = null;
            switch (node.Name)
            {
                case "body":
                    elm = new OutlineElement(ElementType.Body);
                    break;
                case "div":
                    elm = new OutlineElement(ElementType.Block);
                    break;
                case "img":
                    elm = new ImageElement();
                    break;
                case "a":
                    elm = new OutlineElement(ElementType.Url);
                    break;
                case "h1":
                    elm = new OutlineElement(ElementType.Heading1);
                    break;
                case "h2":
                    elm = new OutlineElement(ElementType.Heading2);
                    break;
                case "h3":
                    elm = new OutlineElement(ElementType.Heading3);
                    break;
                case "h4":
                    elm = new OutlineElement(ElementType.Heading4);
                    break;
                case "#text":
                    elm = new OutlineElement(ElementType.Text);
                    break;
                case "p":
                    elm = new OutlineElement(ElementType.Paragraph);
                    break;
                default:
                    elm = new OutlineElement(ElementType.Element);
                    break;
            }

            return elm;
        }
    }
}
