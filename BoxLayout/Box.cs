using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace Boxing
{
    /*
    Box options

=== Orientation
        Orientation [Horizontal / Vertical]
=== Size
        UseExtraspace=true/false
        Fill
        Expand
        Homogeneous
        HAlign
        VAlign
     */
    abstract public class Box : BoxBase
    {
        public Box (Orientation orientation) : base (orientation)
        {
        }

        public void Layout (int width, int height)
        {
            Layout (0, 0, width, height);
        }

        public void Layout (Point point, Size size)
        {
            Layout (point.X, point.Y, size.Width, size.Height);
        }

        public void Layout (int x, int y, int width, int height)
        {
            Size used;

            // Make sure layout size has been limited to UserMaxSize by the caller.
            // If it hasn't then it's an error by the caller and we'll limit ourselves.
            width = Math.Min (width, UserMaxSize.Width);
            height = Math.Min (height, UserMaxSize.Height);

            if (Children.Count == 0)
            {
                used = Size.New (0, 0, Orientation);
            }
            else
            {
                Size size = Size.New (width, height, Orientation);

                // First pass, find used cross size given all layout size.
                // This could be the final pass if cross fill is used since all layout size is given here.
                used = LayoutPass (size);

                // Second pass, if cross fill is not used (because the first pass already gave all available cross length).
                if (Children.Any (b => b.Expand.GetCross (Orientation)) == false && used.Cross < size.Cross)
                {
                    size.Cross = used.Cross;
                    used = LayoutPass (size);
                }
            }
            // Always respect given layout position and size.
            LayoutPosition = Point.New (x, y, Orientation);
            LayoutSize = Size.New (width, height, Orientation);
            // Can't be smaller than user limits.
            ActualSize.Main = Math.Max (used.Main, UserMinSize.Main);
            ActualSize.Cross = Math.Max (used.Cross, UserMinSize.Cross);
        }

        protected Size LayoutPass (Size layoutSize)
        {
            if (Wrap == true && Children.Count > 1)
                return LayoutPassWrap (layoutSize);
            return LayoutLine (Point.New (Orientation), layoutSize, Children, ChildrenMin);
        }

        private Size LayoutPassWrap (Size max)
        {
            List<Line> lines = Lines.GetLines (Orientation, Children, max.Width, max.Height);
            Point position = Point.New (Orientation);
            Point offset = Point.New (Orientation);
            Size total = Size.New (Orientation);
            Size size = Size.New (Orientation);
            Size used;
            Spacing fill;
            Line line;

            offset.Cross = max.Cross;

            for (int i = 0; i < lines.Count; i++)
            {
                line = lines[i];
                position.Main = 0;
                position.Cross = total.Cross;
                size.Main = max.Main;
                size.Cross = offset.Cross;

                used = LayoutLine (position, size, line.Children, line.Min);
                total.Main = Math.Max (total.Main, used.Main);
                total.Cross += used.Cross;
                offset.Cross = Math.Max (offset.Cross - used.Cross, 0);
            }

            fill = Boxing.Spacing.New (lines.Count (l => l.Children.Any (c => c.Expand.GetCross (Orientation))), total.Cross, max.Cross);
            total.Reset ();
            offset.Cross = max.Cross;

            for (int i = 0; i < lines.Count; i++)
            {
                line = lines[i];
                position.Main = 0;
                position.Cross = total.Cross;
                size.Main = max.Main;
                size.Cross = offset.Cross;

                used = LayoutLine (position, size, line.Children, line.Min);
                position.Main = 0;
                position.Cross = total.Cross;
                size.Main = max.Main;
                size.Cross = used.Cross + (line.Children.Any (c => c.Expand.GetCross(Orientation)) ? fill.Next () : 0);

                used = LayoutLine (position, size, line.Children, line.Min);
                total.Main = Math.Max (total.Main, used.Main);
                total.Cross = total.Cross + size.Cross;
                offset.Cross = Math.Max (offset.Cross - size.Cross, 0);
            }
            return total;
        }

        protected Size LayoutLine (Point position,
                                   Size lineSize,
                                   List<Box> children,
                                   Size childrenSize)
        {
            Point offset = Point.New (Orientation);
            Point point = Point.New (Orientation);
            Size used = Size.New (Orientation);
            Size size = Size.New (Orientation);

            Spacing fill = Boxing.Spacing.New (children.Count (c => c.Expand.GetMain (Orientation)), childrenSize.Main, lineSize.Main);

            for (int i = 0; i < children.Count; i++)
            {
                Box child = children[i];

                point.Main = position.Main + offset.Main;
                point.Cross = position.Cross;

                // Distribute extra space along the main axis for those that wants it.
                int fillLength = (child.Expand.GetMain (Orientation) ? fill.Next () : 0);
                size.Main = child.Min.GetMain (Orientation) + fillLength;
                size.Cross = lineSize.Cross;

                // Make sure we're respecting user set max sizes.
                size.Width = Math.Min (size.Width, child.UserMaxSize.Width);
                size.Height = Math.Min (size.Height, child.UserMaxSize.Height);

                child.Layout (point, size);

                // Get the actual used size, unless it overflows.
                if (child.ActualSize.GetMain (Orientation) > child.LayoutSize.GetMain (Orientation))
                    used.Main += child.LayoutSize.GetMain (Orientation);
                else
                    used.Main += child.ActualSize.GetMain (Orientation);

                if (child.ActualSize.GetCross (Orientation) > child.LayoutSize.GetCross (Orientation))
                    used.Cross = Math.Max (used.Cross, child.LayoutSize.GetCross (Orientation));
                else
                    used.Cross = Math.Max (used.Cross, child.ActualSize.GetCross (Orientation));

                // Make sure not to use size.Main here as it does not include fillLength.
                offset.Main = offset.Main + child.Min.GetMain (Orientation) + fillLength;
            }
            return used;
        }
    }
}