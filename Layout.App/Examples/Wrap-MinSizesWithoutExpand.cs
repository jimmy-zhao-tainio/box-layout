using UI.Controls;

namespace Boxes
{
    public class WrapMinSizesWithoutExpand : Example, IExample
    {
        public Box GetTop ()
        {
            Box top = BoxCreate.FromXml (@"
                <hbox>
                    <hbox wrap=""true"" expand=""true"">
                        <vbox min-size=""300, 300"" expand=""false"" />
                        <vbox min-size=""150, 150"" expand=""false"" />
                        <vbox min-size=""150, 150"" expand=""false"" />
                        <vbox min-size=""300, 300"" expand=""false"" />
                    </hbox>
                </hbox>
            ");
            brushes.Clear ();
            CreateBrushForBox (top);
            return top;
        }
    }
}
