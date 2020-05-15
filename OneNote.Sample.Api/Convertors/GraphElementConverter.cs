using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OneNote.Sample.Api
{
    public class GraphElementConverter : IGraphElementConverter<HtmlNode>
    {
        /// <summary>
        /// Create respective factory builder
        /// </summary>
        private IElementBuilderFactory<HtmlNode> creator = new GraphElementBuilderFactory();

        public Element ConvertToLocal(HtmlNode node, CompositeElement<IOutlineChildElement> parent)
        {
            var builder = creator.GetBuilder(node);
            //if (!node.HasAttributes)
            //{
            //    return builder.GetElement();
            //}

            Dictionary<string, string> properties = ReadNodeProperties(node);

            if (builder.GetElement().ElementType == ElementType.Text)
            {
               properties.Add("Text", node.InnerText);
            }

            if (parent != null)
            {
                builder.BuildElement(properties, parent);
            }
            else
            {
                builder.BuildElement(properties);
            }

            return builder.GetElement();
        }

        public HtmlNode ConvertToOneNote(Element src)
        {
            throw new NotImplementedException();
        }

        private Dictionary<string, string> ReadNodeProperties(HtmlNode node)
        {
            var propertiers = new Dictionary<string, string>();
            foreach (var attr in node.Attributes)
            {
                if (attr.Name == "style" && !string.IsNullOrEmpty(attr.Value))
                {
                    foreach (var item in attr.Value.Split(';').Select(x => x.Split(':')).ToDictionary(x => x.First(), x => x.Last()))
                    {
                        propertiers.Add(item.Key, item.Value);
                    }
                }
                else
                {
                    propertiers.Add(attr.Name, attr.Value);
                }
            }
            return propertiers;
        }
    }
}
