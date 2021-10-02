using System.Drawing;
using UI.Controls;

namespace Boxes
{
    public class AlignCrossCenter : Example, IExample
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
            brushes.Clear ();
            CreateBrushForBox (top);

            int r = random.Next (256 - 50) + 50;
            int g = random.Next (256 - 50) + 50;
            int b = random.Next (256 - 50) + 50;
            brushes.Add (top, new SolidBrush (Color.FromArgb (r, g, b)));
            return top;
        }
    }
}
