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
    public class glBaseProperties
    {
        private static Color _color = new Color();
        private static string type;

        public Color propColor{
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

    }
}
