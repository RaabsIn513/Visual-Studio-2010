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
    class line : glPrimitives
    {
        public line(Point A, Point B)
        {
            List<Point> data = new List<Point>();
            data.Add(A);
            data.Add(B);
            //glPrimitives result = new glPrimitives(data, "LINE");
            this.setData(data, "LINE");
        }
    }
}
