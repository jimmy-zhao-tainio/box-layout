﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Boxing;

namespace Boxes
{
    public class ExpandCross : Example, IExample
    {
        public Box GetTop ()
        {
            Box top = BoxCreate.FromXml (@"
                    <vbox>
                        <hbox expand-cross=""true"">
                            <vbox min-size=""100, 100"" />
                        </hbox>
                    </vbox>
            ");
            brushes.Clear ();
            CreateBrushForBox (top);
            int r = random.Next (256 - 50) + 50;
            int g = random.Next (256 - 50) + 50;
            int b = random.Next (256 - 50) + 50;
            brushes.Add (top.Children[0], new SolidBrush (Color.FromArgb (r, g, b)));

            return top;
        }
    }
}
