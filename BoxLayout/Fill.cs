using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boxing 
{
    public struct Fill
    {
        public int Length;
        public int Remainder;
        public int Index;

        static public Fill New(int itemCount, int itemLength, int availableLength)
        {
            Fill fill;
            int difference = availableLength - itemLength;

            if (difference <= 0 || itemCount == 0)
            {
                fill.Length = 0;
                fill.Remainder = 0;
                fill.Index = 0;
                return fill;
            }
            fill.Length = difference / itemCount;
            fill.Remainder = difference % itemCount;
            fill.Index = 0;
            return fill;
        }

        public int Next()
        {
            if (Index++ < Remainder)
                return Length + 1;
            return Length;
        }
    }
}
