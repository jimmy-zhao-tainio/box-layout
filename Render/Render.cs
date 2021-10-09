using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using UI.Structures;

namespace UI
{
    public class Render
    {
        static Dictionary<Controls.Box, SolidBrush> brushes = new Dictionary<Controls.Box, SolidBrush> ();

        static Random random = new Random ();

        static SolidBrush CreateBrushForBox (Controls.Box box)
        {
            SolidBrush brush = null;

            if (box.Children.Count == 0)
            {
                int r = random.Next (256 - 50) + 50;
                int g = random.Next (256 - 50) + 50;
                int b = random.Next (256 - 50) + 50;
                brush = new SolidBrush(Color.FromArgb(r, g, b));
            }
            for (int i = 0; i < box.Children.Count; i++)
                CreateBrushForBox (box.Children[i]);
            return brush;
        }

        static public void RenderBox(Structures.Point position, Controls.Box box, Graphics graphics)
        {
            if (box.LayoutSize.Width == 0 || box.LayoutSize.Height == 0)
                return;

            UI.Structures.Point absolute = UI.Structures.Point.New (position.X + box.LayoutPosition.X,
                                                                    position.Y + box.LayoutPosition.Y,
                                                                    UI.Structures.Orientation.Horizontal);

            if (brushes.TryGetValue(box, out SolidBrush brush) == false)
                brushes[box] = brush = CreateBrushForBox(box);
            if (brush != null)
                graphics.FillRectangle(brush,
                                        absolute.X,
                                        absolute.Y,
                                        box.LayoutSize.Width,
                                        box.LayoutSize.Height);
            foreach (Controls.Box child in box.Children)
                Render.RenderBox(absolute, child, graphics);
            if (box.HorizontalScrollbar.Visible)
                Render.RenderHorizontalScrollbar (graphics, absolute, box);
            if (box.VerticalScrollbar.Visible)
                Render.RenderVerticalScrollbar (graphics, absolute, box);
            if (box.HorizontalScrollbar.Visible && box.VerticalScrollbar.Visible)
                Render.RenderCornerScrollbar(graphics, absolute, box);
        }
        
        static private void RenderHorizontalScrollbar(Graphics graphics, Structures.Point offset, Controls.Box box)
        {
            Structures.HScrollbar scrollbar = box.HorizontalScrollbar;
            int handleLength = ScrollbarHandleLength(scrollbar.Size.Width, box.LayoutSize.Width, box.ActualSize.Width, 
                                                     box.VerticalScrollbar.Visible);

            graphics.FillRectangle (new SolidBrush (Color.LightGray), 
                                    scrollbar.Position.X + offset.X, 
                                    scrollbar.Position.Y + offset.Y, 
                                    scrollbar.Size.Width, 
                                    scrollbar.Size.Height);
            graphics.FillRectangle (new SolidBrush (Color.Gray), 
                                    scrollbar.Position.X + offset.X + ScrollbarSettings.LengthPadding, 
                                    scrollbar.Position.Y + offset.Y + ScrollbarSettings.SidePadding, 
                                    handleLength, 
                                    scrollbar.Size.Height - ScrollbarSettings.SidePadding * 2);
        }

        static private void RenderVerticalScrollbar(Graphics graphics, Structures.Point offset, Controls.Box box)
        {
            Structures.VScrollbar scrollbar = box.VerticalScrollbar;
            int handleLength = ScrollbarHandleLength(scrollbar.Size.Height, box.LayoutSize.Height, box.ActualSize.Height, 
                                                     box.HorizontalScrollbar.Visible);

            graphics.FillRectangle (new SolidBrush (Color.LightGray), 
                                    scrollbar.Position.X + offset.X, 
                                    scrollbar.Position.Y + offset.Y, 
                                    scrollbar.Size.Width, 
                                    scrollbar.Size.Height);
            graphics.FillRectangle (new SolidBrush (Color.Gray), 
                                    scrollbar.Position.X + offset.X + ScrollbarSettings.SidePadding, 
                                    scrollbar.Position.Y + offset.Y + ScrollbarSettings.LengthPadding, 
                                    scrollbar.Size.Width - ScrollbarSettings.SidePadding * 2, 
                                    handleLength);
        }

        static private int ScrollbarHandleLength(int scrollbarLength, int boxLayoutLength, int boxActualLength, bool oppositeScrollbar)
        {
            int handleLength;

            handleLength = scrollbarLength * (boxLayoutLength - (oppositeScrollbar ? ScrollbarSettings.Thickness : 0));
            handleLength /= boxActualLength;
            handleLength -= ScrollbarSettings.LengthPadding * 2;

            if (handleLength < ScrollbarSettings.Thickness)
            {
                if (scrollbarLength >= ScrollbarSettings.MinHandleLength + ScrollbarSettings.LengthPadding * 2)
                    handleLength = ScrollbarSettings.Thickness;
                else
                    handleLength = scrollbarLength - ScrollbarSettings.LengthPadding * 2;
            }
            return handleLength;
        }

        static private void RenderCornerScrollbar(Graphics graphics, Structures.Point offset, Controls.Box box)
        {
            Structures.VScrollbar vscrollbar = box.VerticalScrollbar;
            Structures.HScrollbar hscrollbar = box.HorizontalScrollbar;

            graphics.FillRectangle(new SolidBrush(Color.LightGray),
                                    vscrollbar.Position.X + offset.X,
                                    hscrollbar.Position.Y + offset.Y,
                                    vscrollbar.Size.Width,
                                    hscrollbar.Size.Height);
        }
    }
}
