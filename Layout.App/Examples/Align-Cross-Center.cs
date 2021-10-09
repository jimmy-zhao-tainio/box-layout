using System.Drawing;
using UI.Controls;

namespace Boxes
{
    public class AlignCrossCenter : IExample
    {
        public Box GetTop ()
        {
            Box top = BoxCreate.FromXml (@"
                <hbox align-cross=""center"">
                        <vbox min-size=""400, 400"" />
                        <vbox min-size=""300, 300"" />
                        <vbox min-size=""200, 200"" />
                        <vbox min-size=""100, 100"" />
                </hbox>
            ");
            return top;
        }
    }
}
