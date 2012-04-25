﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Diagnostics;
using System.Threading;

namespace OpenTK_002_WindowsForm
{
    public static class Axis
    {
        //public Axis()
        //{
        //}

        public static void drawXaxis(int dist)
        {
            float width = (float)2.0;
            GL.Color3(Color.Blue);
            GL.LineWidth(width);

            GL.Begin(BeginMode.Lines);

            GL.Vertex3(-dist/2, 0, 0);
            GL.Vertex3(dist/2, 0, 0);

            GL.End();
        }
        public static void drawYaxis(int dist)
        {
            float width = (float)2.0;
            GL.Color3(Color.Red);
            GL.LineWidth(width);

            GL.Begin(BeginMode.Lines);

            GL.Vertex3(0, -dist/2, 0);
            GL.Vertex3(0, dist/2, 0);

            GL.End();
        }
        public static void drawZaxis(int dist)
        {
            float width = (float)2.0;
            GL.Color3(Color.Green);
            GL.LineWidth(width);

            GL.Begin(BeginMode.Lines);

            GL.Vertex3(0, 0, -dist/2);
            GL.Vertex3(0, 0, dist/2);

            GL.End();
        }

        public static void drawOrigin()
        {
            drawXaxis(500);
            drawYaxis(500);
            drawZaxis(500);
            GL.End();
        }

    }
}
