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
        private Structures.Scrollbar SelectedScrollbar = null;

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

            SelectedScrollbar = null;

            Box found = Loop.FindControl(e.X, e.Y, Top, ref relativePoint);
            if (found == null)
                return;
        }

        private void Queue_MouseMoveEvent(MouseMoveEventArgs e)
        {
            Structures.Point relativePoint = Structures.Point.New(0, 0);

            if (SelectedScrollbar != null)
            {
                SelectedScrollbar.DragScroll(Structures.Point.New(e.X, e.Y).GetMain(SelectedScrollbar.Orientation));
                ProcessAndRender(true);
                return;
            }

            Box found = Loop.FindControl(e.X, e.Y, Top, ref relativePoint);

            if (found == null)
            {
                PrintDebug("RegisterMouseMove: null");
                return;
            }
        }

        private void Queue_MouseLoseEvent(MouseLoseEventArgs e)
        {
            SelectedScrollbar = null;
        }

        private void Queue_MouseDownEvent(MouseDownEventArgs e)
        {
            Structures.Point absolutePoint = Structures.Point.New(e.X, e.Y);
            Structures.Point relativePoint = Structures.Point.New(0, 0);

            Box found = Loop.FindControl(e.X, e.Y, Top, ref relativePoint);
            if (found == null)
                return;
            if (found.HorizontalScrollbar.AtPoint(relativePoint))
                MouseDownScrollbar(found, found.HorizontalScrollbar, absolutePoint, relativePoint);
            else if (found.VerticalScrollbar.AtPoint(relativePoint))
                MouseDownScrollbar(found, found.VerticalScrollbar, absolutePoint, relativePoint);
        }

        private void MouseDownScrollbar(Box box, Structures.Scrollbar scrollbar, Structures.Point absolutePoint, Structures.Point relativePoint)
        {
            if (scrollbar.HandleAtPoint(relativePoint))
            {
                SelectedScrollbar = scrollbar;
                SelectedScrollbar.DragScrollBegin(absolutePoint.GetMain(scrollbar.Orientation));
            }
            else
            {
                scrollbar.SetContentOffsetByHandleCenter(relativePoint.GetMain(scrollbar.Orientation));
                ProcessAndRender(true);
            }
        }

        private void ProcessAndRender(bool onlyScrolling = false)
        {
            ProcessAndRender(Top.LayoutSize.Width, Top.LayoutSize.Height, onlyScrolling);
        }

        private void ProcessAndRender(int width, int height, bool onlyScrolling = false)
        {
            LayoutManager.Process (Top, width, height, onlyScrolling);

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
