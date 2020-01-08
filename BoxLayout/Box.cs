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

                // Second pass, if cross fill is not used, because the first pass already gave all available cross length.
                if (Children.Any (b => b.Expand.GetCross (Orientation)) == false && used.Cross < size.Cross)
                {
                    size.Cross = used.Cross;
                    used = LayoutPass (size);
                }
            }
            LayoutPosition = Point.New (x, y, Orientation);
            LayoutSize = Size.New (width, height, Orientation);
            // Can't be smaller than UserMinSize.
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
            Dictionary<Line, int> minimumCross = new Dictionary<Line, int> ();
            Dictionary<Line, int> finalCross = new Dictionary<Line, int> ();

            Point position = Point.New (Orientation);

            // Layout with no cross length to find minimum used cross length for each line.
            Size min = Size.New (max.Width, max.Height, Orientation);
            lines.ForEach (line => {
                min.Cross = line.Min.Cross;
                minimumCross[line] = LayoutLine (position, min, line.Children, line.Min).Cross;
                // For lines that cannot expand, this would also be their final cross length.
                if (line.Expand.GetCross (Orientation) == false)
                    finalCross[line] = minimumCross[line];
            });

            // Find lines that can expand cross wise.
            List<Line> expandingLines = lines.Where (line => line.Expand.GetCross (Orientation)).ToList ();

            while (expandingLines.Count != 0)
            {
                // Find minimum cross lengths for expanding lines.
                int expandingMinCross = expandingLines.Sum (e => minimumCross[e]);
                // Find final cross lengths, i.e. already used lengths.
                int usedCross = finalCross.Sum (pair => pair.Value);
                // Calculate available cross length.
                int availableCross = max.Cross - usedCross;

                Spacing spacing = Spacing.New (expandingLines.Count, expandingMinCross, availableCross);

                // Set spacing
                expandingLines.ForEach (line => finalCross[line] = minimumCross[line] + spacing.Next ());

                // Find lines that hits UserMaxSize and expanded too much.
                List<Line> hitUserMax = expandingLines.Where (line => finalCross[line] >= line.UserMaxSize.Cross).ToList ();

                if (hitUserMax.Count > 0)
                {
                    // Correct the finalCross length for lines.
                    hitUserMax.ForEach (line => finalCross[line] = line.UserMaxSize.Cross);
                    // Remove them from the expandingLines list and re-run the loop if necessary.
                    expandingLines = expandingLines.Except (hitUserMax).ToList ();
                    // Reset expandingLines final cross length.
                    expandingLines.ForEach (line => finalCross[line] = 0);
                }
                else
                {
                    // If no line hit UserMax, then they've expanded as much as possible and we're done.
                    expandingLines.Clear ();
                }
            }

            // Layout with final cross lengths.
            Size total = Size.New (Orientation);

            lines.ForEach (line => {
                Size lineSize = Size.New (Orientation);
                lineSize.Main = max.Main;
                lineSize.Cross = finalCross[line];

                position.Main = 0;
                position.Cross = total.Cross;

                Size used = LayoutLine (position, lineSize, line.Children, line.Min);

                total.Main = Math.Max (total.Main, used.Main);
                total.Cross += lineSize.Cross;
            });

            return total;
        }

        protected Size LayoutLine (Point position,
                                   Size lineSize,
                                   List<Box> children,
                                   Size childrenSize)
        {
            Point offset = Point.New (Orientation);
            Point point = Point.New (Orientation);
            Size usedTotal = Size.New (Orientation);
            Size size = Size.New (Orientation);

            Dictionary<Box, int> mainLengths = ExpandLine.GetMainLengths (Orientation, children, lineSize.Main);

            for (int i = 0; i < children.Count; i++)
            {
                Box child = children[i];

                point.Main = position.Main + offset.Main;
                point.Cross = position.Cross;
                size.Main = mainLengths[child];
                size.Cross = lineSize.Cross;

                child.Layout (point, size);

                if (child.ActualSize.GetMain (Orientation) > child.LayoutSize.GetMain (Orientation))
                    usedTotal.Main += child.LayoutSize.GetMain (Orientation);
                else
                    usedTotal.Main += child.ActualSize.GetMain (Orientation);

                if (child.ActualSize.GetCross (Orientation) > child.LayoutSize.GetCross (Orientation))
                    usedTotal.Cross = Math.Max (usedTotal.Cross, child.LayoutSize.GetCross (Orientation));
                else
                    usedTotal.Cross = Math.Max (usedTotal.Cross, child.ActualSize.GetCross (Orientation));
                offset.Main = offset.Main + size.Main;
            }
            return usedTotal;
        }
    }
}