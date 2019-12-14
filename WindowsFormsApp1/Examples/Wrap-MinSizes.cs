using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Boxing;

namespace Boxes
{
    public class WrapMinSizes : Example, IExample
    {
        public Box GetTop ()
        {
            Box top = BoxCreate.FromXml (@"
                <hbox>
                    <hbox wrap=""true"" fill=""true"">
                        <vbox minSize=""300, 300"" fill=""true"" />
                        <vbox minSize=""150, 150"" fill=""true"" />
                        <vbox minSize=""150, 150"" fill=""true"" />
                        <vbox minSize=""300, 300"" fill=""true"" />
                    </hbox>
                </hbox>
            ");
            brushes.Clear ();
            CreateBrushForBox (top);
            return top;
        }
    }
}
