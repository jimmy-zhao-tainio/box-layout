using System;
using System.Collections.Generic;
using System.Linq;

namespace Boxing
{
    abstract public class Box : BoxBase
    {
        public Box (Orientation orientation) : base (orientation)
        {
        }
    }
}