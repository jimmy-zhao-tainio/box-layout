using System;
using System.Collections.Generic;
using UI.Structures;
using UI.Controls;

namespace UI.Layout
{
    public class Lines
    {
        public List<Line> GetLines (Box parent, List<Box> children, int maxLength)
        {
            List<Line> lines = new List<Line> ();
            int index = 0;

            while (index < children.Count)
            {
                Line line = new Line (parent.Orientation, maxLength);
                lines.Add (line);
                while (true)
                {
                    line.Add (children[index]);
                    index++;

                    if (index >= children.Count)
                        break;
                    if (line.CanAdd (children[index]) == false)
                        break;
                }
            }
            return lines;
        }

        public List<Line> GetLine (Orientation orientation, List<Box> children)
        {
            List<Line> lines = new List<Line> ();
            Line line = new Line (orientation, Int32.MaxValue);
            children.ForEach (b => line.Add (b));
            lines.Add (line);
            return lines;
        }
    }
}