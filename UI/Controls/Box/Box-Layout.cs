using UI.Structures;

namespace UI.Controls
{
    public partial class Box
    {
        public void Layout(int width, int height)
        {
            LayoutLines(width, height, 0, 0);

            SetScrollbarVisibility(VerticalScrollbar, ContentSize.Height, LayoutSize.Height);
            SetScrollbarVisibility(HorizontalScrollbar, ContentSize.Width, LayoutSize.Width);

            if (HorizontalScrollbar.Visible && VerticalScrollbar.Visible)
            {
                LayoutLines(width, height, ScrollbarSettings.Thickness, ScrollbarSettings.Thickness);
            }
            else if (VerticalScrollbar.Visible)
            {
                LayoutLines(width, height, 0, ScrollbarSettings.Thickness);
                SetScrollbarVisibility(HorizontalScrollbar, ContentSize.Width, LayoutSize.Width - ScrollbarSettings.Thickness);
                if (HorizontalScrollbar.Visible)
                    LayoutLines(width, height, ScrollbarSettings.Thickness, ScrollbarSettings.Thickness);
            }
            else if (HorizontalScrollbar.Visible)
            {
                LayoutLines(width, height, ScrollbarSettings.Thickness, 0);
                SetScrollbarVisibility(VerticalScrollbar, ContentSize.Height, LayoutSize.Height - ScrollbarSettings.Thickness);
                if (VerticalScrollbar.Visible)
                    LayoutLines(width, height, ScrollbarSettings.Thickness, ScrollbarSettings.Thickness);
            }
            SetScrollbarGeometry( VerticalScrollbar, HorizontalScrollbar.Visible);
            SetScrollbarGeometry( HorizontalScrollbar, VerticalScrollbar.Visible);
            ScrollAreaSize.Width = LayoutSize.Width - VerticalScrollbar.Size.Width;
            ScrollAreaSize.Height = LayoutSize.Height - HorizontalScrollbar.Size.Height;
        }

        public void SetScrollingHandles()
        {
            foreach (Box child in Children)
                child.SetScrollingHandles();
            SetHandleGeometry(VerticalScrollbar);
            SetHandleGeometry(HorizontalScrollbar);
        }

        private void SetScrollbarGeometry(Scrollbar scrollbar, bool oppositeVisible)
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
                scrollbar.Position.Cross = LayoutSize.GetCross (scrollbar.Orientation) - ScrollbarSettings.Thickness;
                scrollbar.Size.Main = LayoutSize.GetMain (scrollbar.Orientation) - (oppositeVisible ? ScrollbarSettings.Thickness : 0);
                scrollbar.Size.Cross = ScrollbarSettings.Thickness;
            }
        }

        private void SetScrollbarVisibility(Scrollbar scrollbar, int contentLength, int layoutLength)
        {
            if (scrollbar.Mode == ScrollbarMode.Hidden)
                scrollbar.Visible = false;
            else if (scrollbar.Mode == ScrollbarMode.Visible)
                scrollbar.Visible = true;
            else if (scrollbar.Mode == ScrollbarMode.Auto)
                scrollbar.Visible = contentLength > layoutLength;
        }

        private void SetHandleGeometry(Structures.Scrollbar scrollbar)
        {
            if (scrollbar.Visible == false)
            {
                scrollbar.ContentOffset = 0;
                return;
            }
            scrollbar.Update (ScrollAreaSize.GetMain(scrollbar.Orientation),
                              ContentSize.GetMain(scrollbar.Orientation));
        }
    }
}