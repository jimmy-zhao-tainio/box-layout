using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boxing
{
    public enum AlignMain
    {
        Start,
        Center,
        End,
        SpaceEvenly,
        SpaceBetween,
        SpaceAround
    }

    public enum AlignCross
    {
        Start,
        Center,
        End
    }

    public enum LineAlignCross
    {
        Start,
        Center,
        End,
        SpaceEvenly,
        SpaceBetween,
        SpaceAround
    }

    public enum SelfAlignCross
    {
        Inherit,
        Start,
        Center,
        End
    }
}