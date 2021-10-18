using UI.Controls;

namespace Boxes
{
    public class ScrollHorizontalSimple : IExample
    {
        public Box GetTop ()
        {
            Box top = BoxCreate.FromXml (@"
                    <hbox wrap=""false"" expand=""false"" min-size=""500, 200"" max-size=""500, 200"">
                        <vbox min-size=""100, 100"" max-size=""100, 100"" />
                        <vbox min-size=""100, 100"" max-size=""100, 100"" />
                        <vbox min-size=""100, 100"" max-size=""100, 100"" />
                        <vbox min-size=""100, 100"" max-size=""100, 100"" />
                        <vbox min-size=""100, 100"" max-size=""100, 100"" />
                        <vbox min-size=""100, 100"" max-size=""100, 100"" />
                        <vbox min-size=""100, 100"" max-size=""100, 100"" />
                        <vbox min-size=""100, 100"" max-size=""100, 100"" />
                        <vbox min-size=""100, 100"" max-size=""100, 100"" />
                    </hbox>
            ");
            return top;
        }
    }
}
