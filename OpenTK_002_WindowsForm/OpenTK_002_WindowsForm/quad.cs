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
    class quad : glPrimitives
    {
        public quad(Point A, Point B, Point C)
        {
            //TODO: determine mathmatics for the fouth point
            List<Point> data = new List<Point>();
            data.Add(A);
            data.Add(B);
            data.Add(C);
            //glPrimitives result = new glPrimitives(data, "LINE");
            this.setData(data, "QUAD");
        }
        public quad(Point A, Point B, Point C, Point D)
        {
            List<Point> data = new List<Point>();
            data.Add(A);
            data.Add(B);
            data.Add(C);
            data.Add(D);
            //glPrimitives result = new glPrimitives(data, "LINE");
            this.setData(data, "QUAD");
        }
    }
}
