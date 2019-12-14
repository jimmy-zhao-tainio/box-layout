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
    public class TestLayout
    {
        [TestMethod]
        public void Layout1H2H()
        {
            Box top = new BoxHorizontal ();
            Box h1 = new BoxHorizontal ();
            Box h2 = new BoxHorizontal ();

            h1.UserMinSize = new Size (10, 10);
            h2.UserMinSize = new Size (20, 20);

            top.Pack (h1);
            top.Pack (h2);
            top.Layout(30, 20);

            Assert.IsTrue (h1.LayoutPosition.Equals (0, 0));
            Assert.IsTrue (h1.LayoutSize.Equals (10, 20));
            Assert.IsTrue (h2.LayoutPosition.Equals (10, 0));
            Assert.IsTrue (h2.LayoutSize.Equals (20, 20));
        }

        [TestMethod]
        public void Layout1H2V2H()
        {
            // +---------------+ <-- parent
            // | +-+ +-------+ | <-- v1 (0, 0, 0, 20) and v2 (0, 10, 10, 20)
            // | | | | +---+ | | <-- h1 (0, 0, 10, 10)
            // | | | | |   | | |
            // | | | | +---+ | | 
            // | | | | +---+ | | <-- h2 (0, 10, 10, 10)
            // | | | | |   | | |
            // | | | | +---+ | |
            // | +-+ +-------+ |
            // +---------------+

            Box top = new BoxHorizontal ();
            Box v1 = new BoxVertical ();
            Box v2 = new BoxVertical ();
            Box h1 = new BoxHorizontal ();
            Box h2 = new BoxHorizontal ();

            h1.UserMinSize = new Size (10, 10);
            h2.UserMinSize = new Size (10, 10);

            top.Pack(v1);
            top.Pack(v2);
            v2.Pack (h1);
            v2.Pack (h2);
            top.Layout (10, 20);

            Assert.IsTrue (h1.LayoutPosition.Equals (0, 0));
            Assert.IsTrue (h1.LayoutSize.Equals (10, 10));
            Assert.IsTrue (h2.LayoutPosition.Equals (0, 10));
            Assert.IsTrue (h2.LayoutSize.Equals (10, 10));
            Assert.IsTrue (v1.LayoutPosition.Equals (0, 0));
            Assert.IsTrue (v1.LayoutSize.Equals (0, 20));
            Assert.IsTrue (v2.LayoutPosition.Equals (0, 0));
            Assert.IsTrue (v2.LayoutSize.Equals (10, 20));
        }

        [TestMethod]
        public void Layout1H2V3H ()
        {
            // +---------------------+ <-- parent
            // | +-------+ +-------+ | <-- v1 (0, 0, 0, 20) and v2 (0, 10, 10, 20)
            // | | +---+ | | +---+ | | <-- h1 (0, 0, 10, 10)
            // | | |h3 | | | |   | | | <-- h3 (0, 0, 10, 10)
            // | | +---+ | | +---+ | | 
            // | |       | | +---+ | | <-- h2 (0, 10, 10, 10)
            // | |       | | |   | | |
            // | |       | | +---+ | |
            // | +-------+ +-------+ |
            // +---------------------+

            Box top = new BoxHorizontal ();
            Box v1 = new BoxVertical ();
            Box v2 = new BoxVertical ();
            Box h1 = new BoxHorizontal ();
            Box h2 = new BoxHorizontal ();
            Box h3 = new BoxHorizontal ();

            h1.UserMinSize = new Size (10, 10);
            h2.UserMinSize = new Size (10, 10);
            h3.UserMinSize = new Size (10, 10);

            top.Pack (v1);
            top.Pack (v2);
            v2.Pack (h1);
            v2.Pack (h2);
            v1.Pack (h3);
            top.Layout (20, 20);

            Assert.IsTrue (h3.LayoutPosition.Equals (0, 0));
            Assert.IsTrue (h3.LayoutSize.Equals (10, 10));
            Assert.IsTrue (h1.LayoutPosition.Equals (0, 0));
            Assert.IsTrue (h1.LayoutSize.Equals (10, 10));
            Assert.IsTrue (h2.LayoutPosition.Equals (0, 10));
            Assert.IsTrue (v1.LayoutPosition.Equals (0, 0));
            // Make sure v1 gets the same height as v2.
            Assert.IsTrue (v1.LayoutSize.Equals (10, 20));
            Assert.IsTrue (v2.LayoutPosition.Equals (10, 0));
            Assert.IsTrue (v2.LayoutSize.Equals (10, 20));
            Assert.IsTrue (h2.LayoutSize.Equals (10, 10));
        }
    }
}