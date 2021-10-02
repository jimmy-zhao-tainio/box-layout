using System.Drawing;
using UI.Controls;

namespace Boxes
{
    public class ExpandMainMinMax: Example, IExample
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
            brushes.Clear ();
            CreateBrushForBox (top);
            int r = random.Next (256 - 50) + 50;
            int g = random.Next (256 - 50) + 50;
            int b = random.Next (256 - 50) + 50;
            brushes.Add (top.Children[0], new SolidBrush (Color.FromArgb (r, g, b)));

            return top;
        }
    }
}
