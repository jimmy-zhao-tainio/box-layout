using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using UI.Structures;

namespace UI.Controls
{
    [XmlType(TypeName = "root")]
    public class BoxRoot
    {
        // Attributres needs to be duplicated for Box.Children as well.
        [XmlElement(typeof (BoxHorizontal))]
        [XmlElement(typeof (BoxVertical))]
        [XmlElement(typeof (TextBox))]
        public Box Box { get; set; }
    }

    public class BoxCreate
    {
        public static Box FromXml(string xml)
        {
            XmlSerializer serializer = new XmlSerializer (typeof (BoxRoot));
            serializer.UnknownNode += new XmlNodeEventHandler(serializer_UnknownNode);
            serializer.UnknownAttribute += new XmlAttributeEventHandler(serializer_UnknownAttribute);

            StringReader reader = new StringReader($"<root>{xml}</root>");
            var root = (BoxRoot)serializer.Deserialize(reader);
            return root.Box;
        }

        private static void serializer_UnknownAttribute(object sender, XmlAttributeEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private static void serializer_UnknownNode(object sender, XmlNodeEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}