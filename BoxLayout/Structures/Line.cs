using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boxing
{
    public class Line
    {
        public Orientation Orientation;
        // Largest main length given this line.
        public int MaxLength;

        public List<Box> Children = new List<Box> ();

        // Combined minimum size for children.
        public Size MinSize;

        // Largest maximum user size (main and cross), if any.
        public Size UserMaxSize;

        // If any child expands (main and cross)
        public Expand Expand;

        // Used size when given minimum cross length.
        public Size ProbedUsedSize;

        // Final size
        public Size FinalSize;

        // Final position
        public Point FinalPosition;

        public Line (Orientation orientation, int maxLength)
        {
            Orientation = orientation;
            MaxLength = maxLength;
            Children = new List<Box> ();
            MinSize = Size.New (orientation);
            UserMaxSize = Size.New (Int32.MaxValue, Int32.MaxValue, orientation);
            Expand = Expand.New (orientation);
        }

        public bool CanAdd (Box box)
        {
            if (MinSize.Main + box.Min.GetMain (Orientation) > MaxLength)
                return false;
            return true;
        }

        public bool CanAdd (int mainLength)
        {
            if (MinSize.Main + mainLength > MaxLength)
                return false;
            return true;
        }

        public void Add (Box box)
        {
            Children.Add (box);

            MinSize.Main += box.Min.GetMain (Orientation);
            MinSize.Cross = Math.Max (MinSize.Cross, box.Min.GetCross (Orientation));

            Expand.Main = Expand.Main || box.Expand.GetMain (Orientation);
            Expand.Cross = Expand.Cross || box.Expand.GetCross (Orientation);

            if (box.UserMaxSize.GetMain (Orientation) != Int32.MaxValue)
            {
                if (UserMaxSize.Main == Int32.MaxValue)
                    UserMaxSize.Main = box.UserMaxSize.GetMain (Orientation);
                else
                    UserMaxSize.Main = Math.Max (UserMaxSize.Main, box.UserMaxSize.GetMain (Orientation));
            }

            if (box.UserMaxSize.GetCross (Orientation) != Int32.MaxValue)
            {
                if (UserMaxSize.Cross == Int32.MaxValue)
                    UserMaxSize.Cross = box.UserMaxSize.GetCross (Orientation);
                else
                    UserMaxSize.Cross = Math.Max (UserMaxSize.Cross, box.UserMaxSize.GetCross (Orientation));
            }
        }
    }
}