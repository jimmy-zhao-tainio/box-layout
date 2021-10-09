using UI.Controls;

namespace Boxes
{
    public class Wrap : IExample
    {
        public Box GetTop ()
        {
            Box top = BoxCreate.FromXml (@"
                <hbox>
                    <vbox wrap=""true"" expand=""true"">
                        <vbox min-size=""50, 50"" expand=""true"" />
                        <vbox min-size=""50, 50"" expand=""true"" />
                        <vbox min-size=""50, 50"" expand=""true"" />
                        <vbox min-size=""50, 50"" expand=""true"" />
                        <vbox min-size=""50, 50"" expand=""true"" />
                        <vbox min-size=""50, 50"" expand=""true"" />
                        <vbox min-size=""50, 50"" expand=""true"" />
                        <vbox min-size=""50, 50"" expand=""true"" />
                        <vbox min-size=""50, 50"" expand=""true"" />
                    </vbox>
                    <hbox wrap=""true"" expand=""true"">
                        <vbox min-size=""50, 50"" expand=""true"" />
                        <vbox min-size=""50, 50"" expand=""true"" />
                        <vbox min-size=""50, 50"" expand=""true"" />
                        <vbox min-size=""50, 50"" expand=""true"" />
                        <vbox min-size=""50, 50"" expand=""true"" />
                        <vbox min-size=""50, 50"" expand=""true"" />
                        <vbox min-size=""50, 50"" expand=""true"" />
                        <vbox min-size=""50, 50"" expand=""true"" />
                        <vbox min-size=""50, 50"" expand=""true"" />
                    </hbox>
                </hbox>
            ");
            return top;
        }
    }
}
