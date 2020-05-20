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
                case "span":
                    return new OutlineElementBuilder(ElementType.TextElement);
                case "#text":
                    return new OutlineElementBuilder(ElementType.PlainText);
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
