using System;
using UI.Structures;
using UI.Controls;

namespace UI.Layout
{
    public class LayoutManager
    {
        static public void Process(Box top, int width, int height)
        {
            SetMinMainCrossSizes (top);
            LayoutScrolling (top, width, height);
            SetLinePositions (top);
            SetAlignMain (top);
            SetAlignCross (top);
        }

        static private void SetMinMainCrossSizes (Box box)
        {
            // Recursively set minimum main and cross lengths. 
            foreach (Box child in box.Children)
                SetMinMainCrossSizes (child);

            box.ChildrenMin = Size.New (box.Orientation);
            foreach (Box child in box.Children)
            {
                // Catch anything that sticks out in either direction.
                if (box.ChildrenMin.Main < box.UserMaxSize.Main)
                {
                    if (box.Wrap == false)
                        box.ChildrenMin.Main += child.Min.GetMain (box.Orientation);
                    else
                        box.ChildrenMin.Main = Math.Max (box.ChildrenMin.Main, child.Min.GetMain (box.Orientation));
                }
                if (box.ChildrenMin.Cross < box.UserMaxSize.Cross)
                {
                    box.ChildrenMin.Cross = Math.Max (box.ChildrenMin.Cross, child.Min.GetCross (box.Orientation));
                }
            }
            box.Min = Size.New (box.ChildrenMin.Width, box.ChildrenMin.Height, box.Orientation);

            // Check if minimum possible main is smaller than user set minimum. If so use UserMinimum.
            box.Min.Main = Math.Max (box.Min.Main, box.UserMinSize.Main);
            // Check if minimum main is larger than UserMaximum. If so use UserMaximum.
            box.Min.Main = Math.Min (box.Min.Main, box.UserMaxSize.Main);
            // Same as above.
            box.Min.Cross = Math.Max (box.Min.Cross, box.UserMinSize.Cross);
            box.Min.Cross = Math.Min (box.Min.Cross, box.UserMaxSize.Cross);
        }

        static private void LayoutScrolling(Box box, int width, int height)
        {
            LayoutLines(box, width, height, 0, 0);

            SetScrollbarVisibility(box.VerticalScrollbar, box.ActualSize.Height, height);
            SetScrollbarVisibility(box.HorizontalScrollbar, box.ActualSize.Width, width);

            if (box.HorizontalScrollbar.Visible && box.VerticalScrollbar.Visible)
            {
                LayoutLines(box, width, height, ScrollbarSettings.Thickness, ScrollbarSettings.Thickness);
            }
            else if (box.VerticalScrollbar.Visible)
            {
                LayoutLines(box, width, height, 0, ScrollbarSettings.Thickness);
                SetScrollbarVisibility(box.HorizontalScrollbar, box.ActualSize.Width, width - ScrollbarSettings.Thickness);
                if (box.HorizontalScrollbar.Visible)
                    LayoutLines(box, width, height, ScrollbarSettings.Thickness, ScrollbarSettings.Thickness);
            }
            else if (box.HorizontalScrollbar.Visible)
            {
                LayoutLines(box, width, height, ScrollbarSettings.Thickness, 0);
                SetScrollbarVisibility(box.VerticalScrollbar, box.ActualSize.Height, height - ScrollbarSettings.Thickness);
                if (box.VerticalScrollbar.Visible)
                    LayoutLines(box, width, height, ScrollbarSettings.Thickness, ScrollbarSettings.Thickness);
            }
            SetScrollbarGeometry(box.VerticalScrollbar, box.LayoutSize, box.HorizontalScrollbar.Visible);
            SetScrollbarGeometry(box.HorizontalScrollbar, box.LayoutSize, box.VerticalScrollbar.Visible);
        }

        static private void SetScrollbarGeometry(Scrollbar scrollbar, Size layoutSize, bool oppositeVisible)
        {
            if (scrollbar.Visible == false)
            {
                scrollbar.Position.X = 0;
                scrollbar.Position.Y = 0;
                scrollbar.Size.Width = 0;
                scrollbar.Size.Height = 0;
            }
            else if (scrollbar is HScrollbar)
            {
                scrollbar.Position.X = 0;
                scrollbar.Position.Y = layoutSize.Height - ScrollbarSettings.Thickness;
                scrollbar.Size.Width = layoutSize.Width - (oppositeVisible ? ScrollbarSettings.Thickness : 0);
                scrollbar.Size.Height = ScrollbarSettings.Thickness;
            }
            else if (scrollbar is VScrollbar)
            { 
                scrollbar.Position.X = layoutSize.Width - ScrollbarSettings.Thickness;
                scrollbar.Position.Y = 0;
                scrollbar.Size.Width = ScrollbarSettings.Thickness;
                scrollbar.Size.Height = layoutSize.Height - (oppositeVisible ? ScrollbarSettings.Thickness : 0);
            }
        }

        static private void SetScrollbarVisibility(Scrollbar scrollbar, int actualLength, int layoutLength)
        {
            if (scrollbar.Mode == ScrollbarMode.Hidden)
                scrollbar.Visible = false;
            else if (scrollbar.Mode == ScrollbarMode.Visible)
                scrollbar.Visible = true;
            else if (scrollbar.Mode == ScrollbarMode.Auto)
                scrollbar.Visible = actualLength > layoutLength;
        }

        static private void LayoutLines (Box box, int width, int height, int horizontalScrollbarThickness, int verticalScrollbarThickness)
        {
            Size actualSize = Size.New (0, 0, box.Orientation);

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
                    line.ProbedUsedSize = LayoutLine (line, min);
                });

                Compute.SetLinesSize (box.Lines, size);

                box.Lines.ForEach (line => {
                    // Layout with final cross lengths.
                    Size used = LayoutLine (line, line.FinalSize);

                    actualSize.Main = Math.Max (actualSize.Main, used.Main);
                    actualSize.Cross += used.Cross;
                });
            }
            // Make sure actual size isn't smaller than UserMinSize.
            box.ActualSize.Main = Math.Max (actualSize.Main, box.UserMinSize.Main);
            box.ActualSize.Cross = Math.Max (actualSize.Cross, box.UserMinSize.Cross);

            box.LayoutSize.Width += verticalScrollbarThickness;
            box.LayoutSize.Height += horizontalScrollbarThickness;
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

                LayoutScrolling(child, size.Width, size.Height);

                // Cross size is largest minimum for all children on this line, but it shouldn't be used unless cross expand is true.
                if (child.Expand.GetCross (line.Orientation) == false &&
                    child.ActualSize.GetCross (line.Orientation) < child.LayoutSize.GetCross (line.Orientation))
                {
                    size.Main = child.Computed.MainLength;
                    size.Cross = child.ActualSize.GetCross (line.Orientation);
                    LayoutScrolling(child, size.Width, size.Height);
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
                Positioning.SetAlignMain (box, line);
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