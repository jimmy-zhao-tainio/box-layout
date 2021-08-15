﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Boxing
{
    public class Layout
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
            // Find minimum recursive main and cross lengths. 
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
            int scrollbarThickness = 20;
            LayoutLines(box, width, height, 0, 0);

            SetScrollbarVisibility(box.VerticalScrollbar, box.ActualSize.Height, height);
            SetScrollbarVisibility(box.HorizontalScrollbar, box.ActualSize.Width, width);

            if (box.HorizontalScrollbar.Visible && box.VerticalScrollbar.Visible)
            {
                LayoutLines(box, width, height, scrollbarThickness, scrollbarThickness);
            }
            else if (box.VerticalScrollbar.Visible)
            {
                LayoutLines(box, width, height, 0, scrollbarThickness);
                SetScrollbarVisibility(box.HorizontalScrollbar, box.ActualSize.Width, width - scrollbarThickness);
                if (box.HorizontalScrollbar.Visible)
                    LayoutLines(box, width, height, scrollbarThickness, scrollbarThickness);
            }
            else if (box.HorizontalScrollbar.Visible)
            {
                LayoutLines(box, width, height, scrollbarThickness, 0);
                SetScrollbarVisibility(box.VerticalScrollbar, box.ActualSize.Height, height - scrollbarThickness);
                if (box.VerticalScrollbar.Visible)
                    LayoutLines(box, width, height, scrollbarThickness, scrollbarThickness);
            }
            SetScrollbarGeometry(box.VerticalScrollbar, box.LayoutSize, scrollbarThickness);
            SetScrollbarGeometry(box.HorizontalScrollbar, box.LayoutSize, scrollbarThickness);
        }

        static private void SetScrollbarGeometry(Scrollbar scrollbar, Size layoutSize, int scrollbarThickness)
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
                scrollbar.Position.Y = layoutSize.Height - scrollbarThickness;
                scrollbar.Size.Width = layoutSize.Width;
                scrollbar.Size.Height = scrollbarThickness;
            }
            else if (scrollbar is VScrollbar)
            { 
                scrollbar.Position.X = layoutSize.Width - scrollbarThickness;
                scrollbar.Position.Y = 0;
                scrollbar.Size.Width = scrollbarThickness;
                scrollbar.Size.Height = layoutSize.Height;
            }
        }

        static private void SetScrollbarVisibility(Scrollbar scrollbar, int actualLength, int layoutLength)
        {
            if (scrollbar.Mode == ScrollbarMode.Hidden)
                scrollbar.Visible = false;
            else if (scrollbar.Mode == ScrollbarMode.Visible)
                scrollbar.Visible = true;
            else //if (scrollbar.Mode == Scrollbar.Auto)
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

                if (box.Wrap)
                    box.Lines = lines.GetLines (box, box.Children, size.Main);
                else
                    box.Lines = lines.GetLine (box.Orientation, box.Children);

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

        /*
        static private void LayoutScrollbars (Box box)
        {
            int scrollbarThickness = 20;

            SetScrollbarVisibility(box.HorizontalScrollbar, box.ActualSize.Width, box.LayoutSize.Width);
            SetScrollbarVisibility(box.VerticalScrollbar, box.ActualSize.Height, box.LayoutSize.Height);

            if (box.HorizontalScrollbar.Visible == true || box.VerticalScrollbar.Visible == true)
            {
                LayoutLines(box,
                            box.LayoutSize.Width - (box.HorizontalScrollbar.Visible ? scrollbarThickness : 0),
                            box.LayoutSize.Height - (box.VerticalScrollbar.Visible ? scrollbarThickness : 0));

                if ((box.HorizontalScrollbar.Visible == false && GetScrollbarVisibility(box.HorizontalScrollbar, box.ActualSize.Width, box.LayoutSize.Width) == true) ||
                    (box.VerticalScrollbar.Visible == false && GetScrollbarVisibility(box.VerticalScrollbar, box.ActualSize.Height, box.LayoutSize.Height) == true))
                {
                    // Scrollbar in one direction caused the need for a scrollbar in the other direction.
                    SetScrollbarVisibility(box.HorizontalScrollbar, box.ActualSize.Width, box.LayoutSize.Width);
                    SetScrollbarVisibility(box.VerticalScrollbar, box.ActualSize.Height, box.LayoutSize.Height);
                    // LayoutSize is decreased twice for at least one of them!!!
                    LayoutLines(box,
                                box.LayoutSize.Width - (box.HorizontalScrollbar.Visible ? scrollbarThickness : 0),
                                box.LayoutSize.Height - (box.VerticalScrollbar.Visible ? scrollbarThickness : 0));
                }
                SetScrollbarGeometry(box.HorizontalScrollbar, 0, box.LayoutSize.Height, box.LayoutSize.Width, scrollbarThickness);
                SetScrollbarGeometry(box.VerticalScrollbar, box.LayoutSize.Width, 0, scrollbarThickness, box.LayoutSize.Height);
            }

            foreach (Box child in box.Children)
            {
                if (child.Lines == null)
                    continue;
                LayoutScrollbars(child);
            }
        }
        */
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