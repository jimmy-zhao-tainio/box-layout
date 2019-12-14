using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Boxing;

namespace Boxes
{
    public class Wrap : Example, IExample
    {
        public Box GetTop ()
        {
            Box top = BoxCreate.FromXml (@"
                <hbox>
                    <vbox wrap=""true"" fill=""true"">
                        <vbox minSize=""50, 50"" fill=""true"" />
                        <vbox minSize=""50, 50"" fill=""true"" />
                        <vbox minSize=""50, 50"" fill=""true"" />
                        <vbox minSize=""50, 50"" fill=""true"" />
                        <vbox minSize=""50, 50"" fill=""true"" />
                        <vbox minSize=""50, 50"" fill=""true"" />
                        <vbox minSize=""50, 50"" fill=""true"" />
                        <vbox minSize=""50, 50"" fill=""true"" />
                        <vbox minSize=""50, 50"" fill=""true"" />
                    </vbox>
                    <hbox wrap=""true"" fill=""true"">
                        <vbox minSize=""50, 50"" fill=""true"" />
                        <vbox minSize=""50, 50"" fill=""true"" />
                        <vbox minSize=""50, 50"" fill=""true"" />
                        <vbox minSize=""50, 50"" fill=""true"" />
                        <vbox minSize=""50, 50"" fill=""true"" />
                        <vbox minSize=""50, 50"" fill=""true"" />
                        <vbox minSize=""50, 50"" fill=""true"" />
                        <vbox minSize=""50, 50"" fill=""true"" />
                        <vbox minSize=""50, 50"" fill=""true"" />
                    </hbox>
                </hbox>
            ");
            brushes.Clear ();
            CreateBrushForBox (top);
            return top;
        }
    }
}
