using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UI.Layout;
using UI.Controls;
using System.Threading;

namespace Unit 
{
    [TestClass]
    public class TestEventQueue
    {
        [TestMethod]
        public void ExitEvent()
        {
            UI.EventQueue queue = new UI.EventQueue(2);
            int maxItems = 10000;
            int handledItems = 0;

            queue.MouseDownEvent += (UI.MouseDownEventArgs e) =>
            {
                Thread.SpinWait(1000);
                handledItems++;
            };
            for (int i = 0; i < maxItems; i++)
            {
                Thread.SpinWait(1000);
                queue.Enqueue(new UI.MouseDownEventArgs(i, i));
            }
            queue.Dispose();
            Assert.IsTrue(handledItems == maxItems);
        }
    }
}
