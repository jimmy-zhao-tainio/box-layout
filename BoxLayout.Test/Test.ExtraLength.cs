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
    public class TestExtraLength
    {
        [TestMethod]
        public void None()
        {
            Fill extra = Fill.New (4, 40, 40);

            Assert.IsTrue (extra.Next () == 0);
            Assert.IsTrue (extra.Next () == 0);
            Assert.IsTrue (extra.Next () == 0);
            Assert.IsTrue (extra.Next () == 0);
        }

        [TestMethod]
        public void OnePixelEach()
        {
            Fill extra = Fill.New (4, 40, 44);

            Assert.IsTrue (extra.Next () == 1);
            Assert.IsTrue (extra.Next () == 1);
            Assert.IsTrue (extra.Next () == 1);
            Assert.IsTrue (extra.Next () == 1);
        }

        [TestMethod]
        public void LastWithoutExtra()
        {
            Fill extra = Fill.New (4, 40, 43);

            Assert.IsTrue (extra.Next () == 1);
            Assert.IsTrue (extra.Next () == 1);
            Assert.IsTrue (extra.Next () == 1);
            Assert.IsTrue (extra.Next () == 0);
        }
   }
}