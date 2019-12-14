using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boxing
{
    abstract public class BoxBase
    {
        public event System.EventHandler OnMinSizeChanged;
        public Orientation Orientation;

        public BoxBase (Orientation orientation)
        {
            Orientation = orientation;
            _actualSize = Size.New (Orientation);
            _userMinSize = Size.New (0, 0, Orientation);
            _userMaxSize = Size.New (int.MaxValue, int.MaxValue, Orientation);
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

        protected List<Box> _children = new List<Box> ();
        public List<Box> Children
        {
            get { return _children; }
        }

        /*
            "Parent setting"

            Wraps items to new lines.
        */
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

        // Toggles the use of extraspace on the main and cross axis.
        // Parent box attempts to distributes an equal amount of space to all children with this setting enabled.
        // See the Fill struct for more details on remainders.
        public bool Fill
        {
            get { return FillMain && FillCross; }
            set
            {
                FillMain = value;
                FillCross = value;
            }
        }

        // Toggles the use of extraspace on the main axis.
        private bool _fillMain = false;
        public bool FillMain
        {
            get { return _fillMain; }
            set
            {
                if (_fillMain == value)
                    return;
                _fillMain = value;
                MinIsValid = false;
            }
        }

        // Toggles the use of extraspace on the cross axis.
        private bool _fillCross = false;
        public bool FillCross
        {
            get { return _fillCross; }
            set
            {
                if (_fillCross == value)
                    return;
                _fillCross = value;
                MinIsValid = false;
            }
        }

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

        private Point _layoutPosition;
        public Point LayoutPosition
        {
            get { return _layoutPosition; }
            set { _layoutPosition = value; }
        }

        private Size _layoutSize;
        public Size LayoutSize
        {
            get { return _layoutSize; }
            set { _layoutSize = value; }
        }

        private Size _actualSize;
        public Size ActualSize
        {
            get { return _actualSize; }
            set { _actualSize = value; }
        }

        protected void Child_OnMinSizeChanged (object sender, EventArgs e)
        {
            MinIsValid = false;
        }

        public Size GetChildrenMin ()
        {
            Size size = Size.New (Orientation);

            foreach (Box box in _children)
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
            _children.Add(child);
            child.OnMinSizeChanged += Child_OnMinSizeChanged;
            MinIsValid = false;
        }

        public void Unpack (Box child)
        {
            for (int i = 0; i < _children.Count; i++)
            {
                if (_children[i] != child)
                    continue;
                _children.RemoveAt (i);
                child.OnMinSizeChanged -= Child_OnMinSizeChanged;
                MinIsValid = false;
            }
        }
    }
}