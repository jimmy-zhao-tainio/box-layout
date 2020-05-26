using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boxing
{
    public class LinesAlgorithm
    {
        private LinesAlgorithm () { }

        public static List<Line> GetLines (Orientation orientation, List<Box> children, int width, int height)
        {
            List<Line> lines = new List<Line> ();
            Line line;
            int index = 0;

            while (true)
            {
                if (index >= children.Count)
                    return lines;
                line = new Line (orientation, children, width, height, index);
                lines.Add (line);
                index += line.Children.Count;
            }
        }
    }

    public class Line
    {
        public Orientation Orientation;
        public List<Box> Children = new List<Box> ();
        public Size Min;
        public Size UserMaxSize;
        public Expand Expand;

        public Line (Orientation orientation, List<Box> children, int maxWidth, int maxHeight, int index)
        {
            Box child;
            Orientation = orientation;
            Min = Size.New (orientation);

            while (index + Children.Count < children.Count)
            {
                child = children[index + Children.Count];

                if (Children.Count != 0)
                {
                    if ((orientation == Orientation.Horizontal && Min.Width + child.Min.Width > maxWidth) ||
                        (orientation == Orientation.Vertical && Min.Height + child.Min.Height > maxHeight))
                        break;
                }
                if (orientation == Orientation.Horizontal)
                {
                    Min.Width += child.Min.Width;
                    Min.Height = Math.Max (Min.Height, child.Min.Height);
                }
                else
                {
                    Min.Width = Math.Max (Min.Width, child.Min.Width);
                    Min.Height += child.Min.Height;
                }
                Children.Add (children[index + Children.Count]);
            }
            UserMaxSize = Size.New (-1, -1, orientation);
            for (int i = 0; i < Children.Count; i++)
            {
                child = Children[i];

                if (child.UserMaxSize.GetMain (orientation) != Int32.MaxValue)
                    UserMaxSize.Main = Math.Max (child.UserMaxSize.GetMain (orientation), UserMaxSize.Main);
                if (child.UserMaxSize.GetCross (orientation) != Int32.MaxValue)
                    UserMaxSize.Cross = Math.Max (child.UserMaxSize.GetCross (orientation), UserMaxSize.Cross);
            }
            if (UserMaxSize.Main == -1)
                UserMaxSize.Main = Int32.MaxValue;
            if (UserMaxSize.Cross == -1)
                UserMaxSize.Cross = Int32.MaxValue;

            Expand = Expand.New (Orientation);
            Expand.Main = Children.Any (b => b.Expand.GetMain (Orientation));
            Expand.Cross = Children.Any (b => b.Expand.GetCross (Orientation));
        }
    }
}