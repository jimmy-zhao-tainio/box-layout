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
    public class TestXml
    {
        [TestMethod]
        public void Box()
        {
            try
            {
                Box box = BoxCreate.FromXml(@"<box />");
                Assert.Fail ();
            }
            catch
            {
                Assert.IsTrue (true);
            }
        }

        [TestMethod]
        public void HBox()
        {
            Box box = BoxCreate.FromXml(@"<hbox />");
            Assert.IsTrue (box is BoxHorizontal);
        }

        [TestMethod]
        public void VBox()
        {
            Box box = BoxCreate.FromXml(@"<vbox />");
            Assert.IsTrue (box is BoxVertical);
        }

        [TestMethod]
        public void H1V2H1Box()
        {
            Box box = BoxCreate.FromXml(@"
                <hbox min-size=""1, 1"">
                    <vbox min-size=""2, 1"">
                        <hbox min-size=""3, 1"" />
                    </vbox>
                    <vbox min-size=""2, 2"">
                        <hbox min-size=""3, 2"" />
                    </vbox>
                </hbox>");
            Box v1 = box.Children[0];
            Box v2 = box.Children[1];
            Box v1h1 = v1.Children[0];
            Box v2h1 = v2.Children[0];
            Assert.IsTrue (box is BoxHorizontal);
            Assert.IsTrue (box.UserMinSize.Width == 1);
            Assert.IsTrue (box.UserMinSize.Height == 1);
            Assert.IsTrue (v1 is BoxVertical);
            Assert.IsTrue (v1.UserMinSize.Width == 2);
            Assert.IsTrue (v1.UserMinSize.Height == 1);
            Assert.IsTrue (v2 is BoxVertical);
            Assert.IsTrue (v2.UserMinSize.Width == 2);
            Assert.IsTrue (v2.UserMinSize.Height == 2);
            Assert.IsTrue (v1h1 is BoxHorizontal);
            Assert.IsTrue (v1h1.UserMinSize.Width == 3);
            Assert.IsTrue (v1h1.UserMinSize.Height == 1);
            Assert.IsTrue (v2h1 is BoxHorizontal);
            Assert.IsTrue (v2h1.UserMinSize.Width == 3);
            Assert.IsTrue (v2h1.UserMinSize.Height == 2);
        }
    }
}