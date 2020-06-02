using System;
using System.Collections.Generic;
using System.Linq;

namespace Boxing
{
    public class Layout
    {
        static public void Run (Box top, int width, int height)
        {
            LayoutLines (top, width, height);
            SetLinePositions (top);
            SetAlignMain (top);
            SetAlignCross (top);
        }

        static private void LayoutLines (Box box, int width, int height)
        {
            Size actualSize = Size.New (0, 0, box.Orientation);

            // Make sure layout size has been limited to UserMaxSize by the caller.
            // If it hasn't then it's an error by the caller and we'll limit ourselves.
            width = Math.Min (width, box.UserMaxSize.Width);
            height = Math.Min (height, box.UserMaxSize.Height);

            if (box.Children.Count > 0)
            {
                Size size = Size.New (width, height, box.Orientation);

                if (box.Wrap)
                    box.Lines = Wrapping.GetLines (box.Orientation, box.Children, size.Main);
                else
                    box.Lines = Wrapping.GetLine (box.Orientation, box.Children);

                // Find minimum cross length for each line.
                Size min = Size.New (box.Orientation);
                min.Main = size.Main;

                box.Lines.ForEach (line => {
                    // Can't be smaller than line.MinSize though.
                    min.Cross = line.MinSize.Cross;
                    line.ProbedUsedSize = LayoutLine (line, min);
                });

                Wrapping.SetLinesFinalSize (box.Lines, size);

                box.Lines.ForEach (line => {
                    // Layout with final cross lengths.
                    Size used = LayoutLine (line, line.FinalSize);

                    actualSize.Main = Math.Max (actualSize.Main, used.Main);
                    actualSize.Cross += used.Cross;
                });
            }
            box.LayoutSize.Width = width;
            box.LayoutSize.Height = height;
            // Make sure actual size isn't smaller than UserMinSize.
            box.ActualSize.Main = Math.Max (actualSize.Main, box.UserMinSize.Main);
            box.ActualSize.Cross = Math.Max (actualSize.Cross, box.UserMinSize.Cross);
        }

        static protected Size LayoutLine (Line line, Size lineSize)
        {
            Point offset = Point.New (line.Orientation);
            Size usedTotal = Size.New (line.Orientation);
            Size size = Size.New (line.Orientation);

            Compute.SetMainLengths (line.Orientation, line.Children, lineSize.Main);

            for (int i = 0; i < line.Children.Count; i++)
            {
                Box child = line.Children[i];

                size.Main = child.Computed.MainLength;
                size.Cross = Math.Max (lineSize.Cross, line.MinSize.Cross);

                LayoutLines (child, size.Width, size.Height);

                // Cross size is largest minimum for all children on this line, but it shouldn't be used unless cross expand is true.
                if (child.Expand.GetCross (line.Orientation) == false &&
                    child.ActualSize.GetCross (line.Orientation) < child.LayoutSize.GetCross (line.Orientation))
                {
                    size.Main = child.Computed.MainLength;
                    size.Cross = child.ActualSize.GetCross (line.Orientation);
                    LayoutLines(child, size.Width, size.Height);
                }

                if (child.ActualSize.GetMain (line.Orientation) > child.LayoutSize.GetMain (line.Orientation))
                    usedTotal.Main += child.LayoutSize.GetMain (line.Orientation);
                else
                    usedTotal.Main += child.ActualSize.GetMain (line.Orientation);

                if (child.ActualSize.GetCross (line.Orientation) > child.LayoutSize.GetCross (line.Orientation))
                    usedTotal.Cross = Math.Max (usedTotal.Cross, child.LayoutSize.GetCross (line.Orientation));
                else
                    usedTotal.Cross = Math.Max (usedTotal.Cross, child.ActualSize.GetCross (line.Orientation));
                offset.Main += size.Main;
            }
            return usedTotal;
        }

        static private void SetLinePositions (Box box)
        {
            if (box.Lines == null)
                return;
            Positioning.LineAlignCross (box);

            foreach (Box child in box.Children)
            {
                if (child.Lines == null)
                    continue;
                SetLinePositions (child);
            }

        }

        static private void SetAlignMain (Box box)
        {
            if (box.Lines == null)
                return;
            foreach (Line line in box.Lines)
            {
                Positioning.AlignMain (box, line);
                foreach (Box child in line.Children)
                {
                    if (child.Lines == null)
                        continue;
                    SetAlignMain (child);
                }
            }
        }

        static private void SetAlignCross (Box box)
        {
            if (box.Lines == null)
                return;
            foreach (Line line in box.Lines)
            {
                Positioning.AlignCross (box, line);
                foreach (Box child in line.Children)
                {
                    if (child.Lines == null)
                        continue;
                    SetAlignCross (child);
                }
            }
        }
    }
}