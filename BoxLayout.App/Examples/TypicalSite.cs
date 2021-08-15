using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Boxing;

namespace Boxes
{
    public class TypicalSite : Example, IExample
    {
        public Box GetTop ()
        {
            Box top = BoxCreate.FromXml (@"
                    <hbox>
                        <vbox expand=""true"">
                            <hbox expand-main=""true"">
                                <hbox min-size=""80, 80"" expand-main=""true"" />
                            </hbox>
                            <hbox expand=""true"">
                                <vbox expand-main=""true"">
                                    <hbox min-size=""80, 80"" expand-cross=""true"" />
                                </vbox>
                                <hbox expand=""true"" min-size=""320, 240"" wrap=""true"">
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                    <hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" /><hbox min-size=""40, 40"" />
                                </hbox>
                                <vbox expand-main=""true"">
                                    <hbox min-size=""80, 80"" expand-cross=""true"" />
                                </vbox>
                            </hbox>
                            <hbox expand-main=""true"">
                                <hbox min-size=""80, 80"" expand-main=""true"" />
                            </hbox>
                        </vbox>
                    </hbox>
            ");
            brushes.Clear ();
            CreateBrushForBox (top);

            return top;
        }
    }
}
