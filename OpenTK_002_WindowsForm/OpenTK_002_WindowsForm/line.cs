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
        private float _vertSize = 1;
        private float _lineWidth = 1;
        private bool _showVerts = false;
        private Color _vertColor = Color.Red;

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

        public Color vertColor
        {
            get { return _vertColor; }
            set { _vertColor = value; }
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

    }
}
