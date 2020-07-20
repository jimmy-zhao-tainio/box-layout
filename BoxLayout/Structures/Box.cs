using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boxing
{
    public class BoxHorizontal : Box
    {
        public BoxHorizontal () : base (Orientation.Horizontal)
        {
        }

        public BoxHorizontal (Size userMinSize) : base (Orientation.Horizontal)
        {
            UserMinSize = userMinSize;
        }
    }

    public class BoxVertical : Box
    {
        public BoxVertical () : base (Orientation.Vertical)
        {
        }

        public BoxVertical (Size userMinSize) : base (Orientation.Vertical)
        {
            UserMinSize = userMinSize;
        }
    }

    public class Box
    {
        public event System.EventHandler OnMinSizeChanged;
        public Orientation Orientation;

        public Box (Orientation orientation)
        {
            Orientation = orientation;
            LayoutPosition = Point.New (Orientation);
            LayoutSize = Size.New (Orientation);
            ActualSize = Size.New (Orientation);
            Computed = new BoxComputed ();
            _userMinSize = Size.New (0, 0, Orientation);
            _userMaxSize = Size.New (int.MaxValue, int.MaxValue, Orientation);
            _expand = Expand.New (Orientation);
        }

        private bool _minIsValid = false;
        protected bool MinIsValid
        {
            get { return _minIsValid; }
            set
            {
                if (value == _minIsValid)
                    return;
                _minIsValid = value;
                if (_minIsValid == true)
                    return;
                if (OnMinSizeChanged != null)
                    OnMinSizeChanged (this, EventArgs.Empty);
            }
        }

        private Size _min;
        public Size Min
        {
            get
            {
                if (_minIsValid == true)
                    return _min;
                _childrenMin = GetChildrenMin ();
                _min = GetMin ();
                _minIsValid = true;
                return _min;
            }
        }

        private Size _childrenMin;
        public Size ChildrenMin
        {
            get
            {
                if (_minIsValid == true)
                    return _childrenMin;
                _childrenMin = GetChildrenMin ();
                _min = GetMin ();
                _minIsValid = true;
                return _childrenMin;
            }
        }

        public List<Box> Children = new List<Box> ();

        private bool _wrap = false;
        public bool Wrap
        {
            get { return _wrap; }
            set
            {
                if (_wrap == value)
                    return;
                _wrap = value;
                MinIsValid = false;
            }
        }

        private Expand _expand;
        public Expand Expand
        {
            get { return _expand; }
            set
            {
                _expand = value;
                MinIsValid = false;
            }
        }

        public bool EqualSizeMain = false;
        public bool EqualSizeCross = false;
        public SelfEqualSize SelfEqualSizeMain = SelfEqualSize.Inherit;
        public SelfEqualSize SelfEqualSizeCross = SelfEqualSize.Inherit;

        public AlignMain AlignMain = AlignMain.Start;
        public AlignCross AlignCross = AlignCross.Start;
        public LineAlignCross LineAlignCross = LineAlignCross.Start;
        public SelfAlignCross SelfAlignCross = SelfAlignCross.Inherit;

        private Size _userMinSize;
        public Size UserMinSize
        {
            get { return _userMinSize; }
            set
            {
                if (_userMinSize.Width == value.Width && _userMinSize.Height == value.Height)
                    return;
                _userMinSize.Width = value.Width;
                _userMinSize.Height = value.Height;
                MinIsValid = false;
            }
        }

        private Size _userMaxSize;
        public Size UserMaxSize
        {
            get { return _userMaxSize; }
            set
            {
                if (_userMaxSize.Width == value.Width && _userMaxSize.Height == value.Height)
                    return;
                _userMaxSize.Width = value.Width;
                _userMaxSize.Height = value.Height;
                MinIsValid = false;
            }
        }

        public Point LayoutPosition;
        public Size LayoutSize;
        public Size ActualSize;
        public BoxComputed Computed;

        public List<Line> Lines;
        
        protected void Child_OnMinSizeChanged (object sender, EventArgs e)
        {
            MinIsValid = false;
        }

        public Size GetChildrenMin ()
        {
            Size size = Size.New (Orientation);

            foreach (Box box in Children)
            {
                if (size.Main < _userMaxSize.Main)
                {
                    if (Wrap == false)
                        size.Main += box.Min.GetMain (Orientation);
                    else
                        size.Main = Math.Max (size.Main, box.Min.GetMain (Orientation));
                }
                if (size.Cross < _userMaxSize.Cross)
                {
                    size.Cross = Math.Max (size.Cross, box.Min.GetCross (Orientation));
                }
            }
            return size;
        }

        public Size GetMin ()
        {
            Size size = Size.New (_childrenMin.Width, _childrenMin.Height, Orientation);

            size.Main = Math.Max (size.Main, _userMinSize.Main);
            size.Cross = Math.Max (size.Cross, _userMinSize.Cross);
            size.Main = Math.Min (size.Main, _userMaxSize.Main);
            size.Cross = Math.Min (size.Cross, _userMaxSize.Cross);
            return size;
        }

        public void Pack (Box child)
        {
            Children.Add(child);
            child.OnMinSizeChanged += Child_OnMinSizeChanged;
            MinIsValid = false;
        }

        public void Unpack (Box child)
        {
            for (int i = 0; i < Children.Count; i++)
            {
                if (Children[i] != child)
                    continue;
                Children.RemoveAt (i);
                child.OnMinSizeChanged -= Child_OnMinSizeChanged;
                MinIsValid = false;
            }
        }
    }
}