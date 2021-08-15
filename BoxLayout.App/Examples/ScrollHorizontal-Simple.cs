using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Boxing;

namespace Boxes
{
    public class ScrollHorizontalSimple : Example, IExample
    {
        public Box GetTop ()
        {
            Box top = BoxCreate.FromXml (@"
                <hbox>
                    <hbox wrap=""false"" expand=""false"" min-size=""100, 100"" max-size=""100, 100"">
                        <vbox min-size=""50, 50"" max-size=""50, 50"" />
                        <vbox min-size=""50, 50"" max-size=""50, 50"" />
                        <vbox min-size=""50, 50"" max-size=""50, 50"" />
                        <vbox min-size=""50, 50"" max-size=""50, 50"" />
                    </hbox>
                    <hbox wrap=""false"" expand=""false"" min-size=""50, 20"" max-size=""50, 20"">
                        <vbox min-size=""20, 20"" max-size=""20, 20"" />
                        <vbox min-size=""20, 20"" max-size=""20, 20"" />
                        <vbox min-size=""20, 20"" max-size=""20, 20"" />
                        <vbox min-size=""20, 20"" max-size=""20, 20"" />
                    </hbox>
                </hbox>
            ");
            brushes.Clear ();
            CreateBrushForBox (top);
            return top;
        }
    }
}
