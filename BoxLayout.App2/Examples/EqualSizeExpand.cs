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
                <vbox>
                    <hbox expand=""true"">
                        <vbox expand=""true"">
                            <hbox min-size=""400, 400"" />
                        </vbox>
                        <vbox expand=""true"">
                            <hbox min-size=""200, 400"" />
                        </vbox>
                    </hbox>
                    <hbox expand=""true"" equal-size=""true"">
                        <vbox expand=""true"">
                            <hbox min-size=""400, 400"" />
                        </vbox>
                        <vbox expand=""true"">
                            <hbox min-size=""200, 400"" />
                        </vbox>
                    </hbox>
                </vbox>
            ");
            brushes.Clear ();
            CreateBrushForBox (top);

            return top;
        }
    }
}
