using System.Drawing;
using UI.Controls;

namespace Boxes
{
    public class ExpandMain: IExample
    {
        public Box GetTop ()
        {
            Box top = BoxCreate.FromXml (@"
                    <vbox>
                        <hbox expand-main=""true"">
                            <vbox min-size=""100, 100"" />
                        </hbox>
                    </vbox>
            ");
            return top;
        }
    }
}
