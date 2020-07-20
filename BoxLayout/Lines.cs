using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boxing
{
    public class Lines
    {
        public int EqualSizeMain = 0;
        public int SelfEqualSizeMain = 0;

        public List<Line> GetLines (Orientation orientation, bool equalSizeMain, List<Box> children, int maxLength)
        {
            List<Line> lines = new List<Line> ();
            int index = 0;

            if (equalSizeMain)
            {
                EqualSizeMain = children.Where (b => b.SelfEqualSizeMain == SelfEqualSize.Inherit ||
                                                     b.SelfEqualSizeMain == SelfEqualSize.True)
                                        .Max (b => b.Min.GetMain (orientation));
            }
            else
            {
                SelfEqualSizeMain = children.Where (b => b.SelfEqualSizeMain == SelfEqualSize.True)
                                            .Max (b => b.Min.GetMain (orientation));
            }

            while (index < children.Count)
            {
                Line line = new Line (orientation, maxLength);
                lines.Add (line);
                while (true)
                {
                    line.Add (children[index]);
                    index++;

                    if (index >= children.Count)
                        break;
                    if (equalSizeMain)
                    {
                        if (children[index].SelfEqualSizeMain == SelfEqualSize.Inherit ||
                            children[index].SelfEqualSizeMain == SelfEqualSize.True)
                        {
                            if (line.CanAdd (EqualSizeMain) == false)
                                continue;
                        }
                    }
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