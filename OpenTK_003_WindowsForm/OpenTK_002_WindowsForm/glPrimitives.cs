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
    class glPrimitives
    {
        private  List<Point> _points;
        private string _type = null;
        private int _ID;
        private Color _color = Color.BlanchedAlmond;
        private Color _selectedColor = Color.Fuchsia;
        private bool select = false;

        public glPrimitives()
        {
        }
        
        public glPrimitives(List<Point> points, string type)
        {
            _points = new List<Point>();
            _points = points;
            _type = type;
            _selectedColor = Color.Fuchsia;
        }

        public void setData(List<Point> points, string type)
        {
            _points = new List<Point>();
            _points = points;
            _type = type;
        }

        public bool isSelected
        {
            get { return select;  }
            set
            {
                select = value;
            }
        }

        public bool isLine
        {
            get
            {
                if (this.getPrimitiveType().ToUpper() == "LINE")
                    return true;
                else
                    return false;
            }
        }

        public string getPrimitiveType()
        {
            return _type;
        }

        public int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
            }
        }

        public List<Point> getGeoData()
        {
            return _points;
        }

        public Color propColor
        {
            get
            {
                if (_color == null)
                    _color = Color.Black;
                return _color;
            }
            set
            {
                _color = value;
            }
        }

        public Color selectedColor
        {
            get
            {
                if (_selectedColor == null)
                    _selectedColor = Color.Fuchsia;
                return _selectedColor;
            }
            set
            {
                _selectedColor = value;
            }
        }

        public triangle asTriangle()
        {
            return (triangle)this;
        }

        public line asLine(glPrimitives glPrim)
        {
            return (line)glPrim;
        }

        public point asPoint(glPrimitives glPrim)
        {
            return (point)glPrim;
        }

        public quad asQuad(glPrimitives glPrim)
        {
            return (quad)glPrim;
        }

        public loopline asLoopLine(glPrimitives glPrim)
        {
            return (loopline)glPrim;
        }
    }
}
