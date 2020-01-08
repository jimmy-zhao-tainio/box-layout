using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Boxing;

namespace Boxes
{
    public class ExpandCrossWrap : Example, IExample
    {
        public Box GetTop ()
        {
            Box top = BoxCreate.FromXml (@"
                <hbox>
                    <hbox wrap=""true"" expand=""true"">
                        <vbox expand=""true"" min-size=""800, 0""   max-size=""800, 200"" />
                        <vbox                 min-size=""200, 200"" max-size=""200, 200"" />
                        <vbox expand=""true"" min-size=""800, 0""   max-size=""800, 200"" />
                    </hbox>
                </hbox>
            ");
            brushes.Clear ();
            CreateBrushForBox (top);
            return top;
        }
    }
}
