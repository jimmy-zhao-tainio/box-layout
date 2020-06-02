using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Boxing;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Boxes.IExample ibox;
        Dictionary<string, Boxes.IExample> iboxes = new Dictionary<string, Boxes.IExample> ()
        {
            { "ExpandEmpty", new Boxes.ExpandEmpty() },
            { "ExpandMain", new Boxes.ExpandMain() },
            { "ExpandCross", new Boxes.ExpandCross() },
            { "ExpandCrossWrap", new Boxes.ExpandCrossWrap() },
            { "ExpandMainMinMax", new Boxes.ExpandMainMinMax() },
            { "EqualSizeExpand", new Boxes.EqualSizeExpand() },
            { "AlignMainLeft", new Boxes.AlignMainStart () },
            { "AlignMainCenter", new Boxes.AlignMainCenter() },
            { "AlignMainRight", new Boxes.AlignMainEnd() },
            { "AlignMainSpaceEvenly", new Boxes.AlignMainSpaceEvenly() },
            { "AlignMainSpaceBetween", new Boxes.AlignMainSpaceBetween() },
            { "AlignMainSpaceAround", new Boxes.AlignMainSpaceAround() },
            { "AlignCrossCenter", new Boxes.AlignCrossCenter() },
            { "ExpandMinMaxWrap", new Boxes.ExpandMinMaxWrap() },
            { "ExpandMinMaxWrapCrossAlign", new Boxes.ExpandMinMaxWrapCrossAlign() },
            { "ExpandMinMaxWrapCrossAlignEnd", new Boxes.ExpandMinMaxWrapCrossAlignEnd() },
            { "ExpandMinMaxWrapCrossAlignEndCenter", new Boxes.ExpandMinMaxWrapCrossAlignEndCenter() },
            { "NoExpandMinMaxWrap", new Boxes.NoExoandMinMaxWrap() },
            { "ExpandCenterHorizontal", new Boxes.ExpandCenterHorizontal() },
            { "ExpandCenterVertical", new Boxes.ExpandCenterVertical() },
            { "WrapMinMaxSizes", new Boxes.WrapMinMaxSizes () },
            { "WrapOnlyMaxSizes", new Boxes.WrapOnlyMaxSizes () },
            { "WrapMinSizesWithExpand", new Boxes.WrapMinSizesWithExpand () },
            { "WrapMinSizesWithoutExpand", new Boxes.WrapMinSizesWithoutExpand () },
            { "WrapAndNoWrap", new Boxes.WrapNoWrap () },
            { "Wrap", new Boxes.Wrap () },
            { "RepeatedBoxes", new Boxes.RepeatedBoxes () },
            { "RandomBoxes", new Boxes.RandomBoxes () },
            { "TypicalSite", new Boxes.TypicalSite () },
        };
        Box top;
        Dictionary<Box, SolidBrush> brushes;

        public Form1 ()
        {
            InitializeComponent ();

            listBox1.Items.Clear ();
            foreach (string key in iboxes.Keys)
                listBox1.Items.Add (key);
            listBox1.SelectedIndex = 0;

            this.Load += Form1_Load;
            this.Paint += Form1_Paint;
        }

        int listBoxWidth = 0;

        private void Form1_Load (object sender, EventArgs e)
        {
            listBoxWidth = listBox1.Width;
            Form1_Resize (null, null);
        }

        private void Form1_Resize (object sender, EventArgs e)
        {
            Boxing.Layout.Run (top, this.ClientSize.Width - listBoxWidth, this.ClientSize.Height);
            this.Invalidate ();
        }

        private void Form1_Paint (object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            DrawBox (Boxing.Point.New (0, 0, Boxing.Orientation.Horizontal), top, e.Graphics);
        }

        private void DrawBox (Boxing.Point position, Box box, Graphics graphics)
        {
            if (box.LayoutSize.Width == 0 || box.LayoutSize.Height == 0)
                return;
            Boxing.Point absolute = Boxing.Point.New (position.X + box.LayoutPosition.X,
                                                      position.Y + box.LayoutPosition.Y,
                                                      Boxing.Orientation.Horizontal);
            if (brushes.TryGetValue (box, out SolidBrush brush))
                graphics.FillRectangle (brush, 
                                        absolute.X + listBoxWidth, 
                                        absolute.Y, 
                                        box.LayoutSize.Width, 
                                        box.LayoutSize.Height);
            for (int i = 0; i < box.Children.Count; i++)
                DrawBox (absolute, box.Children[i], graphics);
        }

        private void listBox1_SelectedIndexChanged (object sender, EventArgs e)
        {
            ibox = iboxes[(string)listBox1.SelectedItem];
            top = ibox.GetTop ();
            brushes = ibox.GetBrushes ();
            Form1_Load (null, null);
        }

    }
}
