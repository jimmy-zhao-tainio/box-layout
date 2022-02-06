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
            UI.Structures.Point relativePoint = UI.Structures.Point.New(0, 0);

            Box top = BoxCreate.FromXml (@"
                <hbox min-size=""100, 100"" />
            ");

            LayoutManager.Process (top, 100, 100);
            Assert.IsTrue(UI.Loop.FindControl(0, 0, top, ref relativePoint) == top);
            Assert.IsTrue(UI.Loop.FindControl(99, 99, top, ref relativePoint) == top);
            Assert.IsTrue(UI.Loop.FindControl(100, 100, top, ref relativePoint) == null);
        }
    
        [TestMethod]
        public void FindControlOverflow()
        {
            UI.Structures.Point relativePoint = UI.Structures.Point.New(0, 0);
            
            Box top = BoxCreate.FromXml (@"
                <hbox min-size=""100, 100"" />
            ");

            LayoutManager.Process (top, 50, 50);
            Assert.IsTrue(UI.Loop.FindControl(0, 0, top, ref relativePoint) == top);
            Assert.IsTrue(UI.Loop.FindControl(49, 49, top, ref relativePoint) == top);
            Assert.IsTrue(UI.Loop.FindControl(50, 50, top, ref relativePoint) == null);
        }

        [TestMethod]
        public void FindControlChildOverflow()
        {
            UI.Structures.Point relativePoint = UI.Structures.Point.New(0, 0);
            
            Box top = BoxCreate.FromXml (@"
                <hbox min-size=""100, 100"">
                    <hbox min-size=""100, 100"" />
                </hbox>
            ");

            LayoutManager.Process (top, 50, 50);
            Assert.IsTrue(UI.Loop.FindControl(50, 50, top, ref relativePoint) == null);
        }
    }
}
