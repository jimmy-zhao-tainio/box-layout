using UI.Controls;

namespace Boxes
{
    public class RandomBoxes : Example, IExample
    {
        int boxCount;

        public Box GetTop ()
        {
            Box top = new BoxHorizontal ();
            top.Expand.Main = true;
            top.Expand.Cross = true;
            boxCount = 1;
            brushes.Clear ();
            Randomize (top, 12, 20);
            CreateBrushForBox (top);
            return top;
        }

        private void Randomize (Box parent, int level, int maxLevel)
        {
            if (level > maxLevel)
                return;
            for (int i = 0; i < level; i++)
            {
                Box box = random.Next (0, 2) == 0 ? (Box)new BoxHorizontal() : (Box)new BoxVertical();
                box.Expand.Main = true;
                box.Expand.Cross = true;
                parent.Pack (box);
                boxCount++;
            }
            for (int i = 0; i < level; i++)
            {
                Randomize (parent.Children[i], level + 4, maxLevel);
            }
        }
    }
}
