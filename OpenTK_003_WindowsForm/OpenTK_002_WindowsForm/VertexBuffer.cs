using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Platform;
using System.Drawing;

namespace OpenTK_002_WindowsForm
{
    // Change this struct to add e.g. color data or anything else you need.
    public struct Vertex
    {
        public Vector3 Position, Normal;
        public Vector2 TexCoord;

        public static readonly int Stride = Marshal.SizeOf(default(Vertex));
    }
 

    sealed public class VertexBuffer
    {
        private int length;

        private int _id;

        int id
        {
            get
            {
                // Create an id on first use.
                if (_id == 0)
                {
                    GraphicsContext.Assert();

                    GL.GenBuffers(1, out _id);
                    if (_id == 0)
                        throw new Exception("Could not create VBO.");
                }
                return _id;
            }
        }

        public VertexBuffer()
        {
        }

        public void SetData(Vertex[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            //System.Console.WriteLine(data.Length);

            this.length = data.Length;
            GL.BindBuffer(BufferTarget.ArrayBuffer, id);
            GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(data.Length * Vertex.Stride), data, BufferUsageHint.StaticDraw);
        }

        public void Render()
        {
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.EnableClientState(ArrayCap.NormalArray);
            GL.EnableClientState(ArrayCap.TextureCoordArray);

            GL.BindBuffer(BufferTarget.ArrayBuffer, id);
            GL.VertexPointer(3, VertexPointerType.Float, Vertex.Stride, new IntPtr(0));
            GL.NormalPointer(NormalPointerType.Float, Vertex.Stride, new IntPtr(Vector3.SizeInBytes));
            GL.TexCoordPointer(2, TexCoordPointerType.Float, Vertex.Stride, new IntPtr(2 * Vector3.SizeInBytes));
            
            //GL.DrawArrays(BeginMode.Triangles, 0, this.length);
            GL.DrawArrays(BeginMode.Quads, 0, this.length);
            GL.Color3(Color.White);
            GL.PointSize(5f);
            GL.DrawArrays(BeginMode.Points, 0, this.length);
        }
    }
}
