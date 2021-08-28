using System.Windows.Forms;

namespace UI.Controls
{
    public class Window
    {
        public Form Form;
        public Controls.Box Top;

        public Window(string title, int width, int height)
        {
            Form = new Form
            {
                ClientSize = new System.Drawing.Size(width, height),
                Text = title
            };
            Top = new BoxHorizontal();
        }
    }
}
