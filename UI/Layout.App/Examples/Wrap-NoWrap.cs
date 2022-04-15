using UI.Controls;

namespace Boxes
{
    public class WrapNoWrap : IExample
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
            return top;
        }
    }
}
