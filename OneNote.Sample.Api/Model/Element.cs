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
    }
}