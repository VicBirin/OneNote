using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OneNote.Sample.Api
{
    public abstract class Element : IElement
    {
        public ElementType ElementType { get; set; }
        public IElement NextSibling { get; set; }
        public ICompositeElement ParentElement { get; set; }
        public IElement PreviousSibling { get; set; }

        public Dictionary<string, string> Attributes { get; set; }

        public Dictionary<string, string> Styles { get; set; }

        public void LoadElement(HtmlNode node)
        {
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
                            Styles.Add(pair.First(), pair.Last());
                        }
                    }
                    else
                    {
                        Attributes.Add(attr.Name, attr.Value);
                    }
                }
            }
        }

        void SaveElement()
        {
            throw new NotImplementedException();
        }
    }
}