using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boxing
{
    public class BoxComputed
    {
        // Uses parent orientation
        public int MainLength;
        public bool CanExpand;
    }

    abstract public class BoxBase
    {
        public Orientation Orientation;

        public BoxBase (Orientation orientation)
        {
            Orientation = orientation;
            ActualSize = Size.New (Orientation);
            UserMinSize = Size.New (0, 0, Orientation);
            UserMaxSize = Size.New (int.MaxValue, int.MaxValue, Orientation);
            Expand = Expand.New (Orientation);
        }

        // Minimum size, considering childrens min and UserMinSize.
        public Size Min;
        public Size ChildrenMin;

        public List<Box> Children = new List<Box> ();

        public bool Wrap = false;
        public List<Box> Lines = null; // Should be not null if Wrap = true

        public Expand Expand;

        public Align AlignMain = Align.Start;
        public Align AlignCross = Align.Start;
        public SelfAlignCross SelfAlignCross = SelfAlignCross.Inherit;

        public Size UserMinSize;
        public Size UserMaxSize;

        public Point LayoutPosition;
        public Size LayoutSize;
        public Size ActualSize;

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

        public BoxComputed Computed = new BoxComputed ();
    }
}