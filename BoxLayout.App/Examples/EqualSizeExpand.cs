using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Boxing;

namespace Boxes
{
    public class EqualSizeExpand : Example, IExample
    {
        public Box GetTop ()
        {
            Box top = BoxCreate.FromXml (@"
                <hbox equal-size=""true"">
                    <hbox min-size=""200, 400"" />
                    <hbox min-size=""300, 300"" />
                    <hbox min-size=""400, 200"" />
                </hbox>
            ");
            brushes.Clear ();
            CreateBrushForBox (top);

            return top;
        }
    }
}
