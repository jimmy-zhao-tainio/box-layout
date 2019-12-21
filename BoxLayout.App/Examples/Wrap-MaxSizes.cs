using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Boxing;

namespace Boxes
{
    public class WrapMaxSizes : Example, IExample
    {
        public Box GetTop ()
        {
            Box top = BoxCreate.FromXml (@"
                <hbox>
                    <hbox wrap=""true"" expand=""true"">
                        <vbox max-size=""300, 300"" expand=""true"" />
                        <vbox max-size=""150, 150"" expand=""true"" />
                        <vbox max-size=""150, 150"" expand=""true"" />
                        <vbox max-size=""300, 300"" expand=""true"" />
                    </hbox>
                </hbox>
            ");
            brushes.Clear ();
            CreateBrushForBox (top);
            return top;
        }
    }
}
