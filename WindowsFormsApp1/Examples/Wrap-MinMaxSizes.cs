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
                    <hbox wrap=""true"" fill=""true"">
                        <vbox minSize=""100, 200"" maxSize=""200, 200"" fill=""true"" />
                        <vbox minSize=""50, 50"" maxSize=""100, 100"" fill=""true"" />
                        <vbox minSize=""50, 50"" maxSize=""100, 100"" fill=""true"" />
                        <vbox minSize=""100, 100"" maxSize=""200, 200""  fill=""true"" />
                    </hbox>
                </hbox>
            ");
            brushes.Clear ();
            CreateBrushForBox (top);
            return top;
        }
    }
}
