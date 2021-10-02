using UI.Controls;

namespace Boxes
{
    public class ExpandMinMaxWrapScroll : Example, IExample
    {
        public Box GetTop ()
        {
            Box top = BoxCreate.FromXml (@"
                    <hbox wrap=""true"" expand=""true"">
                        <vbox min-size=""400,400"" max-size=""400, 400"" expand=""true"" />
                        <vbox min-size=""300,300"" max-size=""300, 300"" expand=""true"" />
                        <vbox min-size=""50,50"" max-size=""50, 50"" expand=""true"" />
                    </hbox>
            ");
            brushes.Clear ();
            CreateBrushForBox (top);
            return top;
        }
    }
}
