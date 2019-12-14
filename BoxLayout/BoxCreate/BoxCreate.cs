using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace Boxing
{
    public class BoxCreate
    {
        [XmlRoot ("BoxGlue")]
        public class BoxGlue
        {
            [XmlAttribute ("orientation")]
            public string Orientation = "horizontal";
            [XmlAttribute ("minSize")]
            public string MinSize;
            [XmlAttribute ("maxSize")]
            public string MaxSize;
            [XmlAttribute ("wrap")]
            public string Wrap = "false";
            [XmlAttribute ("fill")]
            public string Fill = "false";
            [XmlAttribute ("fillMain")]
            public string FillMain = null;
            [XmlAttribute ("fillCross")]
            public string FillCross = null;

            [XmlElement ("BoxGlue")]
            public List<BoxGlue> Children = new List<BoxGlue> ();

            public Box ToBox ()
            {
                Box box;

                if (Orientation == "horizontal")
                    box = new BoxHorizontal ();
                else
                    box = new BoxVertical ();
                if (MinSize != null)
                {
                    box.UserMinSize = new Size (0, 0);
                    box.UserMinSize.Width = Convert.ToInt32 (MinSize.Split (",".ToCharArray ())[0]);
                    box.UserMinSize.Height = Convert.ToInt32 (MinSize.Split (",".ToCharArray ())[1]);
                }
                if (MaxSize != null)
                {
                    box.UserMaxSize = new Size (0, 0);
                    box.UserMaxSize.Width = Convert.ToInt32 (MaxSize.Split (",".ToCharArray ())[0]);
                    box.UserMaxSize.Height = Convert.ToInt32 (MaxSize.Split (",".ToCharArray ())[1]);
                }
                box.Wrap = Wrap.ToLower() == "true" ? true : false;
                box.Fill = Fill.ToLower() == "true" ? true : false;
                if (FillMain != null)
                    box.FillMain = FillMain.ToLower () == "true" ? true : false;
                if (FillCross != null)
                    box.FillCross = FillCross.ToLower() == "true" ? true : false;
                for (int i = 0; i < Children.Count; i++)
                    box.Pack (Children[i].ToBox ());
                return box;
            }
        }
        
        public static Box FromXml(string xml)
        {
            xml = xml.Replace ("<hbox", "<BoxGlue orientation=\"horizontal\"");
            xml = xml.Replace ("/hbox", "/BoxGlue");
            xml = xml.Replace ("<vbox", "<BoxGlue orientation=\"vertical\"");
            xml = xml.Replace ("/vbox", "/BoxGlue");

            XmlSerializer serializer = new XmlSerializer (typeof (BoxGlue));
            StringReader reader = new StringReader (xml);
            BoxGlue boxGlue = (BoxGlue)serializer.Deserialize (reader);
            return boxGlue.ToBox ();
        }
    }
}