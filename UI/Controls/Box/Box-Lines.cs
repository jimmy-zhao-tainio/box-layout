using System;
using System.Collections.Generic;

namespace UI.Controls
{
    public partial class Box
    {
        public List<Line> GetLines(int maxLength)
        {
            List<Line> lines = new List<Line>();
            int index = 0;

            while (index < Children.Count)
            {
                Line line = new Line(Orientation, maxLength);
                lines.Add(line);
                while (true)
                {
                    line.Add(Children[index]);
                    index++;

                    if (index >= Children.Count)
                        break;
                    if (line.CanAdd(Children[index]) == false)
                        break;
                }
            }
            return lines;
        }

        public List<Line> GetLine()
        {
            List<Line> lines = new List<Line>();
            Line line = new Line(Orientation, Int32.MaxValue);
            Children.ForEach(b => line.Add(b));
            lines.Add(line);
            return lines;
        }
    }
}