using HtmlAgilityPack;

namespace OneNote.Sample.Api
{
    public class GraphElementBuilderFactory : IElementBuilderFactory<HtmlNode>
    {
        public IElementBuilder GetBuilder(HtmlNode node)
        {
            switch (node.Name)
            {
                case "a":
                case "h1":
                case "h2":
                case "h3":
                case "h4":
                case "#text":
                    return new OutlineElementBuilder(ElementType.Text);
                case "p":
                    return new OutlineElementBuilder(ElementType.Paragraph);
                case "div":
                    return new OutlineElementBuilder(ElementType.Block);
                case "img":
                    return new ImageElementBuilder();
                default:
                    return new OutlineElementBuilder(ElementType.Element);
            }
        }
    }
}
