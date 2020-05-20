using System.Drawing;

namespace OneNote.Sample.Api
{
    public interface IOutlineElementBuilder : IElementBuilder
    {
        void ReadText();
        void ReadTextStyle();
        void ReadMargins();
    }
}
