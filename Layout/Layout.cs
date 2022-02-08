using System;
using UI.Structures;
using UI.Controls;

namespace UI.Layout
{
    public partial class LayoutManager
    {
        static public void Process(Box top, int width, int height)
        {
            SetMinMainCrossSizes (top);
            SetScrolling (top, width, height);
            SetScrollingHandles (top);
            SetLinePositions (top);
            SetAlignMain (top);
            SetAlignCross (top);
        }
    }
}