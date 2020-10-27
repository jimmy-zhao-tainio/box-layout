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
            Layout.Process (top, 0, 0);
            Assert.IsTrue (top.Min.Equals (0, 0));
        }

        [TestMethod]
        public void ComputeMinSize1H1H()
        {
            Box top = new BoxHorizontal ();
            Box child = new BoxHorizontal ();

            child.UserMinSize.Width = 7;
            child.UserMinSize.Height = 11;
            top.Pack(child);
            Layout.Process (top, 0, 0);
            Assert.IsTrue (top.Min.Equals (7, 11));
        }

        [TestMethod]
        public void ComputeMinSize1H2V()
        {
            Box top = new BoxHorizontal ();
            Box child1 = new BoxVertical ();
            Box child2 = new BoxVertical ();

            child1.UserMinSize.Width = 7;
            child1.UserMinSize.Height = 11;
            top.Pack(child1);
            child2.UserMinSize.Width = 13;
            child2.UserMinSize.Height = 17;
            top.Pack(child2);
            Layout.Process (top, 0, 0);
            Assert.IsTrue (top.Min.Equals (7 + 13, 17));
        }

        [TestMethod]
        public void ComputeMinSize1V2H()
        {
            Box top = new BoxVertical ();
            Box child1 = new BoxHorizontal ();
            Box child2 = new BoxHorizontal ();

            child1.UserMinSize.Width = 7;
            child1.UserMinSize.Height = 11;
            top.Pack(child1);
            child2.UserMinSize.Width = 13;
            child2.UserMinSize.Height = 17;
            top.Pack(child2);
            Layout.Process (top, 0, 0);
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

            h1.UserMinSize.Width = 10;
            h1.UserMinSize.Height = 10;
            h2.UserMinSize.Width = 30;
            h2.UserMinSize.Height = 30;
            h3.UserMinSize.Width = 50;
            h3.UserMinSize.Height = 50;
            h4.UserMinSize.Width = 70;
            h4.UserMinSize.Height = 70;

            top.Pack(v1);
            top.Pack(v2);
            v1.Pack(h1);
            v1.Pack(h2);
            v2.Pack(h3);
            v2.Pack(h4);
            Layout.Process (top, 0, 0);
            Assert.IsTrue (top.Min.Equals (30 + 70, 50 + 70));
        }

        [TestMethod]
        public void UserMax ()
        {
            Box top = BoxCreate.FromXml (@"
                <hbox max-size=""10, 20"">
                    <vbox min-size=""100, 100"" />
                </hbox>
            ");
            Layout.Process (top, 0, 0);
            Assert.IsTrue (top.Min.Equals (10, 20));
        }
    }
}