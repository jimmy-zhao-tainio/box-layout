using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UI.Structures;

namespace UI.Controls
{
    [XmlType(TypeName = "hbox")]
    public class BoxHorizontal : Box
    {
        public BoxHorizontal() : base(Orientation.Horizontal)
        {
        }
    }

    [XmlType(TypeName = "vbox")]
    public class BoxVertical : Box
    {
        public BoxVertical() : base(Orientation.Vertical)
        {
        }
    }

    public partial class Box
    {
        public Orientation Orientation;

        [XmlIgnore] public Size Min;
        [XmlIgnore] public Size ChildrenMin;

        [XmlElement("hbox", typeof(BoxHorizontal))]
        [XmlElement("vbox", typeof(BoxVertical))]
        [XmlElement("text", typeof(TextBox))]
        public List<Box> Children = new List<Box> ();

        [XmlAttribute ("wrap")]
        public bool Wrap = false;

        [XmlAttribute("expand")]
        public bool ExpandBoth
        {
            get { return false; } // Serializer
            set
            {
                Expand.Main = value;
                Expand.Cross = value;
            }
        }

        [XmlAttribute("expand-main")]
        public bool ExpandMain
        {
            get { return false; }
            set { Expand.Main = value; }
        }
    
        [XmlAttribute("expand-cross")]
        public bool ExpandCross
        {
            get { return false; }
            set { Expand.Cross = value; }
        }
        [XmlIgnore] public Expand Expand;

        [XmlAttribute("align-main")]
        public string AlignMainString
        {
            get { return string.Empty; }
            set { AlignMain = (AlignMain)Enum.Parse(typeof(AlignMain), value, true); }
        }
        [XmlIgnore] public AlignMain AlignMain = AlignMain.Start;
        
        [XmlAttribute ("align-cross")]
        public string AlignCrossString
        {
            get { return string.Empty; }
            set { AlignCross = (AlignCross)Enum.Parse(typeof(AlignCross), value, true); }
        }
        [XmlIgnore] public AlignCross AlignCross = AlignCross.Start;
        
        [XmlAttribute ("line-align-cross")]
        public string LineAlignCrossString
        {
            get { return string.Empty; }
            set { LineAlignCross = (LineAlignCross)Enum.Parse(typeof(LineAlignCross), value, true); }
        }
        [XmlIgnore] public LineAlignCross LineAlignCross = LineAlignCross.Start;

        [XmlAttribute ("self-align-cross")]
        public string SelfAlignCrossString
        {
            get { return string.Empty; }
            set { SelfAlignCross = (SelfAlignCross)Enum.Parse(typeof(SelfAlignCross), value, true); }
        }
        [XmlIgnore] public SelfAlignCross SelfAlignCross = SelfAlignCross.Inherit;

        [XmlAttribute ("equal-size")]
        public string EqualSizeString
        {
            get { return string.Empty; }
            set { EqualSize = (EqualSize)Enum.Parse(typeof(EqualSize), value, true); }
        }
        [XmlIgnore] public EqualSize EqualSize = EqualSize.False;

        [XmlAttribute("min-size")]
        public string UserMinSizeString
        {
            get => "";// For the serializer
            set => UserMinSize = Size.New(value, Orientation);
        }
        [XmlIgnore] public Size UserMinSize;
        
        [XmlAttribute("max-size")]
        public string UserMaxSizeString
        {
            get => ""; // For the serializer
            set => UserMaxSize = Size.New(value, Orientation);
        }
        [XmlIgnore] public Size UserMaxSize;

        [XmlIgnore] public Point LayoutPosition;
        [XmlIgnore] public Size LayoutSize;
        [XmlIgnore] public Size ContentSize;
        [XmlIgnore] public Size ScrollAreaSize;
        [XmlIgnore] public BoxComputed Computed;

        [XmlAttribute("horizontal-scrollbar")]
        public string HorizontalScrollbarString
        {
            get => "";
            set => HorizontalScrollbar.Mode = (ScrollbarMode)Enum.Parse (typeof (ScrollbarMode), value, true);
        }
        
        [XmlAttribute("vertical-scrollbar")]
        public string VerticalScrollbarString
        { 
            get => "";
            set => VerticalScrollbar.Mode = (ScrollbarMode)Enum.Parse (typeof (ScrollbarMode), value, true);
        }

        [XmlAttribute("scroll-offset-x")]
        public string ScrollOffsetX
        { 
            get => "";
            set => HorizontalScrollbar.ContentOffset = Int32.Parse(value);
        }

        [XmlAttribute("scroll-offset-y")]
        public string ScrollOffsetY
        {
            get => "";
            set => VerticalScrollbar.ContentOffset = Int32.Parse(value);
        }
        
        [XmlIgnore] public HScrollbar HorizontalScrollbar;
        [XmlIgnore] public VScrollbar VerticalScrollbar;

        [XmlIgnore] public List<Line> Lines;

        public Box() { }

        public Box (Orientation orientation)
        {
            Orientation = orientation;
            LayoutPosition = Point.New (Orientation);
            LayoutSize = Size.New (Orientation);
            ContentSize = Size.New (Orientation);
            ScrollAreaSize = Size.New (Orientation);
            Computed = new BoxComputed ();
            UserMinSize = Size.New (0, 0, Orientation);
            UserMaxSize = Size.New (int.MaxValue, int.MaxValue, Orientation);
            Expand = Expand.New (Orientation);
            HorizontalScrollbar = new HScrollbar(ScrollbarMode.Auto);
            VerticalScrollbar = new VScrollbar(ScrollbarMode.Auto);
        }

        public virtual void Pack (Box child)
        {
            Children.Add(child);
        }

        public virtual void Unpack (Box child)
        {
            for (int i = 0; i < Children.Count; i++)
            {
                if (Children[i] != child)
                    continue;
                Children.RemoveAt (i);
            }
        }
    }
}