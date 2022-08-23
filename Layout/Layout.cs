using System;
using UI.Structures;
using UI.Controls;

namespace UI.Layout
{
    public partial class LayoutManager
    {
        static public void Process(Box top, int width, int height, bool onlyScrolling = false)
        {
            if (onlyScrolling)
            {
                top.SetScrollingHandles ();
                return;
            }
            top.SetMinSizes ();
            top.Layout (width, height);
            top.SetScrollingHandles ();
            top.AlignLines();
            top.AlignLinesContent();
        }
    }
}