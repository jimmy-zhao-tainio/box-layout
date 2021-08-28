using System.Collections.Generic;
using System.Drawing;
using UI.Controls;

namespace Boxes
{
    public interface IExample
    {
        Box GetTop ();
        Dictionary<Box, SolidBrush> GetBrushes ();
    }
}
