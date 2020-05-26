using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boxing
{
    public class BoxVertical : Box
    {
        public BoxVertical () : base (Orientation.Vertical)
        {
        }

        public BoxVertical (Size userMinSize) : base (Orientation.Vertical)
        {
            UserMinSize = userMinSize;
        }
    }
}