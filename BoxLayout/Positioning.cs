using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boxing
{
    public class Position
    {
        static public void AlignMain (Box parent, List<Box> children, int lineSizeMain)
        {
            int position;

            // Reset main positions
            children.ForEach (c => c.LayoutPosition.SetMain (parent.Orientation, 0));

            // Get used main length.
            int usedMainLength = children.Sum (c => c.Computed.MainLength);

            // Available main length.
            int availableMainLength = lineSizeMain - usedMainLength;

            if (parent.AlignMain == Align.Start || parent.AlignMain == Align.Center || parent.AlignMain == Align.End)
            {
                if (parent.AlignMain == Align.Start)
                    position = 0;
                else if (parent.AlignMain == Align.Center)
                    position = Math.Max (0, lineSizeMain - usedMainLength) / 2;
                else
                    position = Math.Max (0, lineSizeMain - usedMainLength);
                for (int i = 0; i < children.Count; i++)
                {
                    children[i].LayoutPosition.SetMain (parent.Orientation, position);
                    position += children[i].Computed.MainLength;
                }
            }
            else if (parent.AlignMain == Align.SpaceEvenly)
            {
                Spacing spacing = Spacing.New (children.Count + 1, usedMainLength, lineSizeMain);
                position = spacing.Next ();
                for (int i = 0; i < children.Count; i++)
                {
                    children[i].LayoutPosition.SetMain (parent.Orientation, position);
                    position += children[i].Computed.MainLength + spacing.Next ();
                }
            }
            else if (parent.AlignMain == Align.SpaceBetween)
            {
                Spacing spacing = Spacing.New (children.Count - 1, usedMainLength, lineSizeMain);
                position = 0;
                for (int i = 0; i < children.Count; i++)
                {
                    children[i].LayoutPosition.SetMain (parent.Orientation, position);
                    position += children[i].Computed.MainLength + spacing.Next ();
                }
            }
            else if (parent.AlignMain == Align.SpaceAround)
            {
                Spacing spacing = Spacing.New (children.Count, usedMainLength, lineSizeMain);
                int space;

                position = 0;
                for (int i = 0; i < children.Count; i++)
                {
                    space = spacing.Next ();
                    children[i].LayoutPosition.SetMain (parent.Orientation, position + (space / 2));
                    position += children[i].Computed.MainLength + space;
                }
            }
        }

        static public void AlignCross (Box parent, Line line)
        {
            int crossLength;

            if (parent.AlignCross == Align.Start)
            {
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