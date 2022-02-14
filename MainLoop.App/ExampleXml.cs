using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp
{
    class ExampleXml
    {
        public static string Buffer;

        static ExampleXml()
        {
            Random random = new Random();
            Buffer = @"
                    <hbox>
                        <vbox expand=""true"">
                            <hbox expand-main=""true"">
                                <hbox min-size=""80, 80"" expand-main=""true"" />
                            </hbox>
                            <hbox expand=""true"">
                                <vbox expand-main=""true"">
                                    <hbox min-size=""80, 80"" expand-cross=""true"" />
                                </vbox>
                                <hbox expand=""true"" min-size=""320, 240"" equal-size=""FirstMinimal"" wrap=""true"">";
            for (int i = 0; i < 500; i++)
            {
                Buffer += String.Format(@"<hbox min-size=""{0}, {1}"" horizontal-scrollbar=""Hidden"" vertical-scrollbar=""Hidden"" />", random.Next(20, 100), random.Next(20, 100));
            }
            Buffer += @"</hbox>
                                <vbox expand-main=""true"">
                                    <hbox min-size=""80, 80"" expand-cross=""true"" />
                                </vbox>
                            </hbox>
                            <hbox expand-main=""true"">
                                <hbox min-size=""80, 80"" expand-main=""true"" />
                            </hbox>
                        </vbox>
                    </hbox>";
        }
    }
}
