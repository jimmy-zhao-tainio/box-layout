using UI.Controls;

namespace Boxes
{
    public class ScrollHorizontalOffset: IExample
    {
        public Box GetTop ()
        {
            Box top = BoxCreate.FromXml (@"
                <hbox wrap=""false"" expand=""false"" max-size=""350, 70"" scroll-offset-x=""50"">
                    <hbox min-size=""100, 50"" max-size=""100, 50"" />
                    <hbox min-size=""100, 50"" max-size=""100, 50"" />
                    <hbox min-size=""100, 50"" max-size=""100, 50"" />
                    <hbox min-size=""100, 50"" max-size=""100, 50"" />
                </hbox>
            ");
            return top;
        }
    }
}
