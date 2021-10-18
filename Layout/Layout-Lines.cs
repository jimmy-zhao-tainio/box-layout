using System;
using UI.Structures;
using UI.Controls;

namespace UI.Layout
{
    public partial class LayoutManager
    {
        static private void LayoutLines (Box box, int width, int height, int horizontalScrollbarThickness, int verticalScrollbarThickness)
        {
            Size contentSize = Size.New (0, 0, box.Orientation);

            // Make sure layout size has been limited to UserMaxSize by the caller.
            // If it hasn't then it's an error by the caller and we'll limit ourselves.
            box.LayoutSize.Width = Math.Min (width, box.UserMaxSize.Width);
            box.LayoutSize.Height = Math.Min (height, box.UserMaxSize.Height);

            // Temporarily reduce available LayoutSize with scrollbar thickness.
            box.LayoutSize.Width -= verticalScrollbarThickness;
            box.LayoutSize.Height -= horizontalScrollbarThickness;

            if (box.Children.Count > 0)
            {
                Size size = Size.New (box.LayoutSize.Width, box.LayoutSize.Height, box.Orientation);
                Lines lines = new Lines ();

                if (box.Wrap) {
                    // If wrapping, size.Main can't be smaller than ChildrenMin.Main
                    size.Main = Math.Max(box.ChildrenMin.Main, size.Main);
                    box.Lines = lines.GetLines(box, box.Children, size.Main);
                }
                else
                    box.Lines = lines.GetLine(box.Orientation, box.Children);

                // Find minimum cross length for each line.
                Size min = Size.New (box.Orientation);
                min.Main = size.Main;

                box.Lines.ForEach (line => {
                    min.Cross = line.MinSize.Cross;

                    // Layout with minimum line cross length.
                    LayoutLine(line, min);
                    line.ProbedUsedSize = line.LayoutSize;
                });

                Compute.SetLinesSize (box.Lines, size);

                box.Lines.ForEach (line => {
                    // Layout with final cross lengths.
                    LayoutLine (line, line.FinalSize);

                    contentSize.Main = Math.Max (contentSize.Main, line.LayoutSize.Main);
                    contentSize.Cross += line.LayoutSize.Cross;
                });
            }

            // Make sure content size isn't smaller than UserMinSize.
            box.ContentSize.Width = Math.Max (contentSize.Width, box.UserMinSize.Width - verticalScrollbarThickness);
            box.ContentSize.Height = Math.Max (contentSize.Height, box.UserMinSize.Height - horizontalScrollbarThickness);

            box.LayoutSize.Width += verticalScrollbarThickness;
            box.LayoutSize.Height += horizontalScrollbarThickness;
        }

        static protected void LayoutLine (Line line, Size lineSize)
        {
            Size size = Size.New (line.Orientation);

            Compute.SetMainLengths (line.Orientation, line.Children, lineSize.Main);

            for (int i = 0; i < line.Children.Count; i++)
            {
                Box child = line.Children[i];

                size.Main = child.Computed.MainLength;
                size.Cross = Math.Max (lineSize.Cross, line.MinSize.Cross);

                SetScrolling(child, size.Width, size.Height);

                // Cross size is largest minimum for all children on this line, but it shouldn't be used unless cross expand is true.
                if (child.Expand.GetCross (line.Orientation) == false &&
                    child.ContentSize.GetCross (line.Orientation) < child.LayoutSize.GetCross (line.Orientation))
                {
                    size.Main = child.Computed.MainLength;
                    size.Cross = child.ContentSize.GetCross (line.Orientation);
                    SetScrolling(child, size.Width, size.Height);
                }
            }
        }
    }
}