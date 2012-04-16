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
    class triangle : glPrimitives
    {
        private static float _size = 1;
        private static float _width = 1;
        private static bool _showVerts = false;
        private static bool _showLines = false;

        private static Color _lineColor = Color.Orange;
        public triangle(Point A, Point B, Point C)
        {
            List<Point> data = new List<Point>();
            data.Add(A);
            data.Add(B);
            data.Add(C);
            //result = new glPrimitives(data, "TRIANGLE");
            this.setData(data, "TRIANGLE");
        }

        public bool showVerts
        {
            get { return _showVerts; }
            set { _showVerts = value; }
        }

        public bool showLines
        {
            get { return _showLines; }
            set { _showLines = value; }
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
        public Color lineColor
        {
            get { return _lineColor; }
            set { _lineColor = value; }
        }
       
    }
}
