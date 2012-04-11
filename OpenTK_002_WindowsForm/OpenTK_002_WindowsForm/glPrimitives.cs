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
        private Color _color = Color.Black;
        private Color _selectedColor = Color.Fuchsia;
        private bool select = false;
        private bool _showVerts = false;

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

        public glPrimitives idk()
        {
            quad lol = new quad(new Point(0, 0), new Point(100, 100));

            return lol;
        }

        public bool isSelected
        {
            get { return select;  }
            set
            {
                select = value;
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
    }
}
