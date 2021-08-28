using UI.Controls;

namespace Boxes
{
    public class WrapNoWrap : Example, IExample
    {
        public Box GetTop ()
        {
            Box top = BoxCreate.FromXml (@"
                <vbox>
                    <hbox wrap=""false"">
                        <vbox min-size=""200, 200"" />
                        <vbox min-size=""200, 200"" />
                    </hbox>
                    <hbox wrap=""true"">
                        <vbox min-size=""200, 200"" />
                        <vbox min-size=""200, 200"" />
                    </hbox>
                    <hbox wrap=""false"">
                        <vbox min-size=""200, 200"" />
                        <vbox min-size=""200, 200"" />
                    </hbox>
                </vbox>
            ");
            brushes.Clear ();
            CreateBrushForBox (top);
            return top;
        }
    }
}
