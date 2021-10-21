using System;
using UI.Structures;
using UI.Controls;

namespace UI.Layout
{
    public partial class LayoutManager
    {
        static private void SetScrolling(Box box, int width, int height)
        {
            LayoutLines(box, width, height, 0, 0);

            SetScrollbarVisibility(box.VerticalScrollbar, box.ContentSize.Height, box.LayoutSize.Height);
            SetScrollbarVisibility(box.HorizontalScrollbar, box.ContentSize.Width, box.LayoutSize.Width);

            if (box.HorizontalScrollbar.Visible && box.VerticalScrollbar.Visible)
            {
                LayoutLines(box, width, height, ScrollbarSettings.Thickness, ScrollbarSettings.Thickness);
            }
            else if (box.VerticalScrollbar.Visible)
            {
                LayoutLines(box, width, height, 0, ScrollbarSettings.Thickness);
                SetScrollbarVisibility(box.HorizontalScrollbar, box.ContentSize.Width, box.LayoutSize.Width - ScrollbarSettings.Thickness);
                if (box.HorizontalScrollbar.Visible)
                    LayoutLines(box, width, height, ScrollbarSettings.Thickness, ScrollbarSettings.Thickness);
            }
            else if (box.HorizontalScrollbar.Visible)
            {
                LayoutLines(box, width, height, ScrollbarSettings.Thickness, 0);
                SetScrollbarVisibility(box.VerticalScrollbar, box.ContentSize.Height, box.LayoutSize.Height - ScrollbarSettings.Thickness);
                if (box.VerticalScrollbar.Visible)
                    LayoutLines(box, width, height, ScrollbarSettings.Thickness, ScrollbarSettings.Thickness);
            }
            SetScrollbarGeometry(box.VerticalScrollbar, box.LayoutSize, box.HorizontalScrollbar.Visible);
            SetScrollbarGeometry(box.HorizontalScrollbar, box.LayoutSize, box.VerticalScrollbar.Visible);
            box.ScrollSize.Width = box.LayoutSize.Width - box.VerticalScrollbar.Size.Width;
            box.ScrollSize.Height = box.LayoutSize.Height - box.HorizontalScrollbar.Size.Height;
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

        static private void SetScrollbarVisibility(Scrollbar scrollbar, int contentLength, int layoutLength)
        {
            if (scrollbar.Mode == ScrollbarMode.Hidden)
                scrollbar.Visible = false;
            else if (scrollbar.Mode == ScrollbarMode.Visible)
                scrollbar.Visible = true;
            else if (scrollbar.Mode == ScrollbarMode.Auto)
                scrollbar.Visible = contentLength > layoutLength;
        }
    }
}