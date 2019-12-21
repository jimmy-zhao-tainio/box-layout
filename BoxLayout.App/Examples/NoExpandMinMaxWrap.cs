using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Boxing;

namespace Boxes
{
    public class NoExoandMinMaxWrap : Example, IExample
    {
        public Box GetTop ()
        {
            Box top = BoxCreate.FromXml (@"
                    <hbox wrap=""true"" expand=""false"">
                        <vbox min-size=""300,300"" max-size=""300, 300"" expand=""false"" />
                        <vbox min-size=""200,200"" max-size=""200, 200"" expand=""false"" />
                        <vbox min-size=""100,100"" max-size=""100, 100"" expand=""false"" />
                        <vbox min-size=""50,50"" max-size=""50, 50"" expand=""false"" />
                        <vbox min-size=""25,25"" max-size=""25, 25"" expand=""false"" />
                        <vbox min-size=""12,12"" max-size=""12, 12"" expand=""false"" />
                        <vbox min-size=""6,6"" max-size=""6, 6"" expand=""false"" />
                        <vbox min-size=""3,3"" max-size=""3, 3"" expand=""false"" />
                    </hbox>
            ");
            brushes.Clear ();
            CreateBrushForBox (top);
            return top;
        }
    }
}
