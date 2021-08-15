using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Boxing;

namespace Boxes
{
    public class WrapMinSizesWithExpand : Example, IExample
    {
        public Box GetTop ()
        {
            Box top = BoxCreate.FromXml (@"
                    <hbox wrap=""true"" expand=""true"">
                        <vbox min-size=""300, 300"" expand=""true"" />
                        <vbox min-size=""150, 150"" expand=""true"" />
                        <vbox min-size=""150, 150"" expand=""true"" />
                        <vbox min-size=""300, 300"" expand=""true"" />
                    </hbox>
            ");
            brushes.Clear ();
            CreateBrushForBox (top);
            return top;
        }
    }
}
