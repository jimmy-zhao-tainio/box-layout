using UI.Structures;
using UI.Controls;

namespace Boxes
{
    public class RepeatedBoxes : Example, IExample
    {
        private void Repeat (Box parent, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Box box = new BoxHorizontal ();
                box.UserMinSize = UI.Structures.Size.New (6, 6, Orientation.Horizontal);
                box.Expand.Main = true;
                box.Expand.Cross = true;
                parent.Pack (box);
            }
        }
        public Box GetTop ()
        {
            Box top1 = new BoxHorizontal ();
            top1.Expand.Main = true;
            top1.Expand.Cross = true;
            Box vbox = new BoxVertical ();
            vbox.Wrap = true;
            vbox.Expand.Main = true;
            vbox.Expand.Cross = true;
            Box hbox = new BoxHorizontal ();
            hbox.Wrap = true;
            hbox.Expand.Main = true;
            hbox.Expand.Cross = true;
            top1.Pack (vbox);
            top1.Pack (hbox);
            Repeat(vbox, 2000);
            Repeat(hbox, 2000);

            Box top2 = new BoxHorizontal ();
            top2.Expand.Main = true;
            top2.Expand.Cross = true;

            vbox = new BoxVertical ();
            vbox.Wrap = true;
            vbox.Expand.Main = true;
            vbox.Expand.Cross = true;

            hbox = new BoxHorizontal ();
            hbox.Wrap = true;
            hbox.Expand.Main = true;
            hbox.Expand.Cross = true;

            top2.Pack (hbox);
            top2.Pack (vbox);
            Repeat(vbox, 2000);
            Repeat(hbox, 2000);

            Box top = new BoxVertical ();
            top.Expand.Main = true;
            top.Expand.Cross = true;
            top.Pack (top1);
            top.Pack (top2);

            brushes.Clear ();

            CreateBrushForBox (top);
            return top;
        }
    }
}
