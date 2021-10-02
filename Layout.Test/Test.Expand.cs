using Microsoft.VisualStudio.TestTools.UnitTesting;
using UI.Layout;
using UI.Controls;

namespace Unit
{
    [TestClass]
    public class TestExpand
    {
        [TestMethod]
        public void ExpandMainTrue()
        {
            Box top = BoxCreate.FromXml (@"
                <vbox>
                    <hbox min-size=""10, 10"" expand-main=""true"" />
                </vbox>
            ");
            Box v1 = top.Children[0];
            LayoutManager.Process (top, 100, 100);
            Assert.IsTrue (v1.LayoutPosition.Equals (0, 0));
            Assert.IsTrue (v1.LayoutSize.Equals (100, 10));
        }

        [TestMethod]
        public void ExpandCrossTrue()
        {
            Box top = BoxCreate.FromXml (@"
                <vbox>
                    <hbox min-size=""10, 10"" expand-cross=""true"" />
                </vbox>
            ");
            Box v1 = top.Children[0];
            LayoutManager.Process (top, 100, 100);
            Assert.IsTrue (v1.LayoutPosition.Equals (0, 0));
            Assert.IsTrue (v1.LayoutSize.Equals (10, 100));
        }

        [TestMethod]
        public void ExpandTrue()
        {
            Box top = BoxCreate.FromXml (@"
                <hbox>
                    <vbox min-size=""10, 10"" expand=""true"" />
                    <vbox min-size=""10, 10"" expand=""true"" />
                </hbox>
            ");
            Box v1 = top.Children[0];
            Box v2 = top.Children[1];
            LayoutManager.Process (top, 100, 100);
            Assert.IsTrue (v1.LayoutPosition.Equals (0, 0));
            Assert.IsTrue (v1.LayoutSize.Equals (50, 100));
            Assert.IsTrue (v2.LayoutPosition.Equals (50, 0));
            Assert.IsTrue (v2.LayoutSize.Equals (50, 100));
        }

        [TestMethod]
        public void ExpandFalse()
        {
            Box top = BoxCreate.FromXml (@"
                <hbox>
                    <vbox min-size=""10, 10"" expand=""false"" />
                    <vbox min-size=""10, 10"" expand=""false"" />
                </hbox>
            ");
            Box v1 = top.Children[0];
            Box v2 = top.Children[1];
            LayoutManager.Process (top, 100, 100);
            Assert.IsTrue (v1.LayoutPosition.Equals (0, 0));
            Assert.IsTrue (v1.LayoutSize.Equals (10, 10));
            Assert.IsTrue (v2.LayoutPosition.Equals (10, 0));
            Assert.IsTrue (v2.LayoutSize.Equals (10, 10));
        }
    }
}