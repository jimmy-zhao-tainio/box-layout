using System;
using UI.Structures;
using UI.Controls;

namespace UI.Layout
{
    public partial class LayoutManager
    {
        static private void SetAlignMain (Box box)
        {
            if (box.Lines == null)
                return;
            foreach (Line line in box.Lines)
            {
                Positioning.SetAlignMain (box, line);
                foreach (Box child in line.Children)
                {
                    if (child.Lines == null)
                        continue;
                    SetAlignMain (child);
                }
            }
        }

        static private void SetAlignCross (Box box)
        {
            if (box.Lines == null)
                return;
            foreach (Line line in box.Lines)
            {
                Positioning.AlignCross (box, line);
                foreach (Box child in line.Children)
                {
                    if (child.Lines == null)
                        continue;
                    SetAlignCross (child);
                }
            }
        }
    }
}