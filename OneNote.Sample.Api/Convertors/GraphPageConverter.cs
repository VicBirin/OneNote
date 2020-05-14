using HtmlAgilityPack;
using System.IO;
using System.Text;

namespace OneNote.Sample.Api.Convertors
{
    public class GraphPageConverter : IPageConverter<Microsoft.Graph.OnenotePage>
    {

        GraphElementConverter elementConverter = new GraphElementConverter();

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
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(content);

            dest[0] = ParseHtmlDocument(null, doc.DocumentNode);

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

                /// Todo: implement logic that writes page as html
                //Content = WritePageContent(src.Source),
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

        private IPageChildElement ParseHtmlDocument(CompositeElement<IOutlineChildElement> parent, HtmlNode node)
        {
            if (node == null)
            {
                return null;
            }

            var elm = ParseElement(node, parent);

            if (elm.IsComposite)
            {
                foreach (HtmlNode n in node.ChildNodes)
                {
                    ParseHtmlDocument(elm as CompositeElement<IOutlineChildElement>, n);
                }
            }

            return elm as IPageChildElement;
        }

        private Element ParseElement(HtmlNode node, CompositeElement<IOutlineChildElement> parent)
        {
            return elementConverter.ConvertToLocal(node, parent);
        }
    }
}
