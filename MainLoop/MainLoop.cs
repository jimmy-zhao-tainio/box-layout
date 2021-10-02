using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.Collections.Generic;
using UI.Layout;
using UI.Controls;
using UI.Structures;
using System;
using System.Diagnostics;

namespace UI
{
    public class MainLoop
    {
        private class Context : ApplicationContext
        {
            private int openWindows = 0;
            private Dictionary<object, Controls.Window> formWindow = new Dictionary<object, Controls.Window>();

            public void Add(Controls.Window window)
            {
                lock (this)
                {
                    formWindow[window.Form] = window;
                    openWindows++;
                }
                window.Form.FormClosed += (s, args) =>
                {
                    lock (this)
                    {
                        formWindow.Remove(s);
                        if (--openWindows == 0)
                            ExitThread();
                    }
                };

                window.Form.SuspendLayout();
                window.Form.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                window.Form.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                window.Form.Load += Form_Load;
                window.Form.Paint += Form_Paint;
                window.Form.Resize += Form_Resize;
                window.Form.MouseMove += Form_MouseMove;
                window.Form.Show();
                window.Form.ResumeLayout();
            }

            private void Form_MouseMove(object sender, MouseEventArgs e)
            {
                Window window = GetWindow(sender);

                Events.RegisterMouseMove(e.X, e.Y, window.Top);
            }

            private void Form_Load(object sender, System.EventArgs e)
            {
                Form_Resize (sender, e);
            }
            
            private void Form_Resize(object sender, System.EventArgs e)
            {
                Window window = GetWindow(sender);

                LayoutManager.Process (window.Top, window.Form.ClientSize.Width, window.Form.ClientSize.Height);
                window.Form.Invalidate ();
            }

            private void Form_Paint(object sender, PaintEventArgs e)
            {
                Controls.Window window = GetWindow(sender);

                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                UI.Render.Box(UI.Structures.Point.New (0, 0, UI.Structures.Orientation.Horizontal), window.Top, e.Graphics);
            }

            private Controls.Window GetWindow(object sender)
            { 
                Controls.Window window;

                lock (this)
                    window = formWindow[sender];
                return window;
            }
        }

        private static readonly Context context = new Context();

        static public void Add(Controls.Window window)
        {
            context.Add(window);
        }

        static public void Run()
        {
            Application.Run(context);
        }
    }
}
