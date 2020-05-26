using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boxing
{
    public class WrapAlgorithm
    {
        public static List<Line> GetLines (Orientation orientation, List<Box> children, int maxLength)
        {
            List<Line> lines = new List<Line> ();
            int index = 0;

            while (index < children.Count)
            {
                Line line = new Line (orientation, maxLength);
                lines.Add (line);
                do
                {
                    line.Add (children[index]);
                    index++;
                } while (index < children.Count && line.CanAdd (children[index]));
            }
            return lines;
        }

        public static List<Line> GetLine (Orientation orientation, List<Box> children)
        {
            List<Line> lines = new List<Line> ();
            Line line = new Line (orientation, Int32.MaxValue);
            children.ForEach (b => line.Add (b));
            lines.Add (line);
            return lines;
        }

        public static void SetLinesFinalSize (List<Line> lines, Size layoutSize)
        {
            lines.ForEach (line => {
                // For lines that cannot expand cross wise, the probed used cross size is the final size.
                if (line.Expand.Cross == false)
                {
                    line.FinalSize = Size.New (line.Orientation);
                    line.FinalSize.Main = layoutSize.Main;
                    line.FinalSize.Cross = line.ProbedUsedSize.Cross;
                }
            });

            // Find lines that can expand cross wise.
            List<Line> expandingLines = lines.Where (line => line.Expand.Cross).ToList ();

            while (expandingLines.Count != 0)
            {
                // Find minimum cross lengths for expanding lines.
                int expandingMinCross = expandingLines.Sum (e => e.ProbedUsedSize.Cross);
                // Calculate available cross length.
                int availableCross = layoutSize.Cross - lines.Sum (e => e.FinalSize == null ? 0 : e.FinalSize.Cross);

                Spacing spacing = Spacing.New (expandingLines.Count, expandingMinCross, availableCross);

                // Set spacing
                expandingLines.ForEach (line => {
                    line.FinalSize = Size.New (line.Orientation);
                    line.FinalSize.Main = layoutSize.Main;
                    line.FinalSize.Cross = line.ProbedUsedSize.Cross + spacing.Next ();
                });

                // Find lines that hits UserMaxSize and expanded too much.
                List<Line> hitUserMax = expandingLines.Where (line => line.FinalSize.Cross >= line.UserMaxSize.Cross).ToList ();

                if (hitUserMax.Count > 0)
                {
                    // Correct the finalCross length for lines.
                    hitUserMax.ForEach (line => line.FinalSize.Cross = line.UserMaxSize.Cross);
                    // Remove them from the expandingLines list and re-run the loop if necessary.
                    expandingLines = expandingLines.Except (hitUserMax).ToList ();
                    // Reset expandingLines final cross length.
                    expandingLines.ForEach (line => line.FinalSize = null);
                }
                else
                {
                    // If no line hit UserMax, then they've expanded as much as possible and we're done.
                    expandingLines.Clear ();
                }
            }
        }
    }

    public class Line
    {
        public Orientation Orientation;
        // Largest main length given this line.
        public int MaxLength;

        public List<Box> Children = new List<Box> ();

        // Combined minimum size for children.
        public Size MinSize;

        // Largest maximum user size (main and cross), if any.
        public Size UserMaxSize;

        // If any child expands (main and cross)
        public Expand Expand;

        // Used size when given minimum cross length.
        public Size ProbedUsedSize;

        // Final size
        public Size FinalSize;

        // Final position
        public Point FinalPosition;

        public Line (Orientation orientation, int maxLength)
        {
            Orientation = orientation;
            MaxLength = maxLength;
            Children = new List<Box> ();
            MinSize = Size.New (orientation);
            UserMaxSize = Size.New (Int32.MaxValue, Int32.MaxValue, orientation);
            Expand = Expand.New (orientation);
        }

        public bool CanAdd (Box box)
        {
            if (MinSize.Main + box.Min.GetMain (Orientation) > MaxLength)
                return false;
            return true;
        }

        public void Add (Box box)
        {
            Children.Add (box);

            MinSize.Main += box.Min.GetMain (Orientation);
            MinSize.Cross = Math.Max (MinSize.Cross, box.Min.GetCross (Orientation));

            Expand.Main = Expand.Main || box.Expand.GetMain (Orientation);
            Expand.Cross = Expand.Cross || box.Expand.GetCross (Orientation);

            if (box.UserMaxSize.GetMain (Orientation) != Int32.MaxValue)
            {
                if (UserMaxSize.Main == Int32.MaxValue)
                    UserMaxSize.Main = box.UserMaxSize.GetMain (Orientation);
                else
                    UserMaxSize.Main = Math.Max (UserMaxSize.Main, box.UserMaxSize.GetMain (Orientation));
            }

            if (box.UserMaxSize.GetCross (Orientation) != Int32.MaxValue)
            {
                if (UserMaxSize.Cross == Int32.MaxValue)
                    UserMaxSize.Cross = box.UserMaxSize.GetCross (Orientation);
                else
                    UserMaxSize.Cross = Math.Max (UserMaxSize.Cross, box.UserMaxSize.GetCross (Orientation));
            }
        }
    }
}