using Microsoft.VisualStudio.TestTools.UnitTesting;
using UI.Layout;
using UI.Controls;

namespace Unit
{
    [TestClass]
    public class TestLayout
    {
        [TestMethod]
        public void Layout1H2H()
        {
            Box top = BoxCreate.FromXml (@"
                <hbox>
                    <hbox min-size=""10, 10"" />
                    <hbox min-size=""20, 20"" />
                </hbox>
            ");
            Box h1 = top.Children[0];
            Box h2 = top.Children[1];

            LayoutManager.Process (top, 30, 20);

            Assert.IsTrue (h1.LayoutPosition.Equals (0, 0));
            Assert.IsTrue (h1.LayoutSize.Equals (10, 10));
            Assert.IsTrue (h2.LayoutPosition.Equals (10, 0));
            Assert.IsTrue (h2.LayoutSize.Equals (20, 20));
        }

        [TestMethod]
        public void LayoutMinSizeNoExpand()
        {
            Box top = BoxCreate.FromXml (@"
                <hbox>
                    <vbox min-size=""10, 20"" />
                    <vbox min-size=""10, 10"" />
                </hbox>
            ");
            Box v1 = top.Children[0];
            Box v2 = top.Children[1];
            LayoutManager.Process (top, 100, 100);
            Assert.IsTrue (v1.LayoutPosition.Equals (0, 0));
            Assert.IsTrue (v1.LayoutSize.Equals (10, 20));
            Assert.IsTrue (v2.LayoutPosition.Equals (10, 0));
            Assert.IsTrue (v2.LayoutSize.Equals (10, 10));
        }
    }
}