using System.Collections.Generic;

namespace Boxing 
{
    public class ExpandLinesCross
    {
        static public Dictionary<Line, int> GetCrossLengths (Orientation orientation, List<Line> lines, int availableLength)
        {
            Dictionary<Line, int> linesCrossLength = new Dictionary<Line, int> ();
            List<Line> expanding = new List<Line> ();

            // Put boxes that expand in the expanding list and subtract min length from availableLength for those that don't.
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].Expand.GetCross (orientation) == false)
                {
                    int length = lines[i].Min.GetCross (orientation);
                    linesCrossLength[lines[i]] = length;
                    availableLength -= length;
                }
                else
                {
                    expanding.Add (lines[i]);
                }
            }

            // Lines may expand cross-wise until the largest UserMaxSize is reached by one of its boxes, and no further.
            // When UserMaxSize is reached, Spacing.New needs to run again since more space will be available for other items.
            while (true)
            {
                List<Line> reachedUserMaxSize = SetCrossLength (orientation, expanding, availableLength, linesCrossLength);

                if (reachedUserMaxSize.Count == 0)
                    return linesCrossLength;

                for (int i = 0; i < reachedUserMaxSize.Count; i++)
                {
                    Line line = reachedUserMaxSize[i];
                    
                    // This line expanded to UserMaxSize.
                    // Remove it from the expanding list and adjust availableLength.
                    int length = line.UserMaxSize.GetCross (orientation);
                    availableLength -= length;
                    expanding.Remove (line);
                }
            }
        }

        static private List<Line> SetCrossLength (Orientation orientation, List<Line> expanding, int availableLength, 
                                                  Dictionary<Line, int> linesCrossLength)
        {
            List<Line> reachedUserMaxSize = new List<Line> ();

            if (expanding.Count == 0)
                return reachedUserMaxSize;

            int expandingMinLength = 0;

            for (int i = 0; i < expanding.Count; i++)
                expandingMinLength += expanding[i].Min.GetCross (orientation);

            Spacing spacing = Spacing.New (expanding.Count, expandingMinLength, availableLength);

            for (int i = 0; i < expanding.Count; i++)
            {
                int minMain = expanding[i].Min.GetCross (orientation);
                int maxMain = expanding[i].UserMaxSize.GetCross (orientation);
                int spacingLength = spacing.Next ();

                if (minMain + spacingLength >= maxMain)
                {
                    linesCrossLength[expanding[i]] = maxMain;
                    reachedUserMaxSize.Add (expanding[i]);
                }
                else
                {
                    linesCrossLength[expanding[i]] = minMain + spacingLength;
                }
            }
            return reachedUserMaxSize;
        }
    }
}
