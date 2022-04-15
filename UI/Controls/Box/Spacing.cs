namespace UI.Controls
{
    public struct Spacing
    {
        public int Length;
        public int Remainder;
        public int Index;

        static public Spacing New(int itemCount, int itemLength, int availableLength)
        {
            Spacing spacing;
            int difference = availableLength - itemLength;

            if (difference <= 0 || itemCount == 0)
            {
                spacing.Length = 0;
                spacing.Remainder = 0;
                spacing.Index = 0;
                return spacing;
            }
            spacing.Length = difference / itemCount;
            spacing.Remainder = difference % itemCount;
            spacing.Index = 0;
            return spacing;
        }

        public int Next()
        {
            if (Index++ < Remainder)
                return Length + 1;
            return Length;
        }
    }
}
