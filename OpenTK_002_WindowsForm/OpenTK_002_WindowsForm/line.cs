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
        private static float _size = 1;
        private static float _width = 1;
        private static bool _showVerts = false;

        public line(Point A, Point B)
        {
            List<Point> data = new List<Point>();
            data.Add(A);
            data.Add(B);
            //glPrimitives result = new glPrimitives(data, "LINE");
            this.setData(data, "LINE");
        }

        public bool showVerts
        {
            get { return _showVerts; }
            set { _showVerts = value; }
        }

        public float size
        {
            get { return _size; }
            set { _size = value; }
        }

        public float width
        {
            get { return _width; }
            set { _width = value; }
        }
    }
}
