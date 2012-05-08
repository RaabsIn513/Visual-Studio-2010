using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Diagnostics;
using System.Threading;


namespace OpenTK_002_WindowsForm
{
    class polygon : glPrimitives
    {
        public polygon(List<Point> data)
        {
            this.setData(data, "POLYGON");
        }
    }
}
