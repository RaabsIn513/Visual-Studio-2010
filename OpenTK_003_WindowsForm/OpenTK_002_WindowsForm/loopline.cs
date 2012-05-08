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
    class loopline : glPrimitives
    {
        private float _vertSize = 1;
        private float _lineWidth = 1;
        private bool _showVerts = false;
        private bool _showLines = false;
        private Color _lineColor = Color.Orange;
        private Color _vertColor = Color.Red;

        public loopline(List<Point> data)
        {
            this.setData(data, "LOOPLINE");
            //new glPrimitives(data, "LOOPLINE");
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
