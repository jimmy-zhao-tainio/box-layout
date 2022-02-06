using System.Diagnostics;
using UI.Controls;
using UI.Layout;
using System.Drawing;
using System;

namespace UI
{
    public partial class Loop : IDisposable
    {
        private EventQueue Queue;
        private Box Top;

        public delegate void RenderHandler (Bitmap bitmap);
        public event RenderHandler RenderEvent;

        public Loop(Box top)
        {
            Top = top;
            Queue = new EventQueue();
            Queue.ChangeSizeEvent += Queue_ChangeSizeEvent;
            Queue.MouseDownEvent += Queue_MouseDownEvent;
            Queue.MouseLoseEvent += Queue_MouseLoseEvent;
            Queue.MouseMoveEvent += Queue_MouseMoveEvent;
            Queue.MouseUpEvent += Queue_MouseUpEvent;
        }

        public void Dispose()
        {
            Queue.Dispose();
        }

        public void ChangeSize(int x, int y)
        {
            Queue.Enqueue(new ChangeSizeEventArgs(x, y));
        }

        public void RegisterMouseMove(int x, int y)
        {
            Queue.Enqueue(new MouseMoveEventArgs(x, y));
        }

        public void RegisterMouseDown(int x, int y)
        {
            Queue.Enqueue(new MouseDownEventArgs(x, y));
        }

        public void RegisterMouseUp(int x, int y)
        {
            Queue.Enqueue(new MouseUpEventArgs(x, y));
        }

        public void RegisterMouseLose()
        {
            Queue.Enqueue(new MouseLoseEventArgs());
        }

        private void Queue_ChangeSizeEvent(ChangeSizeEventArgs e)
        {
            ProcessAndRender(e.Width, e.Height);
        }

        private void Queue_MouseUpEvent(MouseUpEventArgs e)
        {
            Structures.Point relativePoint = Structures.Point.New(0, 0);

            Box found = Loop.FindControl(e.X, e.Y, Top, ref relativePoint);
            if (found == null)
                return;
        }

        private void Queue_MouseMoveEvent(MouseMoveEventArgs e)
        {
            Structures.Point relativePoint = Structures.Point.New(0, 0);

            Box found = Loop.FindControl(e.X, e.Y, Top, ref relativePoint);

            if (found == null)
            {
                PrintDebug("RegisterMouseMove: null");
                return;
            }

            PrintDebug(string.Format("RegisterMouseMove: {0}, {1}, {2}", found.GetHashCode(), found.LayoutSize.Width, found.LayoutSize.Height));
        }

        private void Queue_MouseLoseEvent(MouseLoseEventArgs e)
        {
        }

        private void Queue_MouseDownEvent(MouseDownEventArgs e)
        {
            Structures.Point relativePoint = Structures.Point.New(0, 0);

            Box found = Loop.FindControl(e.X, e.Y, Top, ref relativePoint);
            if (found == null)
                return;
            if (found.HorizontalScrollbar.AtPoint(relativePoint))
                MouseDownScrollbar(found, found.HorizontalScrollbar, relativePoint);
            else if (found.VerticalScrollbar.AtPoint(relativePoint))
                MouseDownScrollbar(found, found.VerticalScrollbar, relativePoint);
        }

        private void MouseDownScrollbar(Box box, Structures.Scrollbar scrollbar, Structures.Point relativePoint)
        {
            if (scrollbar.HandleAtPoint(relativePoint))
            {
                PrintDebug("RegisterMouseDown: HandleAtPoint");
            }
            else
            {
                int handleCenter = relativePoint.GetMain(scrollbar.Orientation);
                int contentOffset = scrollbar.GetContentOffsetByHandleCenter(handleCenter);
                scrollbar.ContentOffset = contentOffset;
                PrintDebug("RegisterMouseDown: Scrollbar");
                ProcessAndRender();
            }
        }

        private void ProcessAndRender()
        {
            ProcessAndRender(Top.LayoutSize.Width, Top.LayoutSize.Height);
        }

        private void ProcessAndRender(int width, int height)
        {
            LayoutManager.Process (Top, width, height);

            Bitmap bitmap = UI.Render.RenderBox(Top);
            if (bitmap == null)
                return;
            if (RenderEvent != null)
                RenderEvent(bitmap);
        }

        private string debugLine = null;

        private void PrintDebug(string line)
        { 
            if (debugLine == line)
                return;
            debugLine = line;
            Debug.WriteLine(debugLine);
        }
    }
}
