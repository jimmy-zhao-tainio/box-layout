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

            top.UserMinSize.Width = 100;
            top.UserMinSize.Height = 100;

            Layout.Process (top, 200, 200);

            Assert.IsTrue (top.LayoutSize.Equals (200, 200));
            Assert.IsTrue (top.ActualSize.Equals (100, 100));
        }

        [TestMethod]
        public void TopMinSizeLarger()
        {
            Box top = new BoxHorizontal ();

            top.UserMinSize.Width = 200;
            top.UserMinSize.Height = 200;

            Layout.Process (top, 100, 100);

            Assert.IsTrue (top.LayoutSize.Equals (100, 100));
            Assert.IsTrue (top.ActualSize.Equals (200, 200));
        }
    }
}