namespace OneNote.Sample.Api
{
    public class Margins
    {
        public Margins(float left, float right, float top, float bottom)
        {
            Left = left;
            Right = Right;
            Top = top;
            Bottom = bottom;
        }

        public float Left { get; set; }

        public float Right { get; set; }

        public float Top { get; set; }

        public float Bottom { get; set; }
    }
}
