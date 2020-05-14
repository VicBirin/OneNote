using System.Text;

namespace OneNote.Sample.Api
{
    public abstract class Element : IElement
    {
        public ElementType ElementType { get; set; }
        public IElement NextSibling { get; set; }
        public ICompositeElement ParentElement { get; set; }
        public IElement PreviousSibling { get; set; }
        public bool IsComposite { get; set; }

        public override string ToString()
        {
            var str = new StringBuilder($"Type: {ElementType}: ");
            str.Append($"isComposite: '{IsComposite}'; ");
            return str.ToString();
        }
    }
}