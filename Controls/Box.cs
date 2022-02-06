using System.Collections.Generic;
using UI.Structures;

namespace UI.Controls
{
    public class BoxHorizontal : Box
    {
        public BoxHorizontal () : base (Orientation.Horizontal)
        {
        }
    }

    public class BoxVertical : Box
    {
        public BoxVertical () : base (Orientation.Vertical)
        {
        }
    }

    public class Box
    {
        public Orientation Orientation;

        public Size Min;
        public Size ChildrenMin;

        public List<Box> Children = new List<Box> ();

        public bool Wrap = false;
        public Expand Expand;

        public AlignMain AlignMain = AlignMain.Start;
        public AlignCross AlignCross = AlignCross.Start;
        public LineAlignCross LineAlignCross = LineAlignCross.Start;
        public SelfAlignCross SelfAlignCross = SelfAlignCross.Inherit;

        public Size UserMinSize;
        public Size UserMaxSize;

        public Point LayoutPosition;
        public Size LayoutSize;
        public Size ContentSize;
        public Size ScrollAreaSize;
        public BoxComputed Computed;

        public HScrollbar HorizontalScrollbar;
        public VScrollbar VerticalScrollbar;

        public List<Line> Lines;

        public Box (Orientation orientation)
        {
            Orientation = orientation;
            LayoutPosition = Point.New (Orientation);
            LayoutSize = Size.New (Orientation);
            ContentSize = Size.New (Orientation);
            ScrollAreaSize = Size.New (Orientation);
            Computed = new BoxComputed ();
            UserMinSize = Size.New (0, 0, Orientation);
            UserMaxSize = Size.New (int.MaxValue, int.MaxValue, Orientation);
            Expand = Expand.New (Orientation);
            HorizontalScrollbar = new HScrollbar(ScrollbarMode.Auto);
            VerticalScrollbar = new VScrollbar(ScrollbarMode.Auto);
        }

        public void Pack (Box child)
        {
            Children.Add(child);
        }

        public void Unpack (Box child)
        {
            for (int i = 0; i < Children.Count; i++)
            {
                if (Children[i] != child)
                    continue;
                Children.RemoveAt (i);
            }
        }
    }
}