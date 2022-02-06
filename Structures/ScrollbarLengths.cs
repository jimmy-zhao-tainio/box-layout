namespace UI.Structures
{
    public class ScrollbarLengths
    {
        private int ContentLength;
        private int ViewLength;
        private int ScrollbarLength;
        public int EffectiveScrollbarLength;
        public int HandleLength;
        private double ViewContentRatio;

        private int ContentScrollRange;
        private int HandleScrollRange;

        public double HandleContentOffsetRatio;
        public double ContentHandleOffsetRatio;

        public ScrollbarLengths()
        {
        }

        public void SetLengths(int contentLength, int viewLength)
        {
            ContentLength = contentLength;
            ViewLength = viewLength;
            ScrollbarLength = viewLength;
            EffectiveScrollbarLength = ScrollbarLength - (ScrollbarSettings.LengthPadding * 2);

            ViewContentRatio = GetViewContentRatio();
            HandleLength = GetHandleLength();

            ContentScrollRange = ContentLength - ViewLength;
            HandleScrollRange = EffectiveScrollbarLength - HandleLength;

            HandleContentOffsetRatio = ContentScrollRange / (double)HandleScrollRange;
            ContentHandleOffsetRatio = HandleScrollRange / (double)ContentScrollRange;
        }

        private double GetViewContentRatio()
        {
            if (ContentLength != 0)
                return ViewLength / (double)ContentLength;
            else
                return 1d;
        }

        private int GetHandleLength()
        {
            int length = (int)(EffectiveScrollbarLength * ViewContentRatio);

            if (length > EffectiveScrollbarLength)
                return EffectiveScrollbarLength;
            if (length < ScrollbarSettings.MinHandleLength)
            {
                if (EffectiveScrollbarLength < ScrollbarSettings.MinHandleLength)
                    return EffectiveScrollbarLength;
                return ScrollbarSettings.MinHandleLength;
            }
            return length;
        }
    }
}