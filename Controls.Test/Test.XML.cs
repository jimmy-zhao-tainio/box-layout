using Microsoft.VisualStudio.TestTools.UnitTesting;
using UI.Controls;

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

        [TestMethod]
        public void MaxSize()
        {
            Box box = BoxCreate.FromXml(@"<hbox max-size=""10, 20"" />");
            Assert.IsTrue(box.UserMaxSize.Width == 10);
            Assert.IsTrue(box.UserMaxSize.Height == 20);
        }

        [TestMethod]
        public void WrapFalse()
        {
            Box box = BoxCreate.FromXml(@"<hbox wrap=""false"" />");
            Assert.IsTrue(box.Wrap == false);
        }

        [TestMethod]
        public void WrapTrue()
        {
            Box box = BoxCreate.FromXml(@"<hbox wrap=""true"" />");
            Assert.IsTrue(box.Wrap == true);
        }

        [TestMethod]
        public void WrapTrueUppercase()
        {
            try
            {
                Box box = BoxCreate.FromXml(@"<hbox wrap=""True"" />");
                Assert.Fail ();
            }
            catch
            {
                Assert.IsTrue (true);
            }
        }

        [TestMethod]
        public void ExpandFalse()
        {
            Box box = BoxCreate.FromXml(@"<hbox expand=""false"" />");
            Assert.IsTrue(box.Expand.Main == false);
            Assert.IsTrue(box.Expand.Cross == false);
        }

        [TestMethod]
        public void ExpandTrue()
        {
            Box box = BoxCreate.FromXml(@"<hbox expand=""true"" />");
            Assert.IsTrue(box.Expand.Main == true);
            Assert.IsTrue(box.Expand.Cross == true);
        }

        [TestMethod]
        public void ExpandMainFalse()
        {
            Box box = BoxCreate.FromXml(@"<hbox expand-main=""false"" />");
            Assert.IsTrue(box.Expand.Main == false);
        }

        [TestMethod]
        public void ExpandMainTrue()
        {
            Box box = BoxCreate.FromXml(@"<hbox expand-main=""true"" />");
            Assert.IsTrue(box.Expand.Main == true);
        }

        [TestMethod]
        public void ExpandCrossFalse()
        {
            Box box = BoxCreate.FromXml(@"<hbox expand-cross=""false"" />");
            Assert.IsTrue(box.Expand.Cross == false);
        }

        [TestMethod]
        public void ExpandCrossTrue()
        {
            Box box = BoxCreate.FromXml(@"<hbox expand-cross=""true"" />");
            Assert.IsTrue(box.Expand.Cross == true);
        }

        [TestMethod]
        public void AlignMainStart()
        {
            Box box = BoxCreate.FromXml(@"<hbox align-main=""Start"" />");
            Assert.IsTrue(box.AlignMain == UI.Structures.AlignMain.Start);
        }

        [TestMethod]
        public void AlignMainSpaceAround()
        {
            Box box = BoxCreate.FromXml(@"<hbox align-main=""SpaceAround"" />");
            Assert.IsTrue(box.AlignMain == UI.Structures.AlignMain.SpaceAround);
        }

        [TestMethod]
        public void AlignCrossStart()
        {
            Box box = BoxCreate.FromXml(@"<hbox align-cross=""Start"" />");
            Assert.IsTrue(box.AlignCross == UI.Structures.AlignCross.Start);
        }

        [TestMethod]
        public void AlignCrossEnd()
        {
            Box box = BoxCreate.FromXml(@"<hbox align-cross=""End"" />");
            Assert.IsTrue(box.AlignCross == UI.Structures.AlignCross.End);
        }

        [TestMethod]
        public void LineAlignCrossSpaceBetween()
        {
            Box box = BoxCreate.FromXml(@"<hbox line-align-cross=""SpaceBetween"" />");
            Assert.IsTrue(box.LineAlignCross == UI.Structures.LineAlignCross.SpaceBetween);
        }

        [TestMethod]
        public void SelfAlignCrossCenter()
        {
            Box box = BoxCreate.FromXml(@"<hbox self-align-cross=""Center"" />");
            Assert.IsTrue(box.SelfAlignCross == UI.Structures.SelfAlignCross.Center);
        }

        [TestMethod]
        public void EqualSizeTrue()
        {
            Box box = BoxCreate.FromXml(@"<hbox equal-size=""true"" />");
            Assert.IsTrue(box.EqualSize == UI.Structures.EqualSize.True);
        }

        [TestMethod]
        public void HorizontalScrollbarVisible()
        {
            Box box = BoxCreate.FromXml(@"<hbox horizontal-scrollbar=""Visible"" />");
            Assert.IsTrue(box.HorizontalScrollbar.Mode == UI.Structures.ScrollbarMode.Visible);
        }

        [TestMethod]
        public void VerticalScrollbarVisible()
        {
            Box box = BoxCreate.FromXml(@"<hbox vertical-scrollbar=""Visible"" />");
            Assert.IsTrue(box.VerticalScrollbar.Mode == UI.Structures.ScrollbarMode.Visible);
        }

        [TestMethod]
        public void ScrollOffsetX10()
        {
            Box box = BoxCreate.FromXml(@"<hbox scroll-offset-x=""10"" />");
            Assert.IsTrue(box.HorizontalScrollbar.ContentOffset == 10);
        }

        [TestMethod]
        public void ScrollOffsetY10()
        {
            Box box = BoxCreate.FromXml(@"<hbox scroll-offset-y=""10"" />");
            Assert.IsTrue(box.VerticalScrollbar.ContentOffset == 10);
        }
    }
}