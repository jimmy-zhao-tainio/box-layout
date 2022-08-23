using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Layout;
using UI.Controls;
using UI.Structures;

namespace UI
{
    public partial class Window : Form
    {
        UI.MainLoop loop;

        public Window(UI.Controls.Box box)
        {
            loop = new UI.MainLoop(box);

            Load += Window_Update;
            Resize += Window_Update;
            FormClosing += Window_FormClosing;

            InitializeComponent();

            pictureBox.MouseMove += PictureBox_MouseMove;
            pictureBox.MouseDown += PictureBox_MouseDown;
            pictureBox.MouseUp += PictureBox_MouseUp;
            pictureBox.MouseCaptureChanged += PictureBox_MouseCaptureChanged;

            loop.RenderEvent += Loop_RenderEvent;
        }

        private void Window_FormClosing(object sender, FormClosingEventArgs e)
        {
            loop.Dispose();
        }

        private void Loop_RenderEvent(Bitmap bitmap)
        {
            Invoke((MethodInvoker)delegate {
                pictureBox.Image?.Dispose();
                pictureBox.Image = (Bitmap)bitmap.Clone();
                bitmap.Dispose();
            });
        }

        private void Window_Update(object sender, EventArgs e)
        {
            loop.ChangeSize(pictureBox.Width, pictureBox.Height);
        }

        private void PictureBox_MouseCaptureChanged(object sender, EventArgs e)
        {
            loop.RegisterMouseLose();
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            loop.RegisterMouseUp(e.X, e.Y);
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            loop.RegisterMouseDown(e.X, e.Y);
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            loop.RegisterMouseMove(e.X, e.Y);
        }
    }
}
