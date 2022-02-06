using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UI
{
    public interface IEventArgs
    { 
    }

    public class ExitEventArgs : IEventArgs
    {
    }

    public class ChangeSizeEventArgs : IEventArgs
    {
        public int Width;
        public int Height;

        public ChangeSizeEventArgs(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }

    public class MouseMoveEventArgs : IEventArgs
    {
        public int X;
        public int Y;

        public MouseMoveEventArgs(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public class MouseDownEventArgs : IEventArgs
    {
        public int X;
        public int Y;

        public MouseDownEventArgs(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public class MouseUpEventArgs : IEventArgs
    {
        public int X;
        public int Y;

        public MouseUpEventArgs(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public class MouseLoseEventArgs : IEventArgs
    {
    }

    public class EventQueue : IDisposable
    {
        private object locker = new object();
        private List<IEventArgs> items;
        private int itemsMax;
        private Thread consumerThread;

        public EventQueue(int maxCapacity = 10000)
        {
            itemsMax = maxCapacity;
            items = new List<IEventArgs>(itemsMax);
            consumerThread = new Thread(Consume);
            consumerThread.Start();
        }

        public delegate void EventArgsHandler<IEventArgs>(IEventArgs e);
        public event EventArgsHandler<ChangeSizeEventArgs> ChangeSizeEvent;
        public event EventArgsHandler<MouseMoveEventArgs> MouseMoveEvent;
        public event EventArgsHandler<MouseDownEventArgs> MouseDownEvent;
        public event EventArgsHandler<MouseUpEventArgs> MouseUpEvent;
        public event EventArgsHandler<MouseLoseEventArgs> MouseLoseEvent;

        private void DispatchEvent(IEventArgs args)
        {
            switch (args)
            {
                case ChangeSizeEventArgs e: ChangeSizeEvent(e); break;
                case MouseMoveEventArgs e: MouseMoveEvent(e); break;
                case MouseDownEventArgs e: MouseDownEvent(e); break;
                case MouseUpEventArgs e: MouseUpEvent(e); break;
                case MouseLoseEventArgs e: MouseLoseEvent(e); break;
            };
        }


        public void Enqueue(IEventArgs item)
        {
            lock (locker)
            {
                while (items.Count >= itemsMax)
                    Monitor.Wait(locker);
                // Maybe not remove all ChangeSizeEventArgs, to get smoother resizing?
                // Keeping all of them is too slow.
                if (item is ChangeSizeEventArgs)
                    items.RemoveAll(e => e is ChangeSizeEventArgs);
                items.Insert(0, item);
                Monitor.PulseAll(locker);
            }
        }

        private void Consume()
        {
            while (true)
            {
                IEventArgs item;
                lock (locker)
                {
                    while (items.Count == 0) 
                        Monitor.Wait(locker);
                    item = items[items.Count - 1];
                    items.RemoveAt(items.Count - 1);
                    Monitor.PulseAll(locker);
                }
                if (item is ExitEventArgs) 
                    return;
                DispatchEvent(item);
            }
        }

        public void Dispose()
        {
            Enqueue(new ExitEventArgs ());
            consumerThread.Join();
        }
    }
}
