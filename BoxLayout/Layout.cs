using System;
using System.Collections.Generic;
using System.Linq;

namespace Boxing
{
    public class Layout
    {
        static public void Run (Box top, int width, int height)
        {
            top.Layout (width, height);
            top.LayoutAlignMain ();
            top.LayoutAlignCross ();
        }
    }
}