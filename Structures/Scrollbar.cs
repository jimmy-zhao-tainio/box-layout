using System.Diagnostics;

namespace UI.Structures
{
    public enum ScrollbarMode
    {
        Hidden,
        Auto,
        Visible
    }

    public abstract class Scrollbar
    {
        public Orientation Orientation;
        public ScrollbarMode Mode = ScrollbarMode.Auto;
        public Point Position;
        public Size Size;
        public Size HandleSize;
        public Point HandlePosition;
        public bool Visible;

        private int ContentLength;
        private int ViewLength;
        private int ScrollbarLength;
        private int EffectiveScrollbarLength;
        private int HandleLength;
        private double ViewContentRatio;

        private int ContentScrollRange;
        private int HandleScrollRange;

        private double HandleContentOffsetRatio;
        private double ContentHandleOffsetRatio;

        private int DragOriginalAbsoluteMain = 0;
        private int DragOriginalHandleMain = 0;

        public int ContentOffset; // Amount of pixels scrolled.
        
        public Scrollbar(ScrollbarMode mode, Orientation orientation)
        {
            Orientation = orientation;
            Size = Size.New(orientation);
            HandleSize = Size.New(orientation);
            Position = Point.New(orientation);
            HandlePosition = Point.New(orientation);
            Mode = mode;
            ContentOffset = 0;
        }

        public void Update(int scrollAreaLength, int contentLength)
        {
            SetLengths(contentLength, scrollAreaLength);
            HandleSize.Main = HandleLength;
            HandleSize.Cross = Size.Cross - ScrollbarSettings.SidePadding * 2;

            if (scrollAreaLength + ContentOffset > contentLength)
                ContentOffset = contentLength - scrollAreaLength;

            int handleOffset = (int)(ContentOffset * ContentHandleOffsetRatio);

            HandlePosition.Main = Position.Main + ScrollbarSettings.LengthPadding + handleOffset;
            HandlePosition.Cross = Position.Cross + ScrollbarSettings.SidePadding;
        }

        public void SetContentOffsetByHandleCenter(int handleCenter)
        {
            int handlePosition = handleCenter - (HandleSize.Main / 2);
            SetContentOffsetByHandlePosition(handlePosition);
        }

        public void SetContentOffsetByHandlePosition(int handlePosition)
        {
            int handleOffset = handlePosition;
            if (handleOffset < 0)
                handleOffset = 0;
            else if (handleOffset + HandleSize.Main > EffectiveScrollbarLength)
                handleOffset = EffectiveScrollbarLength - HandleSize.Main;
            ContentOffset = (int)(handleOffset * HandleContentOffsetRatio);
        }

        public bool AtPoint(Structures.Point point)
        {
            if (Visible == false)
                return false;
            if (point.X >= Position.X && point.X < Position.X + Size.Width &&
                point.Y >= Position.Y && point.Y < Position.Y + Size.Height)
                return true;
            return false;
        }

        public bool HandleAtPoint(Structures.Point point)
        {
            if (Visible == false)
                return false;
            if (point.X >= HandlePosition.X && point.X < HandlePosition.X + HandleSize.Width &&
                point.Y >= HandlePosition.Y && point.Y < HandlePosition.Y + HandleSize.Height)
                return true;
            return false;
        }

        private void SetLengths(int contentLength, int viewLength)
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

        public void DragScrollBegin(int absoluteMain)
        {
            DragOriginalAbsoluteMain = absoluteMain;
            DragOriginalHandleMain = HandlePosition.Main;
        }

        public void DragScroll(int absoluteMain)
        {
            int mainDiff = absoluteMain - DragOriginalAbsoluteMain;
            SetContentOffsetByHandlePosition(DragOriginalHandleMain + mainDiff);
        }
    }

    public class HScrollbar: Scrollbar 
    {
        public HScrollbar(ScrollbarMode mode) : base (mode, Orientation.Horizontal)
        {
        }
    }

    public class VScrollbar: Scrollbar 
    {
        public VScrollbar(ScrollbarMode mode) : base (mode, Orientation.Vertical)
        {
        }
    }
}