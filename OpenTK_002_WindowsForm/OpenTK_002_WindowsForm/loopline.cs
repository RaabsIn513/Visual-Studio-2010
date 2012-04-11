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
        private static float _width = 1;

        public loopline(List<Point> data)
        {
            this.setData(data, "LOOPLINE");
            //new glPrimitives(data, "LOOPLINE");
        }
        public float width
        {
            get { return _width; }
            set { _width = value; }
        }
    }
}
