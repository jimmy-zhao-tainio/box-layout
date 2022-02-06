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

namespace WindowsFormsApp
{
    public partial class Form1 : Form
    {
        UI.Loop loop;

        public Form1()
        {
            loop = new UI.Loop(BoxCreate.FromXml(ExampleXml.Buffer));

            Load += Form1_Update;
            Resize += Form1_Update;
            FormClosing += Form1_FormClosing;
            InitializeComponent();

            pictureBox1.MouseMove += PictureBox1_MouseMove;
            pictureBox1.MouseDown += PictureBox1_MouseDown;
            pictureBox1.MouseUp += PictureBox1_MouseUp;
            pictureBox1.MouseCaptureChanged += PictureBox1_MouseCaptureChanged;

            loop.RenderEvent += Loop_RenderEvent;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            loop.Dispose();
        }

        private void Loop_RenderEvent(Bitmap bitmap)
        {
            Invoke((MethodInvoker)delegate {
                pictureBox1.Image?.Dispose();
                pictureBox1.Image = (Bitmap)bitmap.Clone();
                bitmap.Dispose();
            });
        }

        private void Form1_Update(object sender, EventArgs e)
        {
            loop.ChangeSize(pictureBox1.Width, pictureBox1.Height);
        }

        private void PictureBox1_MouseCaptureChanged(object sender, EventArgs e)
        {
            loop.RegisterMouseLose();
        }

        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            loop.RegisterMouseUp(e.X, e.Y);
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            loop.RegisterMouseDown(e.X, e.Y);
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            loop.RegisterMouseMove(e.X, e.Y);
        }
    }
}
