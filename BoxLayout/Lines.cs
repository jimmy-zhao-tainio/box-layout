using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boxing
{
    public class Lines
    {
        public int EqualSizeMainMax = 0;
        public int SelfEqualSizeMainMax = 0;

        public List<Line> GetLines (Box parent, List<Box> children, int maxLength)
        {
            List<Line> lines = new List<Line> ();
            int index = 0;

            if (parent.EqualSizeMain)
                EqualSizeMainMax = children.Max (b => b.SelfEqualSizeMain != SelfEqualSize.False ? b.Min.GetMain (parent.Orientation) : 0);
            else
                SelfEqualSizeMainMax = children.Max (b => b.SelfEqualSizeMain == SelfEqualSize.True ? b.Min.GetMain (parent.Orientation) : 0);

            while (index < children.Count)
            {
                Line line = new Line (parent.Orientation, maxLength);
                lines.Add (line);
                while (true)
                {
                    if (parent.EqualSizeMain)
                    {
                        if (children[index].SelfEqualSizeMain != SelfEqualSize.False)
                            line.Add (children[index], EqualSizeMainMax);
                        else
                            line.Add (children[index]);
                    }
                    else
                    {
                        if (children[index].SelfEqualSizeMain == SelfEqualSize.True)
                            line.Add (children[index], SelfEqualSizeMainMax);
                        else
                            line.Add (children[index]);
                    }

                    index++;

                    if (index >= children.Count)
                        break;
                    if (parent.EqualSizeMain)
                    {
                        if (children[index].SelfEqualSizeMain != SelfEqualSize.False)
                        {
                            if (line.CanAdd (EqualSizeMainMax) == false)
                                break;
                        }
                        else
                        {
                            if (line.CanAdd (children[index]) == false)
                                break;
                        }
                    }
                    else
                    {
                        if (children[index].SelfEqualSizeMain == SelfEqualSize.True)
                        {
                            if (line.CanAdd (EqualSizeMainMax) == false)
                                break;
                        }
                        else
                        {
                            if (line.CanAdd (children[index]) == false)
                                break;
                        }
                    }
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