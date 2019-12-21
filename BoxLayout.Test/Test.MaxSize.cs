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
    public class TestMaxSize
    {
        [TestMethod]
        public void TopMaxSizeSmaller()
        {
            Box top = BoxCreate.FromXml (@"
                <hbox>
                    <hbox max-size=""10, 10"">
                        <vbox min-size=""10, 10"" />
                        <vbox min-size=""10, 10"" />
                    </hbox>
                </hbox>
            ");

            top.Layout (1000, 1000);
            Box max = top.Children[0];

            Assert.IsTrue (top.LayoutSize.Equals (1000, 1000));
            Assert.IsTrue (top.ActualSize.Equals (10, 10));

            Assert.IsTrue (max.LayoutSize.Equals (10, 10));
            Assert.IsTrue (max.ActualSize.Equals (20, 10));
        }
    }
}