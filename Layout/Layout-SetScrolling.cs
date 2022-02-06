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
            SetScrollbarGeometry(box, box.VerticalScrollbar, box.HorizontalScrollbar.Visible);
            SetScrollbarGeometry(box, box.HorizontalScrollbar, box.VerticalScrollbar.Visible);
            box.ScrollAreaSize.Width = box.LayoutSize.Width - box.VerticalScrollbar.Size.Width;
            box.ScrollAreaSize.Height = box.LayoutSize.Height - box.HorizontalScrollbar.Size.Height;
            SetHandleGeometry(box, box.VerticalScrollbar);
            SetHandleGeometry(box, box.HorizontalScrollbar);
        }

        static private void SetScrollbarGeometry(Box box, Scrollbar scrollbar, bool oppositeVisible)
        {
            if (scrollbar.Visible == false)
            {
                scrollbar.Position.X = 0;
                scrollbar.Position.Y = 0;
                scrollbar.Size.Width = 0;
                scrollbar.Size.Height = 0;
            }
            else
            {
                scrollbar.Position.Main = 0;
                scrollbar.Position.Cross = box.LayoutSize.GetCross (scrollbar.Orientation) - ScrollbarSettings.Thickness;
                scrollbar.Size.Main = box.LayoutSize.GetMain (scrollbar.Orientation) - (oppositeVisible ? ScrollbarSettings.Thickness : 0);
                scrollbar.Size.Cross = ScrollbarSettings.Thickness;
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

        static private void SetHandleGeometry(Box box, Structures.Scrollbar scrollbar)
        {
            if (scrollbar.Visible == false)
                return;

            scrollbar.Update (box.ScrollAreaSize.GetMain(scrollbar.Orientation),
                              box.ContentSize.GetMain(scrollbar.Orientation));
            scrollbar.SetHandlePositionByContentOffset();
        }
    }
}