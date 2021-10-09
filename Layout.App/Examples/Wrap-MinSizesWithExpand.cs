using UI.Controls;

namespace Boxes
{
    public class WrapMinSizesWithExpand : IExample
    {
        public Box GetTop ()
        {
            Box top = BoxCreate.FromXml (@"
                    <hbox wrap=""true"" expand=""true"">
                        <vbox min-size=""300, 300"" expand=""true"" />
                        <vbox min-size=""150, 150"" expand=""true"" />
                        <vbox min-size=""150, 150"" expand=""true"" />
                        <vbox min-size=""300, 300"" expand=""true"" />
                    </hbox>
            ");
            return top;
        }
    }
}
