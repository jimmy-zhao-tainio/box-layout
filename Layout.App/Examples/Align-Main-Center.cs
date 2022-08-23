using System.Drawing;
using UI.Controls;

namespace Boxes
{
    public class AlignMainCenter : IExample
    {
        public Box GetTop ()
        {
            Box top = BoxCreate.FromXml (@"
                <hbox expand=""true"" align-main=""center"">
                        <vbox min-size=""100, 100"" />
                        <vbox min-size=""100, 100"" />
                        <vbox min-size=""100, 100"" />
                        <vbox min-size=""100, 100"" />
                </hbox>
            ");
            return top;
        }
    }
}
