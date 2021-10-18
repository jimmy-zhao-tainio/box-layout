using System;
using UI.Structures;
using UI.Controls;

namespace UI.Layout
{
    public partial class LayoutManager
    {
        static private void SetMinMainCrossSizes (Box box)
        {
            // Recursively set minimum main and cross lengths. 
            foreach (Box child in box.Children)
                SetMinMainCrossSizes (child);

            box.ChildrenMin = Size.New (box.Orientation);
            foreach (Box child in box.Children)
            {
                // Catch anything that sticks out in either direction.
                if (box.ChildrenMin.Main < box.UserMaxSize.Main)
                {
                    if (box.Wrap == false)
                        box.ChildrenMin.Main += child.Min.GetMain (box.Orientation);
                    else
                        box.ChildrenMin.Main = Math.Max (box.ChildrenMin.Main, child.Min.GetMain (box.Orientation));
                }
                if (box.ChildrenMin.Cross < box.UserMaxSize.Cross)
                {
                    box.ChildrenMin.Cross = Math.Max (box.ChildrenMin.Cross, child.Min.GetCross (box.Orientation));
                }
            }
            box.Min = Size.New (box.ChildrenMin.Width, box.ChildrenMin.Height, box.Orientation);

            // Check if minimum possible main is smaller than user set minimum. If so use UserMinimum.
            box.Min.Main = Math.Max (box.Min.Main, box.UserMinSize.Main);
            // Check if minimum main is larger than UserMaximum. If so use UserMaximum.
            box.Min.Main = Math.Min (box.Min.Main, box.UserMaxSize.Main);
            // Same as above.
            box.Min.Cross = Math.Max (box.Min.Cross, box.UserMinSize.Cross);
            box.Min.Cross = Math.Min (box.Min.Cross, box.UserMaxSize.Cross);
        }
    }
}