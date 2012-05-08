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
        private float _vertSize = 1;
        private float _lineWidth = 1;
        private bool _showVerts = false;
        private bool _showLines = false;
        private Color _lineColor = Color.Orange;
        private Color _vertColor = Color.Red;

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
        public quad(Point A, Point B)
        {
            //TODO: determine mathmatics for the fouth point
            List<Point> data = new List<Point>();
            data.Add(A);
            data.Add(new Point(B.X, A.Y));
            data.Add(B);
            data.Add(new Point(A.X, B.Y));
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
