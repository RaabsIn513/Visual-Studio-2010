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
    class point : glPrimitives
    {
        public point(Point A)
        {
            List<Point> data = new List<Point>();
            data.Add(A);
            this.setData(data, "POINT");
        }
        public point(Point A, float size)
        {
            List<Point> data = new List<Point>();
            data.Add(A);
            this.glSize = size;
            this.setData(data, "POINT");
        }
        
    }
}
