namespace UI.Structures
{
    public class Size
    {
        public int Width;
        public int Height;

        public virtual int Main { get; set; }
        public virtual int Cross { get; set; }

        public Size() { }

        public Size (int width, int height)
        {
            Width = width;
            Height = height;
        }

        public Size (Size size)
        {
            Width = size.Width;
            Height = size.Height;
        }

        public bool Equals (int width, int height)
        {
            return Width == width && Height == height;
        }

        public static Size New (int width, int height, Orientation orientation)
        {
            if (orientation == Orientation.Horizontal)
                return new HSize (width, height);
            return new VSize (width, height);
        }
        
        public static Size New (string sizeText, Orientation orientation)
        {
            Size size = New(orientation);

            string[] parts = sizeText.Split(',');

            if (parts.Length == 2 && int.TryParse(parts[0], out int width) && int.TryParse(parts[1], out int height))
            {
                size.Width = width;
                size.Height = height;
            }
            return size;
        }

        public static Size New (Orientation orientation)
        {
            return New (0, 0, orientation);
        }

        public void Reset ()
        {
            Main = 0;
            Cross = 0;
        }

        public int GetMain (Orientation orientation)
        {
            if (orientation == Orientation.Horizontal)
                return Width;
            return Height;
        }

        public int GetCross (Orientation orientation)
        {
            if (orientation == Orientation.Horizontal)
                return Height;
            return Width;
        }

        override public string ToString()
        {
            return string.Format("{0}, {1}", Width, Height);
        }
    }

    public class HSize : Size
    {
        public HSize (int width, int height) : base (width, height)
        {
            Main = width;
            Cross = height;
        }

        public override int Main
        {
            get { return Width; }
            set { Width = value; }
        }
     
        public override int Cross
        {
            get { return Height; }
            set { Height = value; }
        }
    }

    public class VSize : Size
    {
        public VSize (int width, int height) : base (width, height)
        {
            Main = height;
            Cross = width;
        }

        public override int Main
        {
            get { return Height; }
            set { Height = value; }
        }
     
        public override int Cross
        {
            get { return Width; }
            set { Width = value; }
        }
    }
}