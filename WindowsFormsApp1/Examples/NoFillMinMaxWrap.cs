using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Boxing;

namespace Boxes
{
    public class NoFillMinMaxWrap : Example, IExample
    {
        public Box GetTop ()
        {
            Box top = BoxCreate.FromXml (@"
                    <hbox wrap=""true"" fill=""false"">
                        <vbox minSize=""300,300"" maxSize=""300, 300"" fill=""false"" />
                        <vbox minSize=""200,200"" maxSize=""200, 200"" fill=""false"" />
                        <vbox minSize=""100,100"" maxSize=""100, 100"" fill=""false"" />
                        <vbox minSize=""50,50"" maxSize=""50, 50"" fill=""false"" />
                        <vbox minSize=""25,25"" maxSize=""25, 25"" fill=""false"" />
                        <vbox minSize=""12,12"" maxSize=""12, 12"" fill=""false"" />
                        <vbox minSize=""6,6"" maxSize=""6, 6"" fill=""false"" />
                        <vbox minSize=""3,3"" maxSize=""3, 3"" fill=""false"" />
                    </hbox>
            ");
            brushes.Clear ();
            CreateBrushForBox (top);
            return top;
        }
    }
}
