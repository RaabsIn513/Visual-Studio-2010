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
        //public Color color;

        public static readonly int Stride = Marshal.SizeOf(default(Vertex));
    }
 

    sealed public class VertexBuffer
    {
        private int length;
        private Vertex[] _data;
        private string _name;
        private int _id;
        private Color _color = new Color();
        private Color _selectedColor = Color.Fuchsia;
        private bool _isSelected = false;

        public int id
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
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }
        public Color color
        {
            get { return _color; }
            set { _color = value; }
        }
        public Color selectedColor
        {
            get { return _selectedColor; }
            set { _selectedColor = value; }
        }
        public bool isSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; }
        }

        public VertexBuffer()
        {
            _name = "vbo_name_not_set";
            _color = Color.CadetBlue;
        }

        public Vertex[] data
        {
            get { return _data; }
            set 
            {
                _data = value;
                if (_data == null)
                {
                    throw new ArgumentNullException("data");
                }

                this.length = _data.Length;
                GL.BindBuffer(BufferTarget.ArrayBuffer, id);
                GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(_data.Length * Vertex.Stride), _data, BufferUsageHint.StaticDraw);
            }
        }

        public void Scale(float scaleFactor)
        {
            if (scaleFactor != 0)
            {
                for (int i = 0; i < _data.Length; i++)
                    _data[i].Position = new Vector3(_data[i].Position.X * scaleFactor, _data[i].Position.Y * scaleFactor,
                        _data[i].Position.Z * scaleFactor);

                this.data = _data;
            }
            else
                throw new ArgumentException("Scale Factor must not be zero!");
        }

        public void Translate(float xDist, float yDist, float zDist)
        {
            for (int i = 0; i < _data.Length; i++)
            {
                _data[i].Position.X += xDist;
                _data[i].Position.Y += yDist;
                _data[i].Position.Z += zDist;
            }
            this.data = _data;
        }

        public void Rotate(float angle, float x, float y, float z)
        {
            for (int i = 0; i < _data.Length; i++)
                _data[i].Position = Vector3.Transform(_data[i].Position, Matrix4.Rotate(new Vector3(x, y, z), angle));
            this.data = _data;
        }

        public static Vector3 getNormalVector(Vector3 A, Vector3 B)
        {
            //Using Sarrus' Rule
            A.Normalize();
            B.Normalize();
            float xComp = (A.Y * B.Z) - (A.Z * B.Y);
            float yComp = (A.Z * B.X) - (A.X * B.Z);
            float zComp = (A.X * B.Y) - (A.Y * B.X);

            return new Vector3(xComp, yComp, zComp);
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
            if (_isSelected)
                GL.Color3(_selectedColor);
            else
                GL.Color3(_color);

            GL.DrawArrays(BeginMode.Quads, 0, this.length);
            GL.Color3(Color.White);
            GL.PointSize(5f);
            GL.DrawArrays(BeginMode.Points, 0, this.length);
        }
    }
}
