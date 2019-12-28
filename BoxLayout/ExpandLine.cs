using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boxing 
{
    public class ExpandLine
    {
        static public Dictionary<Box, int> GetMainLengths (Orientation orientation, List<Box> items, int availableLength)
        {
            Dictionary<Box, int> boxLengths = new Dictionary<Box, int> ();
            List<Box> expanding = new List<Box> ();

            // Put boxes that expand in the expanding list and subtract min length from availableLength for those that don't.
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Expand.GetMain (orientation) == false)
                {
                    int length = items[i].Min.GetMain (orientation);
                    boxLengths[items[i]] = length;
                    availableLength -= length;
                }
                else
                {
                    expanding.Add (items[i]);
                }
            }

            // Boxes may expand until they reach UserMaxSize and no further.
            // When UserMaxSize is reached, Spacing.New needs to run again since more space will be available for other items.
            while (true)
            {
                List<Box> reachedUserMaxSize = SetMainLength (orientation, expanding, availableLength, boxLengths);

                if (reachedUserMaxSize.Count == 0)
                    return boxLengths;

                for (int i = 0; i < reachedUserMaxSize.Count; i++)
                {
                    Box box = reachedUserMaxSize[i];
                    
                    // This box expanded to UserMaxSize.
                    // Remove it from the expanding list and adjust availableLength.
                    int length = box.UserMaxSize.GetMain (orientation);
                    availableLength -= length;
                    expanding.Remove (box);
                }
            }
        }

        static private List<Box> SetMainLength (Orientation orientation, List<Box> expanding, int availableLength, 
                                                Dictionary<Box, int> boxLengths)
        {
            List<Box> reachedUserMaxSize = new List<Box> ();

            if (expanding.Count == 0)
                return reachedUserMaxSize;

            int expandingMinLength = 0;

            for (int i = 0; i < expanding.Count; i++)
                expandingMinLength += expanding[i].Min.GetMain (orientation);

            Spacing spacing = Spacing.New (expanding.Count, expandingMinLength, availableLength);

            for (int i = 0; i < expanding.Count; i++)
            {
                int minMain = expanding[i].Min.GetMain (orientation);
                int maxMain = expanding[i].UserMaxSize.GetMain (orientation);
                int spacingLength = spacing.Next ();

                if (minMain + spacingLength >= maxMain)
                {
                    boxLengths[expanding[i]] = maxMain;
                    reachedUserMaxSize.Add (expanding[i]);
                }
                else
                {
                    boxLengths[expanding[i]] = minMain + spacingLength;
                }
            }
            return reachedUserMaxSize;
        }
    }
}
