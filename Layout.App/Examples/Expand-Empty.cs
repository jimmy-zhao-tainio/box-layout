using UI.Controls;

namespace Boxes
{
    public class ExpandEmpty : IExample
    {
        public Box GetTop ()
        {
            Box top = BoxCreate.FromXml (@"
                    <vbox>
                        <hbox expand=""true"" />
                    </vbox>
            ");
            return top;
        }
    }
}
