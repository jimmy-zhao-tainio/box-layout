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
        public int Offset; // Amount of pixels scrolled.

        public Scrollbar(ScrollbarMode mode, Orientation orientation)
        {
            Orientation = orientation;
            Size = Size.New(orientation);
            HandleSize = Size.New(orientation);
            Position = Point.New(orientation);
            HandlePosition = Point.New(orientation);
            Mode = mode;
            Offset = 0;
        }

        public bool AtPoint(Structures.Point point)
        {
            if (Visible == false)
                return false;
            if (point.X - Position.X < 0 ||
                point.Y - Position.Y < 0 ||
                point.X - Position.X >= Size.Width ||
                point.Y - Position.Y >= Size.Height)
                return false;
            return true;
        }

        public bool HandleAtPoint(Structures.Point point)
        {
            if (Visible == false)
                return false;
            if (point.X - HandlePosition.X < 0 ||
                point.Y - HandlePosition.Y < 0 ||
                point.X - HandlePosition.X >= HandleSize.Width ||
                point.Y - HandlePosition.Y >= HandleSize.Height)
                return false;
            return true;
        }

        public void SetHandleGeometry(int contentLength)
        {
            // (scrollbarLength / contentLength) * scrollbarLength
            HandleSize.Main = Size.Main * Size.Main;
            HandleSize.Main /= contentLength;
            HandleSize.Main -= ScrollbarSettings.LengthPadding * 2;

            if (HandleSize.Main < ScrollbarSettings.Thickness)
            {
                if (Size.Main >= ScrollbarSettings.MinHandleLength + ScrollbarSettings.LengthPadding * 2)
                    HandleSize.Main = ScrollbarSettings.Thickness;
                else
                    HandleSize.Main = Size.Main - ScrollbarSettings.LengthPadding * 2;
            }
            HandleSize.Cross = Size.Cross - ScrollbarSettings.SidePadding * 2;
            HandlePosition.Main = Position.Main + ScrollbarSettings.LengthPadding;
            HandlePosition.Cross = Position.Cross + ScrollbarSettings.SidePadding;
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