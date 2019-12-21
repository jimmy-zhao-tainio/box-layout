using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Boxing;

namespace Boxes
{
    public class ExpandCenterVertical: Example, IExample
    {
        public Box GetTop ()
        {
            Box top = BoxCreate.FromXml (@"
                    <vbox>
                        <hbox expand=""true"" />
                        <hbox expand-main=""true"">
                            <vbox expand-cross=""true"" />
                            <vbox min-size=""100, 100"" />
                            <vbox expand-cross=""true"" />
                        </hbox>
                        <hbox expand=""true"" />
                    </vbox>
            ");
            brushes.Clear ();
            CreateBrushForBox (top);
            return top;
        }
    }
}
