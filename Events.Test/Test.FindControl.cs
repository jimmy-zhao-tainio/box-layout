using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UI.Layout;
using UI.Controls;

namespace Unit 
{
    [TestClass]
    public class TestFindControl
    {
        [TestMethod]
        public void FindControl()
        {
            Box top = BoxCreate.FromXml (@"
                <hbox min-size=""100, 100"" />
            ");

            LayoutManager.Process (top, 100, 100);
            Assert.IsTrue(UI.Events.FindControl(0, 0, top) == top);
            Assert.IsTrue(UI.Events.FindControl(99, 99, top) == top);
            Assert.IsTrue(UI.Events.FindControl(100, 100, top) == null);
        }
    
        [TestMethod]
        public void FindControlOverflow()
        {
            Box top = BoxCreate.FromXml (@"
                <hbox min-size=""100, 100"" />
            ");

            LayoutManager.Process (top, 50, 50);
            Assert.IsTrue(UI.Events.FindControl(0, 0, top) == top);
            Assert.IsTrue(UI.Events.FindControl(49, 49, top) == top);
            Assert.IsTrue(UI.Events.FindControl(50, 50, top) == null);
        }

        [TestMethod]
        public void FindControlChildOverflow()
        {
            Box top = BoxCreate.FromXml (@"
                <hbox min-size=""100, 100"">
                    <hbox min-size=""100, 100"" />
                </hbox>
            ");

            LayoutManager.Process (top, 50, 50);
            Assert.IsTrue(UI.Events.FindControl(50, 50, top) == null);
        }
    }
}
