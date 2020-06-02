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
    }
}
