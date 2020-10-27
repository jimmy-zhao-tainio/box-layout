using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Boxing;

namespace Unit
{
    /*
    [TestClass]
    public class TestWrapEqualSize
    {
        [TestMethod]
        public void WrapEqualSizeMain ()
        {
            Box top = BoxCreate.FromXml (@"
                <hbox wrap=""true"" equal-size-main=""true"">
                    <hbox min-size=""50, 1"" />
                    <hbox min-size=""10, 1"" />
                    <hbox min-size=""10, 1"" />
                    <hbox min-size=""10, 1"" />
                    <hbox min-size=""10, 1"" />
                </hbox>
            ");
            Box h1 = top.Children[0];
            Box h2 = top.Children[1];
            Box h3 = top.Children[2];
            Box h4 = top.Children[3];
            Box h5 = top.Children[4];
            Layout.Process (top, 100, 150);

            Assert.IsTrue (h1.LayoutPosition.Cross == 0);
            Assert.IsTrue (h2.LayoutPosition.Cross == 0);
            Assert.IsTrue (h3.LayoutPosition.Cross == 1);
            Assert.IsTrue (h4.LayoutPosition.Cross == 1);
            Assert.IsTrue (h5.LayoutPosition.Cross == 2);
        }
    }
    */
}