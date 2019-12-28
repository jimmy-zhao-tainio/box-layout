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
    public class TestExpandLine
    {
        [TestMethod]
        public void WithoutSpace()
        {
            Box h1;
            Box h2;

            h1 = new BoxHorizontal (Size.New (10, 10, Orientation.Horizontal));
            h2 = new BoxHorizontal (Size.New (10, 10, Orientation.Horizontal));

            h1.Expand.Horizontal = true;
            h2.Expand.Horizontal = true;

            List<Box> list = new List<Box> () { h1, h2 };

            Dictionary<Box, int> lengths = ExpandLine.GetMainLengths(Orientation.Horizontal, list, 20);

            Assert.IsTrue (lengths[h1] == 10);
            Assert.IsTrue (lengths[h2] == 10);
        }

        [TestMethod]
        public void WithoutExpand()
        {
            Box h1;
            Box h2;

            h1 = new BoxHorizontal (Size.New (10, 10, Orientation.Horizontal));
            h2 = new BoxHorizontal (Size.New (10, 10, Orientation.Horizontal));

            h1.Expand.Horizontal = false;
            h2.Expand.Horizontal = true;

            List<Box> list = new List<Box> () { h1, h2 };

            Dictionary<Box, int> lengths = ExpandLine.GetMainLengths(Orientation.Horizontal, list, 40);
            
            Assert.IsTrue (lengths[h1] == 10);
            Assert.IsTrue (lengths[h2] == 30);
        }

        [TestMethod]
        public void FirstReachedMax()
        {
            Box h1;
            Box h2;

            h1 = new BoxHorizontal (Size.New (10, 10, Orientation.Horizontal));
            h2 = new BoxHorizontal (Size.New (10, 10, Orientation.Horizontal));

            h1.Expand.Horizontal = true;
            h2.Expand.Horizontal = true;

            h1.UserMaxSize.Main = 10;

            List<Box> list = new List<Box> () { h1, h2 };

            Dictionary<Box, int> lengths = ExpandLine.GetMainLengths(Orientation.Horizontal, list, 21);

            Assert.IsTrue (lengths[h1] == 10);
            Assert.IsTrue (lengths[h2] == 11);
        }

        [TestMethod]
        public void LastReachedMax()
        {
            Box h1;
            Box h2;

            h1 = new BoxHorizontal (Size.New (10, 10, Orientation.Horizontal));
            h2 = new BoxHorizontal (Size.New (10, 10, Orientation.Horizontal));

            h1.Expand.Horizontal = true;
            h2.Expand.Horizontal = true;

            h2.UserMaxSize.Main = 10;

            List<Box> list = new List<Box> () { h1, h2 };

            Dictionary<Box, int> lengths = ExpandLine.GetMainLengths(Orientation.Horizontal, list, 22);

            Assert.IsTrue (lengths[h1] == 12);
            Assert.IsTrue (lengths[h2] == 10);
        }

        [TestMethod]
        public void MiddleReachedMax()
        {
            Box h1;
            Box h2;
            Box h3;

            h1 = new BoxHorizontal (Size.New (10, 10, Orientation.Horizontal));
            h2 = new BoxHorizontal (Size.New (10, 10, Orientation.Horizontal));
            h3 = new BoxHorizontal (Size.New (10, 10, Orientation.Horizontal));

            h1.Expand.Horizontal = true;
            h2.Expand.Horizontal = true;
            h3.Expand.Horizontal = true;

            h2.UserMaxSize.Main = 20;

            List<Box> list = new List<Box> () { h1, h2, h3 };

            Dictionary<Box, int> lengths = ExpandLine.GetMainLengths(Orientation.Horizontal, list, 300);

            Assert.IsTrue (lengths[h1] == 140);
            Assert.IsTrue (lengths[h2] == 20);
            Assert.IsTrue (lengths[h3] == 140);
        }

        [TestMethod]
        public void ThreeIterations()
        {
            Box h1;
            Box h2;
            Box h3;

            h1 = new BoxHorizontal (Size.New (10, 1, Orientation.Horizontal));
            h2 = new BoxHorizontal (Size.New (10, 1, Orientation.Horizontal));
            h3 = new BoxHorizontal (Size.New (10, 1, Orientation.Horizontal));

            h1.Expand.Horizontal = true;
            h2.Expand.Horizontal = true;
            h3.Expand.Horizontal = true;

            // Just picked some numbers to cause 3 iterations
            // Would be fun to have a formula to test with n boxes here.
            h1.UserMaxSize.Main = 20;
            h2.UserMaxSize.Main = 60;
            h3.UserMaxSize.Main = 80;

            List<Box> list = new List<Box> () { h1, h2, h3 };

            Dictionary<Box, int> lengths = ExpandLine.GetMainLengths(Orientation.Horizontal, list, 160);

            Assert.IsTrue (lengths[h1] == 20);
            Assert.IsTrue (lengths[h2] == 60);
            Assert.IsTrue (lengths[h3] == 80);
        }
    }
}