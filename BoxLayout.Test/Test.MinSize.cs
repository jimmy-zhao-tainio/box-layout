using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Boxing;

namespace Unit
{
    [TestClass]
    public class TestMinxSize
    {
        [TestMethod]
        public void TopMinSizeSmaller()
        {
            Box top = new BoxHorizontal ();

            top.UserMinSize = new Size (100, 100);

            top.Layout(200, 200);

            Assert.IsTrue (top.LayoutSize.Equals (200, 200));
            Assert.IsTrue (top.ActualSize.Equals (100, 100));
        }

        [TestMethod]
        public void TopMinSizeLarger()
        {
            Box top = new BoxHorizontal ();

            top.UserMinSize = new Size (200, 200);

            top.Layout(100, 100);

            Assert.IsTrue (top.LayoutSize.Equals (100, 100));
            Assert.IsTrue (top.ActualSize.Equals (200, 200));
        }
    }
}