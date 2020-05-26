using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Boxing;

namespace Boxes
{
    public class WrapMinMaxSizes : Example, IExample
    {
        public Box GetTop ()
        {
            Box top = BoxCreate.FromXml (@"
                <hbox>
                    <hbox wrap=""true"" expand=""true"">
                        <vbox min-size=""100, 200"" max-size=""200, 200"" expand=""true"" />
                        <vbox min-size=""50, 50"" max-size=""100, 100"" expand=""true"" />
                        <vbox min-size=""50, 50"" max-size=""100, 100"" expand=""true"" />
                        <vbox min-size=""100, 100"" max-size=""200, 200""  expand=""true"" />
                    </hbox>
                </hbox>
            ");
            brushes.Clear ();
            CreateBrushForBox (top);
            return top;
        }
    }
}
