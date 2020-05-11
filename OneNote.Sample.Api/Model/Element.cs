using System.Collections.Generic;
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

        public string Text { get; set; }

        public Dictionary<string, string> Attributes { get; set; }
        public Dictionary<string, string> Styles { get; set; }

        public override string ToString()
        {
            var str = new StringBuilder($"Type: {ElementType}: ");
            if (!string.IsNullOrEmpty(Text))
            {
                str.Append($"'{Text.Trim('\r', '\n').Trim()}'; ");
            }
            return str.ToString();
        }
    }
}