using Microsoft.VisualStudio.TestTools.UnitTesting;
using UI.Layout;
using UI.Controls;

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

            LayoutManager.Process (top, 1000, 1000);
            Box max = top.Children[0];

            Assert.IsTrue (top.LayoutSize.Equals (1000, 1000));
            Assert.IsTrue (top.ContentSize.Equals (10, 10));
            Assert.IsTrue (max.LayoutSize.Equals (10, 10));
            Assert.IsTrue (max.ContentSize.Equals (20, 10));
        }
    }
}