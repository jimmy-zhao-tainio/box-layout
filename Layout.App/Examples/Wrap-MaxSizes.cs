using UI.Controls;

namespace Boxes
{
    public class WrapOnlyMaxSizes : IExample
    {
        public Box GetTop ()
        {
            Box top = BoxCreate.FromXml (@"
                <hbox>
                    <hbox wrap=""true"" expand=""true"">
                        <vbox max-size=""300, 300"" expand=""true"" />
                        <vbox max-size=""150, 150"" expand=""true"" />
                        <vbox max-size=""150, 150"" expand=""true"" />
                        <vbox max-size=""300, 300"" expand=""true"" />
                    </hbox>
                </hbox>
            ");
            return top;
        }
    }
}
