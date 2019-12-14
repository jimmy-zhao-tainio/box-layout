using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Boxing;

namespace Boxes
{
    public class RepeatedBoxes : Example, IExample
    {
        private void Repeat (Box parent, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Box box = new BoxHorizontal (new Boxing.Size (6, 6));
                box.Fill = true;
                parent.Pack (box);
            }
        }
        public Box GetTop ()
        {
            Box top1 = new BoxHorizontal ();
            top1.Fill = true;
            Box vbox = new BoxVertical ();
            vbox.Wrap = true;
            vbox.Fill = true;
            Box hbox = new BoxHorizontal ();
            hbox.Wrap = true;
            hbox.Fill = true;
            top1.Pack (vbox);
            top1.Pack (hbox);
            Repeat(vbox, 2000);
            Repeat(hbox, 2000);

            Box top2 = new BoxHorizontal ();
            top2.Fill = true;
            vbox = new BoxVertical ();
            vbox.Wrap = true;
            vbox.Fill = true;
            hbox = new BoxHorizontal ();
            hbox.Wrap = true;
            hbox.Fill = true;
            top2.Pack (hbox);
            top2.Pack (vbox);
            Repeat(vbox, 2000);
            Repeat(hbox, 2000);

            Box top = new BoxVertical ();
            top.Fill = true;
            top.Pack (top1);
            top.Pack (top2);

            brushes.Clear ();

            CreateBrushForBox (top);
            return top;
        }
    }
}
