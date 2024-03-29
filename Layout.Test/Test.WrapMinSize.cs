﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using UI.Layout;
using UI.Controls;

namespace Unit
{
    [TestClass]
    public class TestWrapMinSize
    {
        [TestMethod]
        public void WrapMinSize ()
        {
            Box top = BoxCreate.FromXml (@"
                <vbox wrap=""false"">
                    <hbox wrap=""false"">
                        <vbox min-size=""50, 50"" />
                        <vbox min-size=""50, 50"" />
                    </hbox>
                    <hbox wrap=""true"">
                        <vbox min-size=""50, 50"" />
                        <vbox min-size=""50, 50"" />
                    </hbox>
                    <hbox wrap=""false"">
                        <vbox min-size=""50, 50"" />
                        <vbox min-size=""50, 50"" />
                    </hbox>
                </vbox>
            ");
            Box v1 = top.Children[0];
            Box v2 = top.Children[1];
            Box v3 = top.Children[2];
            LayoutManager.Process (top, 50, 150);
            Assert.IsTrue (v1.LayoutPosition.Equals (0, 0));
            Assert.IsTrue (v2.LayoutPosition.Equals (0, 50));
            Assert.IsTrue (v3.LayoutPosition.Equals (0, 100));
            Assert.IsTrue (v1.LayoutSize.Equals (100, 50));
            Assert.IsTrue (v2.LayoutSize.Equals (100, 50));
            Assert.IsTrue (v3.LayoutSize.Equals (100, 50));
        }

        [TestMethod]
        public void WrapMinSizeSimple ()
        {
            Box top = BoxCreate.FromXml (@"
                <vbox wrap=""false"">
                    <hbox wrap=""true"">
                        <vbox min-size=""50, 50"" />
                        <vbox min-size=""50, 50"" />
                    </hbox>
                </vbox>
            ");
            Box v1 = top.Children[0];
            LayoutManager.Process (top, 50, 100);
            Assert.IsTrue (v1.LayoutPosition.Equals (0, 0));
            Assert.IsTrue (v1.LayoutSize.Equals (50, 50));
            Assert.IsTrue (v1.ContentSize.Equals (50, 100));
        }
    }
}