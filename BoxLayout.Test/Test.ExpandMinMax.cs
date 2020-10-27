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
    public class TestExpandMinMax
    {
        [TestMethod]
        public void ExpandMinMaxMain()
        {
            Box top = BoxCreate.FromXml (@"
                <hbox>
                    <hbox min-size=""10, 10"" max-size=""20, 10"" expand-main=""true"" />
                    <hbox min-size=""10, 10"" expand-main=""true"" />
                </hbox>
            ");
            Box h1 = top.Children[0];
            Box h2 = top.Children[1];
            Layout.Process (top, 50, 10);
            Assert.IsTrue (h1.LayoutPosition.Equals (0, 0));
            Assert.IsTrue (h1.LayoutSize.Equals (20, 10));
            Assert.IsTrue (h2.LayoutPosition.Equals (20, 0));
            Assert.IsTrue (h2.LayoutSize.Equals (30, 10));
        }
    }
}