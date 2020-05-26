using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Boxing;

namespace Boxes
{
    public class Example
    {
        protected Random random = new Random ();
        protected Dictionary<Box, SolidBrush> brushes = new Dictionary<Box, SolidBrush> ();

        public Dictionary<Box, SolidBrush> GetBrushes ()
        {
            return brushes;
        }

        protected void CreateBrushForBox (Box box)
        {
            if (box.Children.Count == 0)
            {
                int r = random.Next (256 - 50) + 50;
                int g = random.Next (256 - 50) + 50;
                int b = random.Next (256 - 50) + 50;
                brushes.Add (box, new SolidBrush (Color.FromArgb (r, g, b)));
            }
            for (int i = 0; i < box.Children.Count; i++)
                CreateBrushForBox (box.Children[i]);
        }
    }
}
