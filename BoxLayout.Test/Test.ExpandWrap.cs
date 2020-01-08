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
    public class TestExpandCross
    {
        [TestMethod]
        public void ExpandWrapCrossTrue()
        {
            Box top = BoxCreate.FromXml (@"
                <hbox wrap=""true"">
                    <hbox min-size=""10, 10"" expand-cross=""true"" />
                    <hbox min-size=""10, 10"" expand-cross=""true"" />
                </hbox>
            ");
            Box v1 = top.Children[0];
            Box v2 = top.Children[1];
            top.Layout (10, 100);
            Assert.IsTrue (v1.LayoutPosition.Equals (0, 0));
            Assert.IsTrue (v1.LayoutSize.Equals (10, 50));
            Assert.IsTrue (v2.LayoutPosition.Equals (0, 50));
            Assert.IsTrue (v2.LayoutSize.Equals (10, 50));
        }

        [TestMethod]
        public void ExpandWrapCrossSomeTrue()
        {
            Box top = BoxCreate.FromXml (@"
                <hbox wrap=""true"">
                    <hbox min-size=""10, 10"" expand-cross=""false"" />
                    <hbox min-size=""10, 10"" expand-cross=""true"" />
                </hbox>
            ");
            Box v1 = top.Children[0];
            Box v2 = top.Children[1];
            top.Layout (10, 100);
            Assert.IsTrue (v1.LayoutPosition.Equals (0, 0));
            Assert.IsTrue (v1.LayoutSize.Equals (10, 10));
            Assert.IsTrue (v2.LayoutPosition.Equals (0, 10));
            Assert.IsTrue (v2.LayoutSize.Equals (10, 90));
        }

        [TestMethod]
        public void ExpandWrapCrossFalse()
        {
            Box top = BoxCreate.FromXml (@"
                <hbox wrap=""true"">
                    <hbox min-size=""10, 10"" expand-cross=""false"" />
                    <hbox min-size=""10, 10"" expand-cross=""false"" />
                </hbox>
            ");
            Box v1 = top.Children[0];
            Box v2 = top.Children[1];
            top.Layout (10, 100);
            Assert.IsTrue (v1.LayoutPosition.Equals (0, 0));
            Assert.IsTrue (v1.LayoutSize.Equals (10, 10));
            Assert.IsTrue (v2.LayoutPosition.Equals (0, 10));
            Assert.IsTrue (v2.LayoutSize.Equals (10, 10));
        }


        [TestMethod]
        public void ExpandWrapMaxSize()
        {
            Box top = BoxCreate.FromXml (@"
                <hbox>
                    <hbox wrap=""true"" expand=""true"">
                        <vbox expand=""true"" min-size=""10, 10"" max-size=""10, 10"" />
                        <vbox expand=""true"" min-size=""10, 0""  max-size=""10, 10"" />
                    </hbox>
                </hbox>
            ");
            Box h1 = top.Children[0];
            Box v1 = h1.Children[0];
            Box v2 = h1.Children[1];
            top.Layout (10, 30);
            Assert.IsTrue (v1.LayoutPosition.Equals (0, 0));
            Assert.IsTrue (v1.LayoutSize.Equals (10, 10));
            Assert.IsTrue (v2.LayoutPosition.Equals (0, 10));
            Assert.IsTrue (v2.LayoutSize.Equals (10, 10));
        }
    }
}