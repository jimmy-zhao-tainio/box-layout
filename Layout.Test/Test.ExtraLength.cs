using Microsoft.VisualStudio.TestTools.UnitTesting;
using UI.Controls;

namespace Unit
{
    [TestClass]
    public class TestExtraLength
    {
        [TestMethod]
        public void None()
        {
            Spacing extra = Spacing.New (4, 40, 40);

            Assert.IsTrue (extra.Next () == 0);
            Assert.IsTrue (extra.Next () == 0);
            Assert.IsTrue (extra.Next () == 0);
            Assert.IsTrue (extra.Next () == 0);
        }

        [TestMethod]
        public void OnePixelEach()
        {
            Spacing extra = Spacing.New (4, 40, 44);

            Assert.IsTrue (extra.Next () == 1);
            Assert.IsTrue (extra.Next () == 1);
            Assert.IsTrue (extra.Next () == 1);
            Assert.IsTrue (extra.Next () == 1);
        }

        [TestMethod]
        public void LastWithoutExtra()
        {
            Spacing extra = Spacing.New (4, 40, 43);

            Assert.IsTrue (extra.Next () == 1);
            Assert.IsTrue (extra.Next () == 1);
            Assert.IsTrue (extra.Next () == 1);
            Assert.IsTrue (extra.Next () == 0);
        }
   }
}