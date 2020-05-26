﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Boxing;

namespace Unit
{
    [TestClass]
    public class TestMainAlign
    {
        [TestMethod]
        public void Start ()
        {
            Box top = BoxCreate.FromXml (@"
                <hbox align-main=""start"">
                    <vbox min-size=""10, 10"" />
                    <vbox min-size=""10, 10"" />
                </hbox>
            ");
            Box v1 = top.Children[0];
            Box v2 = top.Children[1];
            top.Layout (100, 10);
            Assert.IsTrue (v1.LayoutPosition.Equals (0, 0));
            Assert.IsTrue (v1.LayoutSize.Equals (10, 10));
            Assert.IsTrue (v2.LayoutPosition.Equals (10, 0));
            Assert.IsTrue (v2.LayoutSize.Equals (10, 10));
        }

        [TestMethod]
        public void Center ()
        {
            Box top = BoxCreate.FromXml (@"
                <hbox align-main=""center"">
                    <vbox min-size=""10, 10"" />
                    <vbox min-size=""10, 10"" />
                </hbox>
            ");
            Box v1 = top.Children[0];
            Box v2 = top.Children[1];
            top.Layout (100, 10);
            Assert.IsTrue (v1.LayoutPosition.Equals (40, 0));
            Assert.IsTrue (v1.LayoutSize.Equals (10, 10));
            Assert.IsTrue (v2.LayoutPosition.Equals (50, 0));
            Assert.IsTrue (v2.LayoutSize.Equals (10, 10));
        }

        [TestMethod]
        public void End()
        {
            Box top = BoxCreate.FromXml (@"
                <hbox align-main=""end"">
                    <vbox min-size=""10, 10"" />
                    <vbox min-size=""10, 10"" />
                </hbox>
            ");
            Box v1 = top.Children[0];
            Box v2 = top.Children[1];
            top.Layout (100, 10);
            Assert.IsTrue (v1.LayoutPosition.Equals (80, 0));
            Assert.IsTrue (v1.LayoutSize.Equals (10, 10));
            Assert.IsTrue (v2.LayoutPosition.Equals (90, 0));
            Assert.IsTrue (v2.LayoutSize.Equals (10, 10));
        }
    }
}