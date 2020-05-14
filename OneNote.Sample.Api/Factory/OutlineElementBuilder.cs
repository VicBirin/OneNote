using System.Collections.Generic;
using System.Drawing;
using System.Globalization;

namespace OneNote.Sample.Api
{
    public class OutlineElementBuilder : IOutlineElementBuilder
    {
        private readonly OutlineElement element;
        private Dictionary<string, string> properties;

        public OutlineElementBuilder(ElementType elementType)
        {
            element = new OutlineElement(elementType);
        }

        public Element GetElement()
        {
            return element;
        }

        public void ReadText()
        {
            if (element.ElementType == ElementType.Text && properties.ContainsKey("Text"))
            {
                element.Text = properties["Text"];
            }
        }

        public void ReadTextStyle()
        {
            var textStyle = ParseTextStyle(properties);
            element.TextStyle = textStyle;
        }

        public void ReadMargins()
        {
            var margins = ParseMargins(properties);
            element.Margins = margins;
        }

        public void ReadPosition()
        {
            if (element.ElementType == ElementType.Text && properties.ContainsKey("position"))
            {
                element.Text = properties["position"];
            }
        }

        public void BuildElement(Dictionary<string, string> properties)
        {
            this.properties = properties;
            ReadText();
            ReadTextStyle();
            ReadMargins();
        }

        public void BuildElement(Dictionary<string, string> properties, CompositeElement<IOutlineChildElement> parent)
        {
            if (parent != null)
            {
                parent.AddChildElement(element as IOutlineChildElement);
                element.ParentElement = parent;
            }

            BuildElement(properties);
        }

        private Margins ParseMargins(Dictionary<string, string> properties)
        {
            properties.TryGetValue("margin-top", out string topStr);
            properties.TryGetValue("margin-bottom", out string bottomStr);
            properties.TryGetValue("margin-left", out string leftStr);
            properties.TryGetValue("margin-right", out string rightStr);

            float top = 0;
            float bottom = 0;
            float left = 0;
            float right = 0;

            if (!string.IsNullOrEmpty(topStr))
            {
                float.TryParse(topStr.Replace("pt", ""), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out top);
            }

            if (!string.IsNullOrEmpty(bottomStr))
            {
                float.TryParse(bottomStr.Replace("pt", ""), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out bottom);
            }

            if (!string.IsNullOrEmpty(leftStr))
            {
                float.TryParse(leftStr.Replace("pt", ""), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out left);
            }

            if (!string.IsNullOrEmpty(rightStr))
            {
                float.TryParse(rightStr.Replace("pt", ""), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out right);
            }

            var margins = new Margins(left, right, top, bottom);
            return margins;
        }

        private TextStyle ParseTextStyle(Dictionary<string, string> properties)
        {
            var textStyle = new TextStyle();

            if (properties.TryGetValue("font-family", out string fontFamily))
            {
                textStyle.FontName = fontFamily;
            }

            properties.TryGetValue("font-size", out string fontSize);
            if (!string.IsNullOrEmpty(fontSize) && float.TryParse(fontSize.Replace("pt", ""), out float fontSizeValue))
            {
                textStyle.FontSize = fontSizeValue;
            }

            var converter = new ColorConverter();
            properties.TryGetValue("color", out string colorString);
            if (string.IsNullOrEmpty(colorString))
            {
                textStyle.FontColor = Color.Empty;
            }
            else
            {
                textStyle.FontColor = (Color)converter.ConvertFromString(colorString);
            }

            return textStyle;
        }
    }
}
