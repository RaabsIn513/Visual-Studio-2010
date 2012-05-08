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
        private float _vertSize = 1;
        private float _lineWidth = 1;
        private bool _showVerts = false;
        private bool _showLines = false;
        private Color _lineColor = Color.Orange;
        private Color _vertColor = Color.Red;

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

        public float vertSize
        {
            get { return _vertSize; }
            set { _vertSize = value; }
        }

        public float lineWidth
        {
            get { return _lineWidth; }
            set { _lineWidth = value; }
        }
        
        public Color lineColor
        {
            get { return _lineColor; }
            set { _lineColor = value; }
        }

        public Color vertColor
        {
            get { return _vertColor; }
            set { _vertColor = value; }
        }
       
    }
}
