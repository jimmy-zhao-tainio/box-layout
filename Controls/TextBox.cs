using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UI.Controls
{
    [XmlType(TypeName = "text")]
    public class TextBox : BoxHorizontal
    {
        [XmlText]
        public string Text { get; set; }
        public override void Pack(Box child)
        { }

        public override void Unpack(Box child)
        { }
    }
}
