using System.Diagnostics;
using UI.Controls;

namespace UI
{
    public partial class Events
    {
        static public void RegisterMouseMove(int x, int y, Box top)
        {
            Box found = Events.FindControl(new Structures.HPoint (x, y), top);

            if (found == null)
                return;

            Debug.WriteLine("Found control {0}, {1}", found.LayoutSize.Width, found.LayoutSize.Height);
        }
    }
}
