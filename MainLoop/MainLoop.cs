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

            public void Add(Controls.Window window)
            {
                lock (this)
                {
                    openWindows++;
                }
                window.Form.FormClosed += (s, args) =>
                {
                    lock (this)
                    {
                        if (--openWindows == 0)
                            ExitThread();
                    }
                };

                window.Form.SuspendLayout();
                window.Form.Load += Form_Update;
                window.Form.Resize += Form_Update;
                window.PictureBox.MouseMove += PictureBox_MouseMove;
                window.Form.Show();
                window.Form.ResumeLayout();
                Render(window);
            }

            private void Form_Update(object sender, EventArgs e)
            {
                Window window = (Window)((Form)sender).Tag;
                window.PictureBox.Width = window.Form.ClientSize.Width;
                window.PictureBox.Height = window.Form.ClientSize.Height;
                LayoutManager.Process (window.Top, window.PictureBox.Width, window.PictureBox.Height);
                Render(window);
            }

            private void PictureBox_MouseMove(object sender, MouseEventArgs e)
            {
                Window window = (Window)((PictureBox)sender).Tag;

                Events.RegisterMouseMove(e.X, e.Y, window.Top);
            }

            private void Render(Window window)
            {
                Bitmap bitmap = UI.Render.RenderBox(window.Top);
                if (bitmap == null)
                    return;
                window.PictureBox.Image?.Dispose();
                window.PictureBox.Image = (Bitmap)bitmap.Clone();
                bitmap.Dispose();
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
