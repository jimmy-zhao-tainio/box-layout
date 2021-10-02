using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Controls;
using UI;

namespace UI
{
    public partial class Events
    {
        static public Box FindControl(int x, int y, Box box)
        {
            return FindControl(new Structures.HPoint (x, y), box);
        }

        static public Box FindControl(Structures.Point point, Box box)
        {
            Structures.Point relative = UI.Structures.Point.New (point.X - box.LayoutPosition.X,
                                                                 point.Y - box.LayoutPosition.Y,
                                                                 Structures.Orientation.Horizontal);

            if (relative.X < 0 || relative.Y < 0)
                return null;
            if (relative.X >= box.LayoutSize.Width || 
                relative.Y >= box.LayoutSize.Height)
                return null;

            foreach (Box child in box.Children)
            {
                Box found = FindControl(relative, child);
                if (found != null)
                    return found;
            }
            return box;
        }
    }
}
