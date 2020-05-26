using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boxing 
{
    public class LayoutAlgorithm
    {
        static public void Layout (Box box, int width, int height)
        {
            Layout (box, 0, 0, width, height);
        }

        static private void Layout (Box box, Point point, Size size)
        {
            Layout (box, point.X, point.Y, size.Width, size.Height);
        }

        static private void Layout (Box box, int x, int y, int width, int height)
        {
            Size used;

            // Make sure layout size has been limited to UserMaxSize by the caller.
            // If it hasn't then it's an error by the caller and we'll limit ourselves.
            width = Math.Min (width, box.UserMaxSize.Width);
            height = Math.Min (height, box.UserMaxSize.Height);

            if (box.Children.Count == 0)
            {
                used = Size.New (0, 0, box.Orientation);
            }
            else
            {
                Size size = Size.New (width, height, box.Orientation);

                // First pass, find used cross size given all layout size.
                // This could be the final pass if cross expand is used since all layout size is given here.
                used = LayoutPass (box, size);

                // Second pass, if cross expand is not used, because the first pass already gave all available cross length.
                if (box.Children.Any (b => b.Expand.GetCross (box.Orientation)) == false && used.Cross < size.Cross)
                {
                    size.Cross = used.Cross;
                    used = LayoutPass (box, size);
                }
            }
            box.LayoutPosition = Point.New (x, y, box.Orientation);
            box.LayoutSize = Size.New (width, height, box.Orientation);
            // Make sure actual size isn't smaller than UserMinSize.
            box.ActualSize.Main = Math.Max (used.Main, box.UserMinSize.Main);
            box.ActualSize.Cross = Math.Max (used.Cross, box.UserMinSize.Cross);
        }

        static private Size LayoutPass (Box box, Size layoutSize)
        {
            if (box.Wrap == true && box.Children.Count > 1)
                return LayoutPassWrap (box, layoutSize);
            return LayoutLine (box, Point.New (box.Orientation), layoutSize, box.Children, box.ChildrenMin);
        }

        static private Size LayoutPassWrap (Box box, Size max)
        {
            List<Line> lines = LinesAlgorithm.GetLines (box.Orientation, box.Children, max.Width, max.Height);
            Dictionary<Line, int> minimumCross = new Dictionary<Line, int> ();
            Dictionary<Line, int> finalCross = new Dictionary<Line, int> ();

            Point position = Point.New (box.Orientation);

            // Layout with no cross length to find minimum used cross length for each line.
            Size min = Size.New (max.Width, max.Height, box.Orientation);
            lines.ForEach (line => {
                min.Cross = line.Min.Cross;
                minimumCross[line] = LayoutLine (box, position, min, line.Children, line.Min).Cross;
                // For lines that cannot expand, this would also be their final cross length.
                if (line.Expand.GetCross (box.Orientation) == false)
                    finalCross[line] = minimumCross[line];
            });

            // Find lines that can expand cross wise.
            List<Line> expandingLines = lines.Where (line => line.Expand.GetCross (box.Orientation)).ToList ();

            while (expandingLines.Count != 0)
            {
                // Find minimum cross lengths for expanding lines.
                int expandingMinCross = expandingLines.Sum (e => minimumCross[e]);
                // Calculate available cross length.
                int availableCross = max.Cross - finalCross.Sum (pair => pair.Value);

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
            Size total = Size.New (box.Orientation);

            lines.ForEach (line => {
                Size lineSize = Size.New (box.Orientation);
                lineSize.Main = max.Main;
                lineSize.Cross = finalCross[line];

                position.Main = 0;
                position.Cross = total.Cross;

                Size used = LayoutLine (box, position, lineSize, line.Children, line.Min);

                total.Main = Math.Max (total.Main, used.Main);
                total.Cross += lineSize.Cross;
            });

            return total;
        }

        static private Size LayoutLine (Box box,
                                        Point position,
                                        Size lineSize,
                                        List<Box> children,
                                        Size childrenMinSize)
        {
            Point offset = Point.New (box.Orientation);
            Point point = Point.New (box.Orientation);
            Size usedTotal = Size.New (box.Orientation);
            Size size = Size.New (box.Orientation);

            Compute.MainLengths (box.Orientation, children, lineSize.Main);

            for (int i = 0; i < children.Count; i++)
            {
                Box child = children[i];

                point.Main = position.Main + offset.Main;
                point.Cross = position.Cross;
                size.Main = child.Computed.MainLength;
                size.Cross = Math.Max (lineSize.Cross, childrenMinSize.Cross);

                Layout (child, point, size);

                // Cross size is largest minimum for all children on this line, but it shouldn't be used unless cross expand is true.
                if (child.Expand.GetCross (box.Orientation) == false &&
                    child.ActualSize.GetCross (box.Orientation) < child.LayoutSize.GetCross (box.Orientation))
                {
                    size.Main = child.Computed.MainLength;
                    size.Cross = child.ActualSize.GetCross (box.Orientation);
                    Layout (child, point, size);
                }

                if (child.ActualSize.GetMain (box.Orientation) > child.LayoutSize.GetMain (box.Orientation))
                    usedTotal.Main += child.LayoutSize.GetMain (box.Orientation);
                else
                    usedTotal.Main += child.ActualSize.GetMain (box.Orientation);

                if (child.ActualSize.GetCross (box.Orientation) > child.LayoutSize.GetCross (box.Orientation))
                    usedTotal.Cross = Math.Max (usedTotal.Cross, child.LayoutSize.GetCross (box.Orientation));
                else
                    usedTotal.Cross = Math.Max (usedTotal.Cross, child.ActualSize.GetCross (box.Orientation));
                offset.Main += size.Main;
            }
            Position.AlignMain (box, children, lineSize.Main);
            //Position.AlignCross (this, children, position, usedTotal.Cross);
            return usedTotal;
        }
    }
}
