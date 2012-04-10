using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text;
using System.Drawing;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Diagnostics;
using System.Threading;

namespace OpenTK_002_WindowsForm
{
    class triangle : glPrimitives
    {
        //public static glPrimitives result;
        public triangle(Point A, Point B, Point C)
        {
            List<Point> data = new List<Point>();
            data.Add(A);
            data.Add(B);
            data.Add(C);
            //result = new glPrimitives(data, "TRIANGLE");
            this.setData(data, "TRIANGLE");
        }
    }
}
