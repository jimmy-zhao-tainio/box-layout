using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Boxing;

namespace Boxes
{
    public class ExpandMinMaxWrapCrossAlignEnd : Example, IExample
    {
        public Box GetTop ()
        {
            Box top = BoxCreate.FromXml (@"
                    <hbox wrap=""true"" expand=""true"" align-cross=""end"">
                        <vbox min-size=""300,300"" max-size=""600, 600"" expand=""true"" />
                        <vbox min-size=""200,200"" max-size=""400, 400"" expand=""true"" />
                        <vbox min-size=""100,100"" max-size=""200, 200"" expand=""true"" />
                        <vbox min-size=""50,50"" max-size=""100, 100"" expand=""true"" />
                        <vbox min-size=""25,25"" max-size=""50, 50"" expand=""true"" />
                        <vbox min-size=""12,12"" max-size=""24, 24"" expand=""true"" />
                        <vbox min-size=""6,6"" max-size=""12, 12"" expand=""true"" />
                        <vbox min-size=""3,3"" max-size=""6, 6"" expand=""true"" />
                    </hbox>
            ");
            brushes.Clear ();
            int r = 50;
            int g = 50;
            int b = 50;
            brushes.Add (top, new SolidBrush (Color.FromArgb (r, g, b)));
            CreateBrushForBox (top);
            return top;
        }
    }
}
