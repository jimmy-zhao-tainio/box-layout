using Microsoft.VisualStudio.TestTools.UnitTesting;
using UI.Layout;
using UI.Controls;

namespace Unit
{
    [TestClass]
    public class TestMinxSize
    {
        [TestMethod]
        public void TopMinSizeSmaller()
        {
            Box top = new BoxHorizontal ();

            top.UserMinSize.Width = 100;
            top.UserMinSize.Height = 100;

            LayoutManager.Process (top, 200, 200);

            Assert.IsTrue (top.LayoutSize.Equals (200, 200));
            Assert.IsTrue (top.ContentSize.Equals (100, 100));
        }

        [TestMethod]
        public void TopMinSizeLarger()
        {
            Box top = new BoxHorizontal ();

            top.UserMinSize.Width = 200;
            top.UserMinSize.Height = 200;
            top.HorizontalScrollbar.Mode = UI.Structures.ScrollbarMode.Hidden;
            top.VerticalScrollbar.Mode = UI.Structures.ScrollbarMode.Hidden;

            LayoutManager.Process (top, 100, 100);

            Assert.IsTrue (top.LayoutSize.Equals (100, 100));
            Assert.IsTrue (top.ContentSize.Equals (200, 200));
        }
    }
}