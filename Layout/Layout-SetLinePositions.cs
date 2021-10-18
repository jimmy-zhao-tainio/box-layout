using System;
using UI.Structures;
using UI.Controls;

namespace UI.Layout
{
    public partial class LayoutManager
    {
        static private void SetLinePositions (Box box)
        {
            if (box.Lines == null)
                return;
            Positioning.LineAlignCross (box);
            foreach (Box child in box.Children)
            {
                if (child.Lines == null)
                    continue;
                SetLinePositions (child);
            }
        }
    }
}