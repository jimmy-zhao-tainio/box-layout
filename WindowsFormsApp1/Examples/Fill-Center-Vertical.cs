using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Boxing;

namespace Boxes
{
    public class FillCenterVertical: Example, IExample
    {
        public Box GetTop ()
        {
            Box top = BoxCreate.FromXml (@"
                    <vbox wrap=""false"" fill=""true"">
                        <hbox fill=""true"" />
                        <hbox fillCross=""true"">
                            <vbox fillMain=""true"" />
                            <vbox minSize=""100, 100"" />
                            <vbox fillMain=""true"" />
                        </hbox>
                        <hbox fill=""true"" />
                    </vbox>
            ");
            brushes.Clear ();
            CreateBrushForBox (top);
            return top;
        }
    }
}
