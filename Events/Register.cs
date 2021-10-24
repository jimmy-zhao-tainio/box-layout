using System.Diagnostics;
using UI.Controls;

namespace UI
{
    public partial class Events
    {
        static private string debugLine = null;
        static Structures.Point relativePoint = Structures.Point.New(0, 0);

        static public void RegisterMouseMove(int x, int y, Box top)
        {
            Box found = Events.FindControl(x, y, top, ref relativePoint);

            if (found == null)
            {
                PrintDebug("RegisterMouseMove: null");
                return;
            }

            PrintDebug(string.Format("RegisterMouseMove: {0}, {1}, {2}", found.GetHashCode(), found.LayoutSize.Width, found.LayoutSize.Height));
        }

        static public void RegisterMouseDown(int x, int y, Box top)
        {
            Box found = Events.FindControl(x, y, top, ref relativePoint);
            if (found == null)
                return;
            if (found.HorizontalScrollbar.AtPoint(relativePoint))
                RegisterMouseDownScrollbar(found, found.HorizontalScrollbar, relativePoint);
            else if (found.VerticalScrollbar.AtPoint(relativePoint))
                RegisterMouseDownScrollbar(found, found.VerticalScrollbar, relativePoint);
        }

        static public void RegisterMouseUp(int x, int y, Box top)
        {
            Box found = Events.FindControl(x, y, top, ref relativePoint);
            if (found == null)
                return;
        }

        static public void RegisterMouseLose(Box top)
        {
        }

        static private void RegisterMouseDownScrollbar(Box box, Structures.Scrollbar scrollbar, Structures.Point relativePoint)
        {
            if (scrollbar.HandleAtPoint(relativePoint))
            {
            }
            else
            { 
            }
        }

        static private void PrintDebug(string line)
        { 
            if (debugLine == line)
                return;
            debugLine = line;
            Debug.WriteLine(debugLine);
        }
    }
}
