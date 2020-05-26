using System;
using System.Collections.Generic;
using System.Linq;

namespace Boxing
{
    public class Layout
    {
        public static void Process (Box top, int width, int height)
        {
            // Limit layout size by UserMaxSize.
            width = Math.Min (width, top.UserMaxSize.Width);
            height = Math.Min (height, top.UserMaxSize.Height);

            MinimumSize.SetMinimumSize (top);
            LayoutAlgorithm.Layout (top, width, height);
        }
    }
}