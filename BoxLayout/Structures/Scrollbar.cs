using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boxing
{
    public enum ScrollbarMode
    {
        Hidden,
        Auto,
        Visible
    }

    public abstract class Scrollbar
    {
        public ScrollbarMode Mode = ScrollbarMode.Auto;
        public Point Position;
        public Size Size;
        public bool Visible;

        public Scrollbar(ScrollbarMode mode)
        {
            Mode = mode;
            Position = Point.New(Orientation.Horizontal);
            Size = new Size(0, 0);
        }
    }

    public class HScrollbar: Scrollbar 
    {
        public HScrollbar(ScrollbarMode mode) : base (mode)
        {
        }
    }

    public class VScrollbar: Scrollbar 
    {
        public VScrollbar(ScrollbarMode mode) : base (mode)
        {
        }
    }
}