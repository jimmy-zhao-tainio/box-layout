using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Boxing;

namespace Boxes
{
    public class EqualSizeWrap : Example, IExample
    {
        public Box GetTop ()
        {
            Box top = BoxCreate.FromXml (@"
                <hbox wrap=""true"" equal-size-main=""true"">
                    <hbox min-size=""200, 50"" />
                    <hbox min-size=""10, 50"" />
                    <hbox min-size=""10, 50"" />
                    <hbox min-size=""10, 50"" />
                    <hbox min-size=""10, 50"" />
                </hbox>
            ");
            brushes.Clear ();
            CreateBrushForBox (top);

            return top;
        }
    }
}
