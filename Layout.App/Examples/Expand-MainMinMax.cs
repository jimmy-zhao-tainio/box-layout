using System.Drawing;
using UI.Controls;

namespace Boxes
{
    public class ExpandMainMinMax: IExample
    {
        public Box GetTop ()
        {
            Box top = BoxCreate.FromXml (@"
                    <vbox>
                        <hbox expand-main=""true"">
                            <hbox min-size=""100,100"" max-size=""200,200"" expand-main=""true"" />
                            <hbox min-size=""100,100"" expand-main=""true"" />
                        </hbox>
                    </vbox>
            ");
            return top;
        }
    }
}
