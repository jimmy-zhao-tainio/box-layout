using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boxing 
{
    public class Compute
    {
        static public void SetMainLengths (Orientation orientation, List<Box> items, int availableLength)
        {
            // Put boxes that expand in the expanding list and subtract min length from availableLength for those that don't.
            int expandCount = 0;
            int expandingMinLength = 0;

            foreach (var box in items)
            {
                if (box.Expand.GetMain (orientation) == false)
                {
                    box.Computed.MainLength = box.Min.GetMain (orientation);
                    availableLength -= box.Computed.MainLength;
                    box.Computed.CanExpand = false;
                }
                else
                {
                    box.Computed.MainLength = 0;
                    box.Computed.CanExpand = true;
                    expandCount++;
                    expandingMinLength += box.Min.GetMain (orientation);
                }
            }

            // Boxes may expand until they reach UserMaxSize and no further.
            // When UserMaxSize is reached, Spacing.New needs to run again since more space will be available for other items.
            // If no box reaches UserMaxSize, a second run will not be necessary.
            //
            // For lengths of N, worst case is O(N), for N-1 boxes.
            //
            // Examples of iterations, where X denotes overflowing usermax
            //
            // Even length = 6
            //
            //     Max  i1  i2  i3  i4  i5  i6
            //     1    .X  .   .   .   .   .
            //     1    .   .X  .   .   .   .
            //     1    .   .   .X  .   .   .
            //     1    .   .   .   .X  .   .
            //     1    .   .   .   .   .X  .
            //
            // Odd length = 5
            //
            //     Max  i1  i2  i3  i4  i5
            //     1    .X  .   .   .   .
            //     1    .   .X  .   .   .
            //     1    .   .   .X  .   .
            //     1    .   .   .   .X  .

            bool reachedUserMax = true;
            while (expandCount > 0 && reachedUserMax == true)
            {
                reachedUserMax = false;

                Spacing spacing = Spacing.New (expandCount, expandingMinLength, availableLength);

                foreach (var box in items)
                {
                    if (box.Computed.CanExpand == false)
                        continue;

                    int minMain = box.Min.GetMain (orientation);
                    int maxMain = box.UserMaxSize.GetMain (orientation);
                    int spacingLength = spacing.Next ();

                    if (minMain + spacingLength >= maxMain)
                    {
                        box.Computed.MainLength = maxMain;
                        availableLength -= maxMain;
                        box.Computed.CanExpand = false;
                        expandCount--;
                        expandingMinLength -= box.Min.GetMain (orientation);
                        reachedUserMax = true;
                    }
                    else
                    {
                        box.Computed.MainLength = minMain + spacingLength;
                    }
                }
            }
        }

        static public void SetLinesSize (List<Line> lines, Size layoutSize)
        {
            lines.ForEach (line => {
                // For lines that cannot expand cross wise, use the probed cross size.
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
}
