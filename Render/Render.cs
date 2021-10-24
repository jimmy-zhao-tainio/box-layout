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

        static SolidBrush CreateBrushForBox(Controls.Box box)
        {
            SolidBrush brush = null;

            if (box.Children.Count == 0)
            {
                byte[] colorBytes = new byte[3];
                colorBytes[0] = (byte)(random.Next(128) + 80);
                colorBytes[1] = (byte)(random.Next(128) + 100);
                colorBytes[2] = (byte)(random.Next(128) + 120);

                brush = new SolidBrush(Color.FromArgb(255, colorBytes[0], colorBytes[1], colorBytes[2]));
            }
            for (int i = 0; i < box.Children.Count; i++)
                CreateBrushForBox (box.Children[i]);
            return brush;
        }

        static public Bitmap RenderBox(Controls.Box box)
        {
            if (box.LayoutSize.Width <= 0 || box.LayoutSize.Height <= 0)
                return null;

            Bitmap bitmap = new Bitmap(box.LayoutSize.Width, box.LayoutSize.Height);
            Graphics graphics = Graphics.FromImage(bitmap);

            if (brushes.TryGetValue(box, out SolidBrush brush) == false)
                brushes[box] = brush = CreateBrushForBox(box);
            if (brush != null)
                graphics.FillRectangle(brush, 0, 0, box.LayoutSize.Width, box.LayoutSize.Height);

            if (box.ContentSize.Width <= box.LayoutSize.Width && box.ContentSize.Height <= box.LayoutSize.Height)
            {
                foreach (Controls.Box child in box.Children)
                {
                    Bitmap childBitmap = Render.RenderBox(child);
                    if (childBitmap != null)
                    {
                        graphics.DrawImage(childBitmap, child.LayoutPosition.X, child.LayoutPosition.Y);
                        childBitmap.Dispose();
                    }
                }
            }
            else
            {
                Bitmap contentBitmap = new Bitmap(box.ContentSize.Width, box.ContentSize.Height);
                Graphics contentGraphics = Graphics.FromImage(contentBitmap);

                foreach (Controls.Box child in box.Children)
                {
                    Bitmap childBitmap = Render.RenderBox(child);
                    if (childBitmap != null)
                    {
                        contentGraphics.DrawImage(childBitmap, child.LayoutPosition.X, child.LayoutPosition.Y);
                        childBitmap.Dispose();
                    }
                }
                graphics.DrawImage(contentBitmap,
                                   Rectangle.FromLTRB(0, 0, 
                                                      box.LayoutSize.Width, 
                                                      box.LayoutSize.Height),
                                   Rectangle.FromLTRB(box.HorizontalScrollbar.Offset, 
                                                      box.VerticalScrollbar.Offset,
                                                      box.HorizontalScrollbar.Offset + box.LayoutSize.Width,
                                                      box.VerticalScrollbar.Offset + box.LayoutSize.Height),
                                   GraphicsUnit.Pixel);

                contentGraphics.Dispose();
                contentBitmap.Dispose();
            }
            if (box.HorizontalScrollbar.Visible)
                Render.RenderScrollbar(graphics, box, box.HorizontalScrollbar);
            if (box.VerticalScrollbar.Visible)
                Render.RenderScrollbar(graphics, box, box.VerticalScrollbar);
            if (box.HorizontalScrollbar.Visible && box.VerticalScrollbar.Visible)
                Render.RenderScrollbarCorner(graphics, box);

            graphics.Dispose();
            return bitmap;
        }
        
        static private void RenderScrollbar(Graphics graphics, Controls.Box box, Structures.Scrollbar scrollbar)
        {
            scrollbar.SetHandleGeometry(box.ContentSize.GetMain(scrollbar.Orientation));

            graphics.FillRectangle (new SolidBrush (Color.LightGray), 
                                    scrollbar.Position.X, 
                                    scrollbar.Position.Y, 
                                    scrollbar.Size.Width, 
                                    scrollbar.Size.Height);
            graphics.FillRectangle (new SolidBrush (Color.Gray), 
                                    scrollbar.HandlePosition.X, 
                                    scrollbar.HandlePosition.Y, 
                                    scrollbar.HandleSize.Width, 
                                    scrollbar.HandleSize.Height);
        }

        static private void RenderScrollbarCorner(Graphics graphics, Controls.Box box)
        {
            graphics.FillRectangle(new SolidBrush(Color.LightGray),
                                    box.VerticalScrollbar.Position.X,
                                    box.HorizontalScrollbar.Position.Y,
                                    box.VerticalScrollbar.Size.Width,
                                    box.HorizontalScrollbar.Size.Height);
        }
    }
}
