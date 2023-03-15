namespace UI.Structures
{
    public class Expand
    {
        public bool Horizontal;
        public bool Vertical;

        public virtual bool Main { get; set; }
        public virtual bool Cross { get; set; }

        public Expand() { }

        public Expand (bool horizontal, bool vertical)
        {
            Horizontal = horizontal;
            Vertical = vertical;
        }

        public Expand(Expand expand)
        {
            Horizontal = expand.Horizontal;
            Vertical = expand.Vertical;
        }

        public bool Equals (bool horizontal, bool vertical)
        {
            return Horizontal == horizontal && Vertical == vertical;
        }

        public static Expand New (bool horizontal, bool vertical, Orientation orientation)
        {
            if (orientation == Orientation.Horizontal)
                return new HExpand(horizontal, vertical);
            return new VExpand (horizontal, vertical);
        }

        public static Expand New (Orientation orientation)
        {
            return New (false, false, orientation);
        }

        public void Reset ()
        {
            Main = false;
            Cross = false;
        }

        public bool GetMain (Orientation orientation)
        {
            if (orientation == Orientation.Horizontal)
                return Horizontal;
            return Vertical;
        }

        public bool GetCross (Orientation orientation)
        {
            if (orientation == Orientation.Horizontal)
                return Vertical;
            return Horizontal;
        }
    }

    public class HExpand : Expand
    {
        public HExpand (bool horizontal, bool vertical) : base (horizontal, vertical)
        {
            Main = horizontal;
            Cross = vertical;
        }

        public override bool Main
        {
            get { return Horizontal; }
            set { Horizontal = value; }
        }
     
        public override bool Cross
        {
            get { return Vertical; }
            set { Vertical = value; }
        }
    }

    public class VExpand : Expand
    {
        public VExpand (bool horizontal, bool vertical) : base (horizontal, vertical)
        {
            Main = vertical;
            Cross = horizontal;
        }

        public override bool Main
        {
            get { return Vertical; }
            set { Vertical = value; }
        }
     
        public override bool Cross
        {
            get { return Horizontal; }
            set { Horizontal = value; }
        }
    }
}