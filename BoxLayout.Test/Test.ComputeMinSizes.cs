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
    public class TestComputeMinSize
    {
        [TestMethod]
        public void ComputeMinSizeZero()
        {
            Box top = new BoxHorizontal ();
            Box child = new BoxHorizontal ();

            top.Pack(child);
            Assert.IsTrue (top.Min.Equals (0, 0));
        }

        [TestMethod]
        public void ComputeMinSize1H1H()
        {
            Box top = new BoxHorizontal ();
            Box child = new BoxHorizontal ();

            child.UserMinSize = new Size (7, 11);
            top.Pack(child);
            Assert.IsTrue (top.Min.Equals (7, 11));
        }

        [TestMethod]
        public void ComputeMinSize1H2V()
        {
            Box top = new BoxHorizontal ();
            Box child1 = new BoxVertical ();
            Box child2 = new BoxVertical ();

            child1.UserMinSize = new Size (7, 11);
            top.Pack(child1);
            child2.UserMinSize = new Size (13, 17);
            top.Pack(child2);
            Assert.IsTrue (top.Min.Equals (7 + 13, 17));
        }

        [TestMethod]
        public void ComputeMinSize1V2H()
        {
            Box top = new BoxVertical ();
            Box child1 = new BoxHorizontal ();
            Box child2 = new BoxHorizontal ();

            child1.UserMinSize = new Size (7, 11);
            top.Pack(child1);
            child2.UserMinSize = new Size (13, 17);
            top.Pack(child2);
            Assert.IsTrue (top.Min.Equals (13, 11 + 17));
        }

        [TestMethod]
        public void ComputeMinSize1H2V4H()
        {
            Box top = new BoxHorizontal ();
            Box v1 = new BoxVertical ();
            Box v2 = new BoxVertical ();
            Box h1 = new BoxHorizontal ();
            Box h2 = new BoxHorizontal ();
            Box h3 = new BoxHorizontal ();
            Box h4 = new BoxHorizontal ();

            h1.UserMinSize = new Size (10, 10);
            h2.UserMinSize = new Size (30, 30);
            h3.UserMinSize = new Size (50, 50);
            h4.UserMinSize = new Size (70, 70);

            top.Pack(v1);
            top.Pack(v2);
            v1.Pack(h1);
            v1.Pack(h2);
            v2.Pack(h3);
            v2.Pack(h4);
            Assert.IsTrue (top.Min.Equals (30 + 70, 50 + 70));
        }

        [TestMethod]
        public void UserMax ()
        {
            Box top = BoxCreate.FromXml (@"
                <hbox maxSize=""10, 20"">
                    <vbox minSize=""100, 100"" />
                </hbox>
            ");
            Assert.IsTrue (top.Min.Equals (10, 20));
        }
    }
}