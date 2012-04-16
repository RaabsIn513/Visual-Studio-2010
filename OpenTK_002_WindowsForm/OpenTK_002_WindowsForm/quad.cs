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
        private static float _size = 1;
        private static float _width = 1;
        private static bool _showVerts = false;
        private static bool _showLines = false;
        private static Color _lineColor = Color.Orange;

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
