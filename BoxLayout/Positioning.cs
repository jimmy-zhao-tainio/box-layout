using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boxing
{
    public class Position
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

            if (parent.AlignMain == Align.Start || parent.AlignMain == Align.Center || parent.AlignMain == Align.End)
            {
                if (parent.AlignMain == Align.Start)
                    position = 0;
                else if (parent.AlignMain == Align.Center)
                    position = Math.Max (0, line.FinalSize.Main - usedMainLength) / 2;
                else
                    position = Math.Max (0, line.FinalSize.Main - usedMainLength);
                for (int i = 0; i < line.Children.Count; i++)
                {
                    line.Children[i].LayoutPosition.SetMain (parent.Orientation, position);
                    position += line.Children[i].Computed.MainLength;
                }
            }
            else if (parent.AlignMain == Align.SpaceEvenly)
            {
                Spacing spacing = Spacing.New (line.Children.Count + 1, usedMainLength, line.FinalSize.Main);
                position = spacing.Next ();
                for (int i = 0; i < line.Children.Count; i++)
                {
                    line.Children[i].LayoutPosition.SetMain (parent.Orientation, position);
                    position += line.Children[i].Computed.MainLength + spacing.Next ();
                }
            }
            else if (parent.AlignMain == Align.SpaceBetween)
            {
                Spacing spacing = Spacing.New (line.Children.Count - 1, usedMainLength, line.FinalSize.Main);
                position = 0;
                for (int i = 0; i < line.Children.Count; i++)
                {
                    line.Children[i].LayoutPosition.SetMain (parent.Orientation, position);
                    position += line.Children[i].Computed.MainLength + spacing.Next ();
                }
            }
            else if (parent.AlignMain == Align.SpaceAround)
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

            if (parent.AlignCross == Align.Start)
            {
                for (int i = 0; i < line.Children.Count; i++)
                {
                    Box child = line.Children[i];

                    child.LayoutPosition.SetCross (parent.Orientation, line.FinalPosition.Cross);
                }
            }
            else if (parent.AlignCross == Align.Center)
            {
                for (int i = 0; i < line.Children.Count; i++)
                {
                    Box child = line.Children[i];

                    crossLength = child.LayoutSize.GetCross (parent.Orientation);
                    child.LayoutPosition.SetCross (parent.Orientation, line.FinalPosition.Cross + ((line.FinalSize.Cross - crossLength) / 2));
                }
            }
            else if (parent.AlignCross == Align.End)
            {
                for (int i = 0; i < line.Children.Count; i++)
                {
                    Box child = line.Children[i];

                    crossLength = child.LayoutSize.GetCross (parent.Orientation);
                    child.LayoutPosition.SetCross (parent.Orientation, line.FinalPosition.Cross + (line.FinalSize.Cross - crossLength));
                }
            }
        }
    }
}