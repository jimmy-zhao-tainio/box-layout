using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boxing 
{
    public class MinimumSize
    {
        static public void SetMinimumSize (Box box)
        {
            foreach (Box child in box.Children)
                SetMinimumSize (child);
            box.ChildrenMin = GetChildrenMin (box);
            box.Min = GetMin (box);
        }

        static private Size GetChildrenMin (Box box)
        {
            Size size = Size.New (box.Orientation);

            foreach (Box child in box.Children)
            {
                if (size.Main < box.UserMaxSize.Main)
                {
                    if (box.Wrap == false)
                        size.Main += child.Min.GetMain (box.Orientation);
                    else
                        size.Main = Math.Max (size.Main, child.Min.GetMain (box.Orientation));
                }
                if (size.Cross < box.UserMaxSize.Cross)
                {
                    size.Cross = Math.Max (size.Cross, child.Min.GetCross (box.Orientation));
                }
            }
            return size;
        }

        static private Size GetMin (Box box)
        {
            Size size = Size.New (box.ChildrenMin.Width, box.ChildrenMin.Height, box.Orientation);

            size.Main = Math.Max (size.Main, box.UserMinSize.Main);
            size.Cross = Math.Max (size.Cross, box.UserMinSize.Cross);
            size.Main = Math.Min (size.Main, box.UserMaxSize.Main);
            size.Cross = Math.Min (size.Cross, box.UserMaxSize.Cross);
            return size;
        }
    }
}
