using System;

namespace OneNote.Sample.Api
{
    public abstract class Element : IElement
    {
        public ElementType ElementType { get; set; }
        public IElement NextSibling { get; set; }
        public ICompositeElement ParentElement { get; set; }
        public IElement PreviousSibling { get; set; }
    }
}