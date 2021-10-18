using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using UI.Controls;
using UI.Structures;

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
            { "AlignMainLeft", new Boxes.AlignMainStart () },
            { "AlignMainCenter", new Boxes.AlignMainCenter() },
            { "AlignMainRight", new Boxes.AlignMainEnd() },
            { "AlignMainSpaceEvenly", new Boxes.AlignMainSpaceEvenly() },
            { "AlignMainSpaceBetween", new Boxes.AlignMainSpaceBetween() },
            { "AlignMainSpaceAround", new Boxes.AlignMainSpaceAround() },
            { "AlignCrossCenter", new Boxes.AlignCrossCenter() },
            { "ExpandMinMaxWrap", new Boxes.ExpandMinMaxWrap() },
            { "ExpandMinMaxWrapSelfCrossAlign", new Boxes.ExpandMinMaxWrapSelfCrossAlign() },
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
            { "ExpandMinMaxWrapScroll", new Boxes.ExpandMinMaxWrapScroll() },
            { "ScrollHorizontalSimple", new Boxes.ScrollHorizontalSimple() },
            { "ScrollHorizontalOffset", new Boxes.ScrollHorizontalOffset() }
        };
        Box top;

        public Form1 ()
        {
            InitializeComponent ();

            listBox1.Items.Clear ();
            foreach (string key in iboxes.Keys)
                listBox1.Items.Add (key);
            listBox1.SelectedIndex = 0;
        }


        private void Form1_Resize (object sender, EventArgs e)
        {
            if (top == null)
                return;
            Render();
        }

        private void listBox1_SelectedIndexChanged (object sender, EventArgs e)
        {
            ibox = iboxes[(string)listBox1.SelectedItem];
            top = ibox.GetTop ();
            Render();
        }
        
        private void Render()
        {
            UI.Layout.LayoutManager.Process (top, this.ClientSize.Width - listBox1.Width, this.ClientSize.Height);
            Bitmap bitmap = UI.Render.RenderBox(top);
            if (bitmap == null)
                return;
            pictureBox.Image?.Dispose();
            pictureBox.Image = (Bitmap)bitmap.Clone();
            bitmap.Dispose();
        }
    }
}
