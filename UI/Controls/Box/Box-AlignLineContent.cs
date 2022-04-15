using System;
using UI.Structures;
using System.Linq;

namespace UI.Controls
{
    public partial class Box
    {
        public void AlignLinesContent()
        {
            AlignLinesContentMain();
            AlignLinesContentCross();
        }

        public void AlignLinesContentMain()
        {
            if (Lines == null)
                return;
            foreach (Line line in Lines)
            {
                AlignLineContentMain(line);
                foreach (Box child in line.Children)
                {
                    if (child.Lines == null)
                        continue;
                    child.AlignLinesContentMain();
                }
            }
        }

        public void AlignLinesContentCross()
        {
            if (Lines == null)
                return;
            foreach (Line line in Lines)
            {
                AlignLineContentCross(line);
                foreach (Box child in line.Children)
                {
                    if (child.Lines == null)
                        continue;
                    child.AlignLinesContentCross();
                }
            }
        }

        public void AlignLineContentMain (Line line)
        {
            int position;

            // Reset main positions
            line.Children.ForEach (c => c.LayoutPosition.SetMain (Orientation, 0));

            // Get used main length.
            int usedMainLength = line.Children.Sum (c => c.Computed.MainLength);

            // Available main length.
            int availableMainLength = line.FinalSize.Main - usedMainLength;

            if (AlignMain == AlignMain.Start || AlignMain == AlignMain.Center || AlignMain == AlignMain.End)
            {
                if (AlignMain == AlignMain.Start)
                    position = 0;
                else if (AlignMain == AlignMain.Center)
                    position = Math.Max (0, line.FinalSize.Main - usedMainLength) / 2;
                else
                    position = Math.Max (0, line.FinalSize.Main - usedMainLength);
                for (int i = 0; i < line.Children.Count; i++)
                {
                    line.Children[i].LayoutPosition.SetMain (Orientation, position);
                    position += line.Children[i].Computed.MainLength;
                }
            }
            else if (AlignMain == AlignMain.SpaceEvenly)
            {
                Spacing spacing = Spacing.New (line.Children.Count + 1, usedMainLength, line.FinalSize.Main);
                position = spacing.Next ();
                for (int i = 0; i < line.Children.Count; i++)
                {
                    line.Children[i].LayoutPosition.SetMain (Orientation, position);
                    position += line.Children[i].Computed.MainLength + spacing.Next ();
                }
            }
            else if (AlignMain == AlignMain.SpaceBetween)
            {
                Spacing spacing = Spacing.New (line.Children.Count - 1, usedMainLength, line.FinalSize.Main);
                position = 0;
                for (int i = 0; i < line.Children.Count; i++)
                {
                    line.Children[i].LayoutPosition.SetMain (Orientation, position);
                    position += line.Children[i].Computed.MainLength + spacing.Next ();
                }
            }
            else if (AlignMain == AlignMain.SpaceAround)
            {
                Spacing spacing = Spacing.New (line.Children.Count, usedMainLength, line.FinalSize.Main);
                int space;

                position = 0;
                for (int i = 0; i < line.Children.Count; i++)
                {
                    space = spacing.Next ();
                    line.Children[i].LayoutPosition.SetMain (Orientation, position + (space / 2));
                    position += line.Children[i].Computed.MainLength + space;
                }
            }
        }

        public void AlignLineContentCross (Line line)
        {
            int crossLength;

            for (int i = 0; i < line.Children.Count; i++)
            {
                Box child = line.Children[i];

                if ((child.SelfAlignCross == SelfAlignCross.Inherit && AlignCross == UI.Structures.AlignCross.Start) ||
                     child.SelfAlignCross == SelfAlignCross.Start)
                {
                    child.LayoutPosition.SetCross (Orientation, line.FinalPosition.Cross);
                }
                else if ((child.SelfAlignCross == SelfAlignCross.Inherit && AlignCross == UI.Structures.AlignCross.Center) ||
                         child.SelfAlignCross == SelfAlignCross.Center)
                {
                    crossLength = child.LayoutSize.GetCross (Orientation);
                    child.LayoutPosition.SetCross (Orientation, line.FinalPosition.Cross + ((line.FinalSize.Cross - crossLength) / 2));
                }
                else if ((child.SelfAlignCross == SelfAlignCross.Inherit && AlignCross == UI.Structures.AlignCross.End) ||
                         child.SelfAlignCross == SelfAlignCross.End)
                {
                    crossLength = child.LayoutSize.GetCross (Orientation);
                    child.LayoutPosition.SetCross (Orientation, line.FinalPosition.Cross + (line.FinalSize.Cross - crossLength));
                }
            }
        }
    }
}