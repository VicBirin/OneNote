using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Text;

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

        public string Href
        {
            get
            {
                Attributes.TryGetValue("href", out string href);
                return href;
            }
            set { Attributes["href"] = value; }
        }

        public string Position
        {
            get
            {
                Styles.TryGetValue("position", out string position);
                return position; 
            }
            set { Styles["position"] = value; }
        }

        public Margins Margins {
            get
            {
                Styles.TryGetValue("margin-top", out string topStr);
                Styles.TryGetValue("margin-bottom", out string bottomStr);
                Styles.TryGetValue("margin-left", out string leftStr);
                Styles.TryGetValue("margin-right", out string rightStr);

                float top = 0;
                float bottom = 0;
                float left = 0;
                float right = 0;

                if (!string.IsNullOrEmpty(topStr)) float.TryParse(topStr.Replace("pt", ""), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out top);
                if (!string.IsNullOrEmpty(bottomStr)) float.TryParse(bottomStr.Replace("pt", ""), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out bottom);
                if (!string.IsNullOrEmpty(leftStr)) float.TryParse(leftStr.Replace("pt", ""), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out left);
                if (!string.IsNullOrEmpty(rightStr)) float.TryParse(rightStr.Replace("pt", ""), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out right);

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
            var str = new StringBuilder($"Type: {ElementType}: ");
            if (!string.IsNullOrEmpty(Text))
                str.Append($"'{Text.Trim('\r', '\n').Trim()}'; ");

            if (Font != null)
                str.Append($"font: {Font.Name}, {FontSize}pt;");

            if (Color != Color.Empty)
                str.Append($"color: {Color.Name}; ");

            if (Margins.Top > 0)
                str.Append($"top {Margins.Top}pt; ");

            if (Margins.Bottom > 0)
                str.Append($"top {Margins.Bottom}pt; ");

            if (Margins.Left > 0)
                str.Append($"top {Margins.Left}pt; ");

            if (Margins.Right > 0)
                str.Append($"top {Margins.Right}pt; ");

            if (!string.IsNullOrEmpty(Href))
                str.Append($"href: {Href}");

            if (!string.IsNullOrEmpty(Position))
                str.Append($"position: {Position}");

            return str.ToString();
        }
    }
}
