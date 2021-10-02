using UI.Controls;

namespace Boxes
{
    public class ExpandCenterHorizontal : Example, IExample
    {
        public Box GetTop ()
        {
            Box top = BoxCreate.FromXml (@"
                    <hbox>
                        <vbox expand=""true"" />
                        <vbox expand-main=""true"">
                            <hbox expand-cross=""true"" />
                            <hbox min-size=""100, 100"" />
                            <hbox expand-cross=""true"" />
                        </vbox>
                        <vbox expand=""true"" />
                    </hbox>
            ");
            brushes.Clear ();
            CreateBrushForBox (top);
            return top;
        }
    }
}
