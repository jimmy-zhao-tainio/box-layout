﻿using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UI.Layout;
using UI.Structures;
using UI.Controls;

namespace Unit
{
    [TestClass]
    public class TestComputeMainLength
    {
        [TestMethod]
        public void WithoutSpace()
        {
            Box h1;
            Box h2;

            h1 = new BoxHorizontal ();
            h2 = new BoxHorizontal ();
            h1.UserMaxSize = Size.New (10, 10, Orientation.Horizontal);
            h2.UserMaxSize = Size.New (10, 10, Orientation.Horizontal);

            h1.Expand.Horizontal = true;
            h2.Expand.Horizontal = true;

            List<Box> list = new List<Box> () { h1, h2 };

            LayoutManager.Process (h1, 0, 0);
            LayoutManager.Process (h2, 0, 0);
            Compute.SetMainLengths(Orientation.Horizontal, list, 20);

            Assert.IsTrue (h1.Computed.MainLength == 10);
            Assert.IsTrue (h2.Computed.MainLength == 10);
        }

        [TestMethod]
        public void WithoutExpand()
        {
            Box h1;
            Box h2;

            h1 = new BoxHorizontal ();
            h2 = new BoxHorizontal ();
            h1.UserMinSize.Width = 10;
            h1.UserMinSize.Height = 10;
            h2.UserMinSize.Width = 10;
            h2.UserMinSize.Height = 10;

            h1.Expand.Horizontal = false;
            h2.Expand.Horizontal = true;

            List<Box> list = new List<Box> () { h1, h2 };

            LayoutManager.Process (h1, 0, 0);
            LayoutManager.Process (h2, 0, 0);
            Compute.SetMainLengths(Orientation.Horizontal, list, 40);
            
            Assert.IsTrue (h1.Computed.MainLength == 10);
            Assert.IsTrue (h2.Computed.MainLength == 30);
        }

        [TestMethod]
        public void FirstReachedMax()
        {
            Box h1;
            Box h2;

            h1 = new BoxHorizontal ();
            h2 = new BoxHorizontal ();
            h1.UserMinSize.Width = 10;
            h1.UserMinSize.Height = 10;
            h2.UserMinSize.Width = 10;
            h2.UserMinSize.Height = 10;

            h1.Expand.Horizontal = true;
            h2.Expand.Horizontal = true;

            h1.UserMaxSize.Width = 10;

            List<Box> list = new List<Box> () { h1, h2 };

            LayoutManager.Process (h1, 0, 0);
            LayoutManager.Process (h2, 0, 0);
            Compute.SetMainLengths(Orientation.Horizontal, list, 21);

            Assert.IsTrue (h1.Computed.MainLength == 10);
            Assert.IsTrue (h2.Computed.MainLength == 11);
        }

        [TestMethod]
        public void LastReachedMax()
        {
            Box h1;
            Box h2;

            h1 = new BoxHorizontal ();
            h2 = new BoxHorizontal ();
            h1.UserMinSize = Size.New (10, 10, Orientation.Horizontal);
            h2.UserMinSize = Size.New (10, 10, Orientation.Horizontal);

            h1.Expand.Horizontal = true;
            h2.Expand.Horizontal = true;

            h2.UserMaxSize.Main = 10;

            List<Box> list = new List<Box> () { h1, h2 };

            LayoutManager.Process (h1, 0, 0);
            LayoutManager.Process (h2, 0, 0);
            Compute.SetMainLengths(Orientation.Horizontal, list, 22);

            Assert.IsTrue (h1.Computed.MainLength == 12);
            Assert.IsTrue (h2.Computed.MainLength == 10);
        }

        [TestMethod]
        public void MiddleReachedMax()
        {
            Box h1;
            Box h2;
            Box h3;

            h1 = new BoxHorizontal ();
            h2 = new BoxHorizontal ();
            h3 = new BoxHorizontal ();
            h1.UserMinSize = Size.New (10, 10, Orientation.Horizontal);
            h2.UserMinSize = Size.New (10, 10, Orientation.Horizontal);
            h3.UserMinSize = Size.New (10, 10, Orientation.Horizontal);

            h1.Expand.Horizontal = true;
            h2.Expand.Horizontal = true;
            h3.Expand.Horizontal = true;

            h2.UserMaxSize.Main = 20;

            List<Box> list = new List<Box> () { h1, h2, h3 };

            LayoutManager.Process (h1, 0, 0);
            LayoutManager.Process (h2, 0, 0);
            LayoutManager.Process (h3, 0, 0);
            Compute.SetMainLengths(Orientation.Horizontal, list, 300);

            Assert.IsTrue (h1.Computed.MainLength == 140);
            Assert.IsTrue (h2.Computed.MainLength == 20);
            Assert.IsTrue (h3.Computed.MainLength == 140);
        }

        [TestMethod]
        public void Benchmark1()
        {
            List<Box> list = new List<Box> ();
            int items = 1000;

            for (int i = 0; i < items; i++)
            {
                Box box = new BoxHorizontal ();
                box.UserMaxSize = Size.New (10, 10, Orientation.Horizontal);
                box.Expand.Horizontal = true;
                if (i % 3 == 0)
                    box.UserMaxSize.Main = 20;
                list.Add (box);
            }

            for (int i = 0; i < items; i++)
                LayoutManager.Process (list[i], 0, 0);
            for (int i = 0; i < items; i++)
                Compute.SetMainLengths (Orientation.Horizontal, list, items * 300);
        }

        [TestMethod]
        public void Benchmark2()
        {
            List<Box> list = new List<Box> ();
            int items = 1000;

            for (int i = 0; i < items; i++)
            {
                Box box = new BoxHorizontal ();
                box.UserMaxSize = Size.New (10, 10, Orientation.Horizontal);
                box.Expand.Horizontal = true;
                if (i % 3 == 0)
                    box.UserMaxSize.Main = 20;
                list.Add (box);
            }

            for (int i = 0; i < items; i++)
                LayoutManager.Process (list[i], 0, 0);
            for (int i = 0; i < items; i++)
                Compute.SetMainLengths (Orientation.Horizontal, list, items * 300);
        }

        [TestMethod]
        public void ThreeIterations()
        {
            Box h1;
            Box h2;
            Box h3;

            h1 = new BoxHorizontal ();
            h2 = new BoxHorizontal ();
            h3 = new BoxHorizontal ();
            h1.UserMaxSize = Size.New (10, 1, Orientation.Horizontal);
            h2.UserMaxSize = Size.New (10, 1, Orientation.Horizontal);
            h3.UserMaxSize = Size.New (10, 1, Orientation.Horizontal);

            h1.Expand.Horizontal = true;
            h2.Expand.Horizontal = true;
            h3.Expand.Horizontal = true;

            // Just picked some numbers to cause 3 iterations
            // Would be fun to have a formula to test with n boxes here.
            h1.UserMaxSize.Main = 20;
            h2.UserMaxSize.Main = 60;
            h3.UserMaxSize.Main = 80;

            List<Box> list = new List<Box> () { h1, h2, h3 };

            LayoutManager.Process (h1, 0, 0);
            LayoutManager.Process (h2, 0, 0);
            LayoutManager.Process (h3, 0, 0);
            Compute.SetMainLengths(Orientation.Horizontal, list, 160);

            Assert.IsTrue (h1.Computed.MainLength == 20);
            Assert.IsTrue (h2.Computed.MainLength == 60);
            Assert.IsTrue (h3.Computed.MainLength == 80);
        }
    }
}