using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boxing
{
    public abstract class Point
    {
        public int X;
        public int Y;

        public abstract int Main { get; set; }
        public abstract int Cross { get; set; }

        public Point (int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point(Point size)
        {
            X = size.X;
            Y = size.Y;
        }

        public bool Equals (int x, int y)
        {
            return X == x && Y == y;
        }

        public static Point New (int x, int y, Orientation orientation)
        {
            if (orientation == Orientation.Horizontal)
                return new HPoint (x, y);
            return new VPoint (x, y);
        }

        public static Point New (Orientation orientation)
        {
            return New (0, 0, orientation);
        }

        public int GetMain (Orientation orientation)
        {
            if (orientation == Orientation.Horizontal)
                return X;
            return Y;
        }

        public int GetCross (Orientation orientation)
        {
            if (orientation == Orientation.Horizontal)
                return Y;
            return X;
        }

        public void SetMain (Orientation orientation, int value)
        {
            if (orientation == Orientation.Horizontal)
                X = value;
            else
                Y = value;
        }

        public void SetCross (Orientation orientation, int value)
        {
            if (orientation == Orientation.Horizontal)
                Y = value;
            else
                X = value;
        }
    }

    public class HPoint : Point
    {
        public HPoint (int x, int y) : base (x, y)
        {
            Main = x;
            Cross = y;
        }

        public override int Main
        {
            get { return X; }
            set { X = value; }
        }
     
        public override int Cross
        {
            get { return Y; }
            set { Y = value; }
        }
    }

    public class VPoint : Point
    {
        public VPoint (int x, int y) : base (x, y)
        {
            Main = y;
            Cross = x;
        }

        public override int Main
        {
            get { return Y; }
            set { Y = value; }
        }
     
        public override int Cross
        {
            get { return X; }
            set { X = value; }
        }
    }
}