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
    public class TestFill
    {
        [TestMethod]
        public void FillMainTrue()
        {
            Box top = BoxCreate.FromXml (@"
                <hbox>
                    <vbox minSize=""10, 10"" fillMain=""true"" />
                </hbox>
            ");
            Box v1 = top.Children[0];
            top.Layout (100, 100);
            Assert.IsTrue (v1.LayoutPosition.Equals (0, 0));
            Assert.IsTrue (v1.LayoutSize.Equals (100, 10));
        }

        [TestMethod]
        public void FillCrossTrue()
        {
            Box top = BoxCreate.FromXml (@"
                <hbox>
                    <vbox minSize=""10, 10"" fillCross=""true"" />
                </hbox>
            ");
            Box v1 = top.Children[0];
            top.Layout (100, 100);
            Assert.IsTrue (v1.LayoutPosition.Equals (0, 0));
            Assert.IsTrue (v1.LayoutSize.Equals (10, 100));
        }

        [TestMethod]
        public void FillTrue()
        {
            Box top = BoxCreate.FromXml (@"
                <hbox>
                    <vbox minSize=""10, 10"" fill=""true"" />
                    <vbox minSize=""10, 10"" fill=""true"" />
                </hbox>
            ");
            Box v1 = top.Children[0];
            Box v2 = top.Children[1];
            top.Layout (100, 100);
            Assert.IsTrue (v1.LayoutPosition.Equals (0, 0));
            Assert.IsTrue (v1.LayoutSize.Equals (50, 100));
            Assert.IsTrue (v2.LayoutPosition.Equals (50, 0));
            Assert.IsTrue (v2.LayoutSize.Equals (50, 100));
        }

        [TestMethod]
        public void FillFalse()
        {
            Box top = BoxCreate.FromXml (@"
                <hbox>
                    <vbox minSize=""10, 10"" fill=""false"" />
                    <vbox minSize=""10, 10"" fill=""false"" />
                </hbox>
            ");
            Box v1 = top.Children[0];
            Box v2 = top.Children[1];
            top.Layout (100, 100);
            Assert.IsTrue (v1.LayoutPosition.Equals (0, 0));
            Assert.IsTrue (v1.LayoutSize.Equals (10, 10));
            Assert.IsTrue (v2.LayoutPosition.Equals (10, 0));
            Assert.IsTrue (v2.LayoutSize.Equals (10, 10));
        }
    }
}