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
    public class TestMainAlign
    {
        [TestMethod]
        public void Right()
        {
            Box top = BoxCreate.FromXml (@"
                <hbox alignMain=""right"">
                    <vbox min-size=""10, 10"" />
                    <vbox min-size=""10, 10"" />
                </hbox>
            ");
            Box v1 = top.Children[0];
            Box v2 = top.Children[1];
            top.Layout (100, 10);
            Assert.IsTrue (v1.LayoutPosition.Equals (80, 0));
            Assert.IsTrue (v1.LayoutSize.Equals (10, 10));
            Assert.IsTrue (v2.LayoutPosition.Equals (90, 10));
            Assert.IsTrue (v2.LayoutSize.Equals (10, 10));
        }
    }
}