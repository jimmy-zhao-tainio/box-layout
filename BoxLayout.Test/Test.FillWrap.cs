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
    public class TestFillCross
    {
        [TestMethod]
        public void FillWrapCrossTrue()
        {
            Box top = BoxCreate.FromXml (@"
                <hbox wrap=""true"">
                    <vbox minSize=""10, 10"" fillCross=""true"" />
                    <vbox minSize=""10, 10"" fillCross=""true"" />
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
        public void FillWrapCrossSomeTrue()
        {
            Box top = BoxCreate.FromXml (@"
                <hbox wrap=""true"">
                    <vbox minSize=""10, 10"" fillCross=""false"" />
                    <vbox minSize=""10, 10"" fillCross=""true"" />
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
        public void FillWrapCrossFalse()
        {
            Box top = BoxCreate.FromXml (@"
                <hbox wrap=""true"">
                    <vbox minSize=""10, 10"" fillCross=""false"" />
                    <vbox minSize=""10, 10"" fillCross=""false"" />
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
    }
}