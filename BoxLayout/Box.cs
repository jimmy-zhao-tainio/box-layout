using System;
using System.Collections.Generic;
using System.Linq;

namespace Boxing
{
    abstract public class Box : BoxBase
    {
        public Box (Orientation orientation) : base (orientation)
        {
        }

        public void LayoutAlignCross ()
        {
            if (Lines == null)
                return;
            foreach (Line line in Lines)
            {
                Position.AlignCross (this, line);
                foreach (Box box in line.Children)
                {
                    if (box.Lines == null)
                        continue;
                    box.LayoutAlignCross ();
                }
            }
        }

        public void Layout (int width, int height)
        {
            Layout (0, 0, width, height);
        }

        public void Layout (int x, int y, int width, int height)
        {
            Size actualSize = Size.New (0, 0, Orientation);

            // Make sure layout size has been limited to UserMaxSize by the caller.
            // If it hasn't then it's an error by the caller and we'll limit ourselves.
            width = Math.Min (width, UserMaxSize.Width);
            height = Math.Min (height, UserMaxSize.Height);

            if (Children.Count > 0)
            {
                Size size = Size.New (width, height, Orientation);

                actualSize = LayoutPass (size);
            }
            LayoutPosition = Point.New (x, y, Orientation);
            LayoutSize = Size.New (width, height, Orientation);
            // Make sure actual size isn't smaller than UserMinSize.
            ActualSize.Main = Math.Max (actualSize.Main, UserMinSize.Main);
            ActualSize.Cross = Math.Max (actualSize.Cross, UserMinSize.Cross);
        }

        protected Size LayoutPass (Size layoutSize)
        {
            if (Wrap)
                Lines = WrapAlgorithm.GetLines (Orientation, Children, layoutSize.Main);
            else
                Lines = WrapAlgorithm.GetLine (Orientation, Children);

            // Find minimum cross length for each line.
            Point position = Point.New (Orientation);
            Size min = Size.New (Orientation);
            min.Main = layoutSize.Main;

            Lines.ForEach (line => {
                // Can't be smaller than line.MinSize though.
                min.Cross = line.MinSize.Cross;
                line.FinalPosition = Point.New (Orientation);
                line.ProbedUsedSize = LayoutLine (line, min);
            });

            WrapAlgorithm.SetLinesFinalSize (Lines, layoutSize);
            
            // Layout with final cross lengths.
            Size total = Size.New (Orientation);

            Lines.ForEach (line => {
                line.FinalPosition = Point.New (position);
                Size used = LayoutLine (line, line.FinalSize);
                position.Cross += line.FinalSize.Cross;

                total.Main = Math.Max (total.Main, used.Main);
                total.Cross += used.Cross;
            });

            return total;
        }

        protected Size LayoutLine (Line line, Size lineSize)
        {
            Point offset = Point.New (Orientation);
            Point point = Point.New (Orientation);
            Size usedTotal = Size.New (Orientation);
            Size size = Size.New (Orientation);

            Compute.MainLengths (Orientation, line.Children, lineSize.Main);

            for (int i = 0; i < line.Children.Count; i++)
            {
                Box child = line.Children[i];

                point.Main = line.FinalPosition.Main + offset.Main;
                point.Cross = line.FinalPosition.Cross;
                size.Main = child.Computed.MainLength;
                size.Cross = Math.Max (lineSize.Cross, line.MinSize.Cross);

                child.Layout (point.X, point.Y, size.Width, size.Height);

                // Cross size is largest minimum for all children on this line, but it shouldn't be used unless cross expand is true.
                if (child.Expand.GetCross (Orientation) == false &&
                    child.ActualSize.GetCross (Orientation) < child.LayoutSize.GetCross (Orientation))
                {
                    size.Main = child.Computed.MainLength;
                    size.Cross = child.ActualSize.GetCross (Orientation);
                    child.Layout (point.X, point.Y, size.Width, size.Height);
                }

                if (child.ActualSize.GetMain (Orientation) > child.LayoutSize.GetMain (Orientation))
                    usedTotal.Main += child.LayoutSize.GetMain (Orientation);
                else
                    usedTotal.Main += child.ActualSize.GetMain (Orientation);

                if (child.ActualSize.GetCross (Orientation) > child.LayoutSize.GetCross (Orientation))
                    usedTotal.Cross = Math.Max (usedTotal.Cross, child.LayoutSize.GetCross (Orientation));
                else
                    usedTotal.Cross = Math.Max (usedTotal.Cross, child.ActualSize.GetCross (Orientation));
                offset.Main += size.Main;
            }
            // Move this to Layout
            Position.AlignMain (this, line.Children, lineSize.Main);
            return usedTotal;
        }
    }
}