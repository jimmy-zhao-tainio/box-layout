﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Boxing;

namespace Boxes
{
    public class RandomBoxes : Example, IExample
    {
        int boxCount;

        public Box GetTop ()
        {
            Box top = new BoxHorizontal ();
            top.Fill = true;
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
                box.Fill = true;
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
