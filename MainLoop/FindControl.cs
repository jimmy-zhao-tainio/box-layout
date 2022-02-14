using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Controls;
using UI;

namespace UI
{
    public partial class MainLoop
    {
        static Structures.Point point = Structures.Point.New(0, 0);

        static public Box FindControl(int x, int y, Box box, ref Structures.Point relativePoint)
        {
            point.X = x - box.LayoutPosition.X;
            point.Y = y - box.LayoutPosition.Y;

            if (point.X < 0 || 
                point.Y < 0 ||
                point.X >= box.LayoutSize.Width || 
                point.Y >= box.LayoutSize.Height)
                return null;

            if (box.HorizontalScrollbar.AtPoint(point) == false && box.VerticalScrollbar.AtPoint(point) == false)
            {
                foreach (Box child in box.Children)
                {
                    Box found = FindControl(point.X + box.HorizontalScrollbar.ContentOffset, 
                                            point.Y + box.VerticalScrollbar.ContentOffset, 
                                            child, ref relativePoint);
                    if (found != null)
                        return found;
                }
            }

            relativePoint.X = point.X;
            relativePoint.Y = point.Y;
            return box;
        }
    }
}
