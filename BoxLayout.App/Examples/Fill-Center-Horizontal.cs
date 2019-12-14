using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Boxing;

namespace Boxes
{
    public class FillCenterHorizontal : Example, IExample
    {
        public Box GetTop ()
        {
            Box top = BoxCreate.FromXml (@"
                    <hbox wrap=""false"" fill=""true"">
                        <vbox fill=""true"" />
                        <vbox fillCross=""true"">
                            <hbox fillMain=""true"" />
                            <hbox minSize=""100, 100"" />
                            <hbox fillMain=""true"" />
                        </vbox>
                        <vbox fill=""true"" />
                    </hbox>
            ");
            brushes.Clear ();
            CreateBrushForBox (top);
            return top;
        }
    }
}
