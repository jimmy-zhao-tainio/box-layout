using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Boxing;

namespace Boxes
{
    public class AlignMainSpaceEvenly : Example, IExample
    {
        public Box GetTop ()
        {
            Box top = BoxCreate.FromXml (@"
                <hbox expand=""true"" align-main=""SpaceEvenly"">
                        <vbox min-size=""100, 100"" />
                        <vbox min-size=""100, 100"" />
                        <vbox min-size=""100, 100"" />
                        <vbox min-size=""100, 100"" />
                </hbox>
            ");
            brushes.Clear ();
            CreateBrushForBox (top);

            int r = random.Next (256 - 50) + 50;
            int g = random.Next (256 - 50) + 50;
            int b = random.Next (256 - 50) + 50;
            brushes.Add (top, new SolidBrush (Color.FromArgb (r, g, b)));
            return top;
        }
    }
}
