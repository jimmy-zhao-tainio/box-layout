using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boxing
{
    public class BoxHorizontal : Box
    {
        public BoxHorizontal () : base (Orientation.Horizontal)
        {
        }

        public BoxHorizontal (Size userMinSize) : base (Orientation.Horizontal)
        {
            UserMinSize = userMinSize;
        }
    }
}