using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;

namespace OneNote.Sample.Api
{
    public class OutlineElement : CompositeElement<IOutlineElementChild>, IOutlineElement, IPageChildElement, IElement, IOutlineElementChild
    {
        public OutlineElement(ElementType elementType) : base(elementType)
        {
            Attributes = new Dictionary<string, string>();
            Styles = new Dictionary<string, string>();
        }

        public Font Font
        {
            get
            {
                Styles.TryGetValue("font-family", out string fontFamily);
                if (string.IsNullOrEmpty(fontFamily)) return null;
                return new Font(fontFamily, FontSize);
            }
            set
            {
                Styles["font-family"] = value.Name;
            }
        }

        public float FontSize
        {
            get
            {
                Styles.TryGetValue("font-size", out string fontSize);
                if (string.IsNullOrEmpty(fontSize)) return 0;
                float.TryParse(fontSize.Replace("pt", ""), out float fontSizeValue);
                return fontSizeValue;
            }
            set { Styles["font-size"] = value + "pt"; }
        }

        public Color Color
        {
            get
            {
                ColorConverter converter = new ColorConverter();
                Styles.TryGetValue("color", out string colorString);
                if (string.IsNullOrEmpty(colorString)) return Color.Empty;
                return (Color)converter.ConvertFromString(colorString);
            }
            set
            {
                ColorConverter converter = new ColorConverter();
                Styles["color"] = converter.ConvertToString(value);
            }
        }
        public Margins Margins {
            get
            {
                Styles.TryGetValue("margin-top", out string topStr);
                Styles.TryGetValue("margin-bottom", out string bottomStr);
                Styles.TryGetValue("margin-left", out string leftStr);
                Styles.TryGetValue("margin-right", out string rightStr);

                int top = 0;
                int bottom = 0;
                int left = 0;
                int right = 0;

                if (!string.IsNullOrEmpty(topStr)) int.TryParse(topStr.Replace("pt", ""), out top);
                if (!string.IsNullOrEmpty(bottomStr)) int.TryParse(bottomStr.Replace("pt", ""), out bottom);
                if (!string.IsNullOrEmpty(leftStr)) int.TryParse(leftStr.Replace("pt", ""), out left);
                if (!string.IsNullOrEmpty(rightStr)) int.TryParse(rightStr.Replace("pt", ""), out right);

                var margins = new Margins(left,right,top,bottom);
                return margins;
            }
            set
            {
                Styles["margin-top"] = value.Top + "pt";
                Styles["margin-bottom"] = value.Bottom + "pt";
                Styles["margin-left"] = value.Left + "pt";
                Styles["margin-right"] = value.Right + "pt";
            }
        }
        public string Text { get; set; }

        public string XPath { get; set; }

        public Dictionary<string, string> Attributes { get; set; }
        public Dictionary<string, string> Styles { get; set; }

        public override string ToString()
        {
            var str = $"Type: {ElementType}; Text: '{Text}'; Font: {(Font == null ? "none" : Font.Name)}, {FontSize}pt; Color: {Color.Name}; Margins: top {Margins.Top}pt, bottom {Margins.Bottom}pt";
            return str;
        }
    }
}
