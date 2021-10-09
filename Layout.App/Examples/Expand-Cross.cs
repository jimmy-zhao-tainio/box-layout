using System.Drawing;
using UI.Controls;

namespace Boxes
{
    public class ExpandCross : IExample
    {
        public Box GetTop ()
        {
            Box top = BoxCreate.FromXml (@"
                    <vbox>
                        <hbox expand-cross=""true"">
                            <vbox min-size=""100, 100"" />
                        </hbox>
                    </vbox>
            ");
            return top;
        }
    }
}
