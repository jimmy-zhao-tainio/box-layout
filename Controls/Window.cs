using System.Windows.Forms;

namespace UI.Controls
{
    public class Window
    {
        public Form Form;
        public PictureBox PictureBox;
        public Controls.Box Top;

        public Window(string title, int width, int height)
        {
            Form = new Form
            {
                ClientSize = new System.Drawing.Size(width, height),
                Text = title
            };
            PictureBox = new PictureBox();
            Top = new BoxHorizontal();

            Form.SuspendLayout();
            PictureBox.Location = new System.Drawing.Point(0, 0);
            PictureBox.Margin = new System.Windows.Forms.Padding(0);
            PictureBox.Name = "PictureBox";
            PictureBox.Size = new System.Drawing.Size(width, height);
            PictureBox.TabIndex = 0;
            PictureBox.TabStop = false;
            PictureBox.Tag = this;
            Form.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            Form.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Form.ClientSize = new System.Drawing.Size(width, height);
            Form.Controls.Add(PictureBox);
            Form.Name = title;
            Form.Text = title;
            Form.Tag = this;
            Form.ResumeLayout(false);
        }
    }
}
