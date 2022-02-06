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

        public int ContentOffset; // Amount of pixels scrolled.
        
        ScrollbarLengths Lengths = new ScrollbarLengths();

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
            Lengths.SetLengths(contentLength, scrollAreaLength);
            HandleSize.Main = Lengths.HandleLength;
            HandleSize.Cross = Size.Cross - ScrollbarSettings.SidePadding * 2;

            if (scrollAreaLength + ContentOffset > contentLength)
                ContentOffset = contentLength - scrollAreaLength;
        }

        public void SetHandlePositionByContentOffset()
        {
            int handleOffset = (int)(ContentOffset * Lengths.ContentHandleOffsetRatio);

            HandlePosition.Main = Position.Main + ScrollbarSettings.LengthPadding + handleOffset;
            HandlePosition.Cross = Position.Cross + ScrollbarSettings.SidePadding;
        }

        public int GetContentOffsetByHandleCenter(int handleCenter)
        {
            int handleOffset = handleCenter - (HandleSize.Main / 2);
            if (handleOffset < 0)
                handleOffset = 0;
            else if (handleOffset + HandleSize.Main > Lengths.EffectiveScrollbarLength)
                handleOffset = Lengths.EffectiveScrollbarLength - HandleSize.Main;
            return (int)(handleOffset * Lengths.HandleContentOffsetRatio);
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