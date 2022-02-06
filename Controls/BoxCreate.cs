using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using UI.Structures;

namespace UI.Controls
{
    public class BoxCreate
    {
        [XmlRoot ("BoxGlue")]
        public class BoxGlue
        {
            [XmlAttribute ("orientation")]
            public string Orientation = "horizontal";

            [XmlAttribute ("min-size")]
            public string MinSize;
            [XmlAttribute ("max-size")]
            public string MaxSize;

            [XmlAttribute ("wrap")]
            public string Wrap = "false";

            [XmlAttribute ("expand")]
            public string Expand = "false";
            [XmlAttribute ("expand-main")]
            public string ExpandMain = null;
            [XmlAttribute ("expand-cross")]
            public string ExpandCross = null;

            [XmlAttribute ("align-main")]
            public string AlignMain = "start";
            [XmlAttribute ("align-cross")]
            public string AlignCross = "start";
            [XmlAttribute ("self-align-cross")]
            public string SelfAlignCross = "inherit";
            [XmlAttribute ("line-align-cross")]
            public string LineAlignCross = "start";
            [XmlAttribute ("scroll-offset-y")]
            public string ScrollOffsetY = "0";
            [XmlAttribute ("scroll-offset-x")]
            public string ScrollOffsetX = "0";

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
                    box.UserMinSize.Width = Convert.ToInt32 (MinSize.Split (",".ToCharArray ())[0]);
                    box.UserMinSize.Height = Convert.ToInt32 (MinSize.Split (",".ToCharArray ())[1]);
                }
                if (MaxSize != null)
                {
                    box.UserMaxSize.Width = Convert.ToInt32 (MaxSize.Split (",".ToCharArray ())[0]);
                    box.UserMaxSize.Height = Convert.ToInt32 (MaxSize.Split (",".ToCharArray ())[1]);
                }
                box.Wrap = Wrap.ToLower() == "true" ? true : false;

                if (Expand.ToLower () == "true")
                {
                    box.Expand.Main = true;
                    box.Expand.Cross = true;
                }
                if (ExpandMain != null)
                    box.Expand.Main = ExpandMain.ToLower () == "true" ? true : false;
                if (ExpandCross != null)
                    box.Expand.Cross = ExpandCross.ToLower() == "true" ? true : false;

                box.AlignMain = (AlignMain)Enum.Parse (typeof (AlignMain), AlignMain, true);
                box.AlignCross = (AlignCross)Enum.Parse (typeof (AlignCross), AlignCross, true);
                box.LineAlignCross = (LineAlignCross)Enum.Parse (typeof (LineAlignCross), LineAlignCross, true);
                box.SelfAlignCross = (SelfAlignCross)Enum.Parse (typeof (SelfAlignCross), SelfAlignCross, true);
                box.VerticalScrollbar.ContentOffset = Int32.Parse(ScrollOffsetY);
                box.HorizontalScrollbar.ContentOffset = Int32.Parse(ScrollOffsetX);

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