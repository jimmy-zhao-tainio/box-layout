using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boxing
{
    public class Positioning
    {
        static public void AlignMain (Box parent, Line line)
        {
            int position;

            // Reset main positions
            line.Children.ForEach (c => c.LayoutPosition.SetMain (parent.Orientation, 0));

            // Get used main length.
            int usedMainLength = line.Children.Sum (c => c.Computed.MainLength);

            // Available main length.
            int availableMainLength = line.FinalSize.Main - usedMainLength;

            if (parent.AlignMain == Boxing.AlignMain.Start || parent.AlignMain == Boxing.AlignMain.Center || parent.AlignMain == Boxing.AlignMain.End)
            {
                if (parent.AlignMain == Boxing.AlignMain.Start)
                    position = 0;
                else if (parent.AlignMain == Boxing.AlignMain.Center)
                    position = Math.Max (0, line.FinalSize.Main - usedMainLength) / 2;
                else
                    position = Math.Max (0, line.FinalSize.Main - usedMainLength);
                for (int i = 0; i < line.Children.Count; i++)
                {
                    line.Children[i].LayoutPosition.SetMain (parent.Orientation, position);
                    position += line.Children[i].Computed.MainLength;
                }
            }
            else if (parent.AlignMain == Boxing.AlignMain.SpaceEvenly)
            {
                Spacing spacing = Spacing.New (line.Children.Count + 1, usedMainLength, line.FinalSize.Main);
                position = spacing.Next ();
                for (int i = 0; i < line.Children.Count; i++)
                {
                    line.Children[i].LayoutPosition.SetMain (parent.Orientation, position);
                    position += line.Children[i].Computed.MainLength + spacing.Next ();
                }
            }
            else if (parent.AlignMain == Boxing.AlignMain.SpaceBetween)
            {
                Spacing spacing = Spacing.New (line.Children.Count - 1, usedMainLength, line.FinalSize.Main);
                position = 0;
                for (int i = 0; i < line.Children.Count; i++)
                {
                    line.Children[i].LayoutPosition.SetMain (parent.Orientation, position);
                    position += line.Children[i].Computed.MainLength + spacing.Next ();
                }
            }
            else if (parent.AlignMain == Boxing.AlignMain.SpaceAround)
            {
                Spacing spacing = Spacing.New (line.Children.Count, usedMainLength, line.FinalSize.Main);
                int space;

                position = 0;
                for (int i = 0; i < line.Children.Count; i++)
                {
                    space = spacing.Next ();
                    line.Children[i].LayoutPosition.SetMain (parent.Orientation, position + (space / 2));
                    position += line.Children[i].Computed.MainLength + space;
                }
            }
        }

        static public void AlignCross (Box parent, Line line)
        {
            int crossLength;

            for (int i = 0; i < line.Children.Count; i++)
            {
                Box child = line.Children[i];

                if ((child.SelfAlignCross == SelfAlignCross.Inherit && parent.AlignCross == Boxing.AlignCross.Start) ||
                     child.SelfAlignCross == SelfAlignCross.Start)
                {
                    child.LayoutPosition.SetCross (parent.Orientation, line.FinalPosition.Cross);
                }
                else if ((child.SelfAlignCross == SelfAlignCross.Inherit && parent.AlignCross == Boxing.AlignCross.Center) ||
                         child.SelfAlignCross == SelfAlignCross.Center)
                {
                    crossLength = child.LayoutSize.GetCross (parent.Orientation);
                    child.LayoutPosition.SetCross (parent.Orientation, line.FinalPosition.Cross + ((line.FinalSize.Cross - crossLength) / 2));
                }
                else if ((child.SelfAlignCross == SelfAlignCross.Inherit && parent.AlignCross == Boxing.AlignCross.End) ||
                         child.SelfAlignCross == SelfAlignCross.End)
                {
                    crossLength = child.LayoutSize.GetCross (parent.Orientation);
                    child.LayoutPosition.SetCross (parent.Orientation, line.FinalPosition.Cross + (line.FinalSize.Cross - crossLength));
                }
            }
        }

        static public void LineAlignCross (Box box)
        {
            if (box.LineAlignCross == Boxing.LineAlignCross.Start ||
                box.LineAlignCross == Boxing.LineAlignCross.Center ||
                box.LineAlignCross == Boxing.LineAlignCross.End)
            {
                int crossPosition;
                int usedCrossLength = box.Lines.Sum (i => i.FinalSize.Cross);

                if (box.LineAlignCross == Boxing.LineAlignCross.Start)
                    crossPosition = 0;
                else if (box.LineAlignCross == Boxing.LineAlignCross.Center)
                    crossPosition = Math.Max (0, box.LayoutSize.Cross - usedCrossLength) / 2;
                else
                    crossPosition = Math.Max (0, box.LayoutSize.Cross - usedCrossLength);

                foreach (Line line in box.Lines)
                {
                    line.FinalPosition = Point.New (box.Orientation);
                    line.FinalPosition.Main = 0;
                    line.FinalPosition.Cross = crossPosition;
                    crossPosition += line.FinalSize.Cross;
                }
            }
            else if (box.LineAlignCross == Boxing.LineAlignCross.SpaceEvenly)
            {
                int usedCrossLength = box.Lines.Sum (i => i.FinalSize.Cross);
                Spacing spacing = Spacing.New (box.Lines.Count + 1, usedCrossLength, box.LayoutSize.Cross);

                int crossPosition = spacing.Next ();
                foreach (Line line in box.Lines)
                {
                    line.FinalPosition = Point.New (box.Orientation);
                    line.FinalPosition.Main = 0;
                    line.FinalPosition.Cross = crossPosition;

                    crossPosition += line.FinalSize.Cross + spacing.Next ();
                }
            }
            else if (box.LineAlignCross == Boxing.LineAlignCross.SpaceBetween)
            {
                int usedCrossLength = box.Lines.Sum (i => i.FinalSize.Cross);
                Spacing spacing = Spacing.New (box.Lines.Count - 1, usedCrossLength, box.LayoutSize.Cross);

                int crossPosition = 0;
                foreach (Line line in box.Lines)
                {
                    line.FinalPosition = Point.New (box.Orientation);
                    line.FinalPosition.Main = 0;
                    line.FinalPosition.Cross = crossPosition;

                    crossPosition += line.FinalSize.Cross + spacing.Next ();
                }
            }
            else if (box.LineAlignCross == Boxing.LineAlignCross.SpaceAround)
            {
                int usedCrossLength = box.Lines.Sum (i => i.FinalSize.Cross);
                Spacing spacing = Spacing.New (box.Lines.Count, usedCrossLength, box.LayoutSize.Cross);
                int space;

                int crossPosition = 0;
                foreach (Line line in box.Lines)
                {
                    space = spacing.Next ();
                    line.FinalPosition = Point.New (box.Orientation);
                    line.FinalPosition.Main = 0;
                    line.FinalPosition.Cross = crossPosition + (space / 2);

                    crossPosition += line.FinalSize.Cross + space;
                }
            }
        }
    }
}