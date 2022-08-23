using System;
using UI.Structures;

namespace UI.Controls
{
    public partial class Box
    {
        private void LayoutLines (int width, int height, int horizontalScrollbarThickness, int verticalScrollbarThickness)
        {
            Size contentSize = Size.New (0, 0, Orientation);

            // Make sure layout size has been limited to UserMaxSize by the caller.
            // If it hasn't then it's an error by the caller and we'll limit ourselves.
            LayoutSize.Width = Math.Min (width, UserMaxSize.Width);
            LayoutSize.Height = Math.Min (height, UserMaxSize.Height);

            // Temporarily reduce available LayoutSize with scrollbar thickness.
            LayoutSize.Width -= verticalScrollbarThickness;
            LayoutSize.Height -= horizontalScrollbarThickness;

            if (Children.Count > 0)
            {
                Size size = Size.New (LayoutSize.Width, LayoutSize.Height, Orientation);

                if (Wrap)
                {
                    // If wrapping, size.Main can't be smaller than ChildrenMin.Main
                    size.Main = Math.Max(ChildrenMin.Main, size.Main);
                    Lines = GetLines(size.Main);
                }
                else
                {
                    Lines = GetLine();
                }

                // Find minimum cross length for each line.
                Size min = Size.New (Orientation);
                min.Main = size.Main;

                Lines.ForEach (line => {
                    min.Cross = line.MinSize.Cross;

                    // Layout with minimum line cross length.
                    LayoutLine(line, min);
                    line.ProbedUsedSize = line.LayoutSize;
                });

                Compute.SetLinesSize (Lines, size);

                Lines.ForEach (line => {
                    // Layout with final cross lengths.
                    LayoutLine (line, line.FinalSize);

                    contentSize.Main = Math.Max (contentSize.Main, line.LayoutSize.Main);
                    contentSize.Cross += line.LayoutSize.Cross;
                });
            }

            // Make sure content size isn't smaller than UserMinSize.
            ContentSize.Width = Math.Max (contentSize.Width, UserMinSize.Width - verticalScrollbarThickness);
            ContentSize.Height = Math.Max (contentSize.Height, UserMinSize.Height - horizontalScrollbarThickness);

            LayoutSize.Width += verticalScrollbarThickness;
            LayoutSize.Height += horizontalScrollbarThickness;
        }

        protected void LayoutLine (Line line, Size lineSize)
        {
            Size size = Size.New (line.Orientation);

            Compute.SetMainLengths (line.Orientation, line.Children, lineSize.Main);

            for (int i = 0; i < line.Children.Count; i++)
            {
                Box child = line.Children[i];

                size.Main = child.Computed.MainLength;
                size.Cross = Math.Max (lineSize.Cross, line.MinSize.Cross);

                child.Layout(size.Width, size.Height);

                // Cross size is largest minimum for all children on this line, but it shouldn't be used unless cross expand is true.
                if (child.Expand.GetCross (line.Orientation) == false &&
                    child.ContentSize.GetCross (line.Orientation) < child.LayoutSize.GetCross (line.Orientation))
                {
                    size.Main = child.Computed.MainLength;
                    size.Cross = child.ContentSize.GetCross (line.Orientation);
                    child.Layout(size.Width, size.Height);
                }
            }
        }
    }
}