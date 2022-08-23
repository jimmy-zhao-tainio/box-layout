using System;
using UI.Structures;
using System.Linq;

namespace UI.Controls
{
    public partial class Box
    {
        public void AlignLines()
        {
            if (Lines == null)
                return;
            AlignLinesCross();
            foreach (Box child in Children)
            {
                if (child.Lines == null)
                    continue;
                child.AlignLines();
            }
        }

        public void AlignLinesCross ()
        {
            if (LineAlignCross == UI.Structures.LineAlignCross.Start ||
                LineAlignCross == UI.Structures.LineAlignCross.Center ||
                LineAlignCross == UI.Structures.LineAlignCross.End)
            {
                int crossPosition;
                int usedCrossLength = Lines.Sum (i => i.FinalSize.Cross);

                if (LineAlignCross == UI.Structures.LineAlignCross.Start)
                    crossPosition = 0;
                else if (LineAlignCross == UI.Structures.LineAlignCross.Center)
                    crossPosition = Math.Max (0, ScrollAreaSize.Cross - usedCrossLength) / 2;
                else
                    crossPosition = Math.Max (0, ScrollAreaSize.Cross - usedCrossLength);

                foreach (Line line in Lines)
                {
                    line.FinalPosition = Point.New (Orientation);
                    line.FinalPosition.Main = 0;
                    line.FinalPosition.Cross = crossPosition;
                    crossPosition += line.FinalSize.Cross;
                }
            }
            else if (LineAlignCross == UI.Structures.LineAlignCross.SpaceEvenly)
            {
                int usedCrossLength = Lines.Sum (i => i.FinalSize.Cross);
                Spacing spacing = Spacing.New (Lines.Count + 1, usedCrossLength, ScrollAreaSize.Cross);

                int crossPosition = spacing.Next ();
                foreach (Line line in Lines)
                {
                    line.FinalPosition = Point.New (Orientation);
                    line.FinalPosition.Main = 0;
                    line.FinalPosition.Cross = crossPosition;

                    crossPosition += line.FinalSize.Cross + spacing.Next ();
                }
            }
            else if (LineAlignCross == UI.Structures.LineAlignCross.SpaceBetween)
            {
                int usedCrossLength = Lines.Sum (i => i.FinalSize.Cross);
                Spacing spacing = Spacing.New (Lines.Count - 1, usedCrossLength, ScrollAreaSize.Cross);

                int crossPosition = 0;
                foreach (Line line in Lines)
                {
                    line.FinalPosition = Point.New (Orientation);
                    line.FinalPosition.Main = 0;
                    line.FinalPosition.Cross = crossPosition;

                    crossPosition += line.FinalSize.Cross + spacing.Next ();
                }
            }
            else if (LineAlignCross == UI.Structures.LineAlignCross.SpaceAround)
            {
                int usedCrossLength = Lines.Sum (i => i.FinalSize.Cross);
                Spacing spacing = Spacing.New (Lines.Count, usedCrossLength, ScrollAreaSize.Cross);
                int space;

                int crossPosition = 0;
                foreach (Line line in Lines)
                {
                    space = spacing.Next ();
                    line.FinalPosition = Point.New (Orientation);
                    line.FinalPosition.Main = 0;
                    line.FinalPosition.Cross = crossPosition + (space / 2);

                    crossPosition += line.FinalSize.Cross + space;
                }
            }
        }
    }
}