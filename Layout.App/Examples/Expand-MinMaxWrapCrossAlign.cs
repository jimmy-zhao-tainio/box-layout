﻿using System.Drawing;
using UI.Controls;

namespace Boxes
{
    public class ExpandMinMaxWrapCrossAlign : IExample
    {
        public Box GetTop ()
        {
            Box top = BoxCreate.FromXml (@"
                    <hbox wrap=""true"" expand=""true"" align-cross=""center"">
                        <vbox min-size=""300,300"" max-size=""600, 600"" expand=""true"" />
                        <vbox min-size=""200,200"" max-size=""400, 400"" expand=""true"" />
                        <vbox min-size=""100,100"" max-size=""200, 200"" expand=""true"" />
                        <vbox min-size=""50,50"" max-size=""100, 100"" expand=""true"" />
                        <vbox min-size=""25,25"" max-size=""50, 50"" expand=""true"" />
                        <vbox min-size=""12,12"" max-size=""24, 24"" expand=""true"" />
                        <vbox min-size=""6,6"" max-size=""12, 12"" expand=""true"" />
                        <vbox min-size=""3,3"" max-size=""6, 6"" expand=""true"" />
                    </hbox>
            ");
            return top;
        }
    }
}
