using UI.Controls;

namespace Boxes
{
    public class WrapMinMaxSizes : IExample
    {
        public Box GetTop ()
        {
            Box top = BoxCreate.FromXml (@"
                <hbox>
                    <hbox wrap=""true"" expand=""true"">
                        <vbox min-size=""100, 200"" max-size=""200, 200"" expand=""true"" />
                        <vbox min-size=""50, 50"" max-size=""100, 100"" expand=""true"" />
                        <vbox min-size=""50, 50"" max-size=""100, 100"" expand=""true"" />
                        <vbox min-size=""100, 100"" max-size=""200, 200""  expand=""true"" />
                    </hbox>
                </hbox>
            ");
            return top;
        }
    }
}
