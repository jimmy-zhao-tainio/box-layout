using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace UI
{
    public class Render
    {
        static Dictionary<Controls.Box, SolidBrush> brushes = null;

        static Random random = new Random ();
        static void CreateBrushForBox (Controls.Box box)
        {
            if (box.Children.Count == 0)
            {
                int r = random.Next (256 - 50) + 50;
                int g = random.Next (256 - 50) + 50;
                int b = random.Next (256 - 50) + 50;
                brushes.Add (box, new SolidBrush (Color.FromArgb (r, g, b)));
            }
            for (int i = 0; i < box.Children.Count; i++)
                CreateBrushForBox (box.Children[i]);
        }

        static public void Box(Structures.Point position, Controls.Box box, Graphics graphics)
        { 
            if (box.LayoutSize.Width == 0 || box.LayoutSize.Height == 0)
                return;
            UI.Structures.Point absolute = UI.Structures.Point.New (position.X + box.LayoutPosition.X,
                                                                    position.Y + box.LayoutPosition.Y,
                                                                    UI.Structures.Orientation.Horizontal);

            if (brushes == null)
            {
                brushes = new Dictionary<Controls.Box, SolidBrush>();
                CreateBrushForBox(box);
            }
            if (brushes.TryGetValue (box, out SolidBrush brush))
                graphics.FillRectangle (brush, 
                                        absolute.X, 
                                        absolute.Y, 
                                        box.LayoutSize.Width, 
                                        box.LayoutSize.Height);
            for (int i = 0; i < box.Children.Count; i++)
                Render.Box (absolute, box.Children[i], graphics);
            if (box.HorizontalScrollbar.Visible)
                Render.HorizontalScrollbar (graphics, absolute, box);
            if (box.VerticalScrollbar.Visible)
                Render.VerticalScrollbar (graphics, absolute, box);
            if (box.HorizontalScrollbar.Visible && box.VerticalScrollbar.Visible)
                Render.CornerScrollbar(graphics, absolute, box);
        }
        
        static private void HorizontalScrollbar(Graphics graphics, Structures.Point offset, Controls.Box box)
        {
            Structures.HScrollbar scrollbar = box.HorizontalScrollbar;
            int handleLength = (scrollbar.Size.Width * (box.LayoutSize.Width - (box.VerticalScrollbar.Visible ? 20 : 0)) / box.ActualSize.Width) - 6;

            graphics.FillRectangle (new SolidBrush (Color.LightGray), 
                                    scrollbar.Position.X + offset.X, 
                                    scrollbar.Position.Y + offset.Y, 
                                    scrollbar.Size.Width, 
                                    scrollbar.Size.Height);
            graphics.FillRectangle (new SolidBrush (Color.Gray), 
                                    scrollbar.Position.X + offset.X + 3, 
                                    scrollbar.Position.Y + offset.Y + 2, 
                                    handleLength, 
                                    scrollbar.Size.Height - 4);
        }

        static private void VerticalScrollbar(Graphics graphics, Structures.Point offset, Controls.Box box)
        {
            Structures.VScrollbar scrollbar = box.VerticalScrollbar;
            int handleLength = (scrollbar.Size.Height * (box.LayoutSize.Height - (box.HorizontalScrollbar.Visible ? 20 : 0)) / box.ActualSize.Height) - 6;

            graphics.FillRectangle (new SolidBrush (Color.LightGray), 
                                    scrollbar.Position.X + offset.X, 
                                    scrollbar.Position.Y + offset.Y, 
                                    scrollbar.Size.Width, 
                                    scrollbar.Size.Height);
            graphics.FillRectangle (new SolidBrush (Color.Gray), 
                                    scrollbar.Position.X + offset.X + 2, 
                                    scrollbar.Position.Y + offset.Y + 3, 
                                    scrollbar.Size.Width - 4, 
                                    handleLength);
        }

        static private void CornerScrollbar(Graphics graphics, Structures.Point offset, Controls.Box box)
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
