using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boxing
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

        public bool EqualSizeMain = false;
        public bool EqualSizeCross = false;
        public SelfEqualSize SelfEqualSizeMain = SelfEqualSize.Inherit;
        public SelfEqualSize SelfEqualSizeCross = SelfEqualSize.Inherit;

        public AlignMain AlignMain = AlignMain.Start;
        public AlignCross AlignCross = AlignCross.Start;
        public LineAlignCross LineAlignCross = LineAlignCross.Start;
        public SelfAlignCross SelfAlignCross = SelfAlignCross.Inherit;

        public Size UserMinSize;
        public Size UserMaxSize;

        public Point LayoutPosition;
        public Size LayoutSize;
        public Size ActualSize;
        public BoxComputed Computed;

        public Scrollbar ScrollbarHorizontal = Scrollbar.Auto;
        public Scrollbar ScrollbarVertical = Scrollbar.Auto;
        public Point ScrollbarHorizontalPosition;
        public Size ScrollbarHorizontalSize;
        public Point ScrollbarVerticalPosition;
        public Size ScrollbarVerticalSize;

        public List<Line> Lines;

        public Box (Orientation orientation)
        {
            Orientation = orientation;
            LayoutPosition = Point.New (Orientation);
            LayoutSize = Size.New (Orientation);
            ActualSize = Size.New (Orientation);
            Computed = new BoxComputed ();
            UserMinSize = Size.New (0, 0, Orientation);
            UserMaxSize = Size.New (int.MaxValue, int.MaxValue, Orientation);
            Expand = Expand.New (Orientation);
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