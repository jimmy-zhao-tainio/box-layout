using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boxing
{
    public class Lines
    {
        private Lines () { }

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
        }
    }
}