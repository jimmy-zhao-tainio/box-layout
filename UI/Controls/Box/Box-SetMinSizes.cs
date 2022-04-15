using System;
using UI.Structures;

namespace UI.Controls
{
    public partial class Box
    {
        public void SetMinSizes()
        {
            // Recursively set minimum main and cross lengths. 
            foreach (Box child in Children)
                child.SetMinSizes();

            ChildrenMin = Size.New(Orientation);
            foreach (Box child in Children)
            {
                // Catch anything that sticks out in either direction.
                if (ChildrenMin.Main < UserMaxSize.Main)
                {
                    if (Wrap == false)
                        ChildrenMin.Main += child.Min.GetMain(Orientation);
                    else
                        ChildrenMin.Main = Math.Max(ChildrenMin.Main, child.Min.GetMain(Orientation));
                }
                if (ChildrenMin.Cross < UserMaxSize.Cross)
                {
                    ChildrenMin.Cross = Math.Max(ChildrenMin.Cross, child.Min.GetCross(Orientation));
                }
            }
            Min = Size.New(ChildrenMin.Width, ChildrenMin.Height, Orientation);

            // Check if minimum possible main is smaller than user set minimum. If so use UserMinimum.
            Min.Main = Math.Max(Min.Main, UserMinSize.Main);
            // Check if minimum main is larger than UserMaximum. If so use UserMaximum.
            Min.Main = Math.Min(Min.Main, UserMaxSize.Main);
            // Same as above.
            Min.Cross = Math.Max(Min.Cross, UserMinSize.Cross);
            Min.Cross = Math.Min(Min.Cross, UserMaxSize.Cross);
        }
    }
}