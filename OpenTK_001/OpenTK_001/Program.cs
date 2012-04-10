// Released to the public domain. Use, modify and relicense at will.

using System;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Audio;
using OpenTK.Audio.OpenAL;
using OpenTK.Input;
using System.Threading;

namespace OpenTK_001
{
    class Game : GameWindow
    {

        float changeColor = 0.0f;
        float changePos = 0.0f;

        /// <summary>Creates a 800x600 window with the specified title.</summary>
        public Game()
            : base(800, 600, GraphicsMode.Default, "OpenTK Quick Start Sample")
        {
            VSync = VSyncMode.On;
        }

        /// <summary>Load resources here.</summary>
        /// <param name="e">Not used.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(0.1f, 0.1f, 0.1f, 0.0f);
            GL.Enable(EnableCap.DepthTest);
        }

        /// <summary>
        /// Called when your window is resized. Set your viewport here. It is also
        /// a good place to set up your projection matrix (which probably changes
        /// along when the aspect ratio of your window).
        /// </summary>
        /// <param name="e">Not used.</param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);

            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, Width / (float)Height, 1.0f, 64.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);
        }

        /// <summary>
        /// Called when it is time to setup the next frame. Add your game logic here.
        /// </summary>
        /// <param name="e">Contains timing information for framerate independent logic.</param>
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (Keyboard[Key.Escape])
                Exit();

            if (Keyboard[Key.Down])
            {
                changeColor += 0.1f;
            }
            if (Keyboard[Key.Up])
            {
                changeColor -= 0.1f;
            }

            if (Keyboard[Key.Left])
            {
                changePos += 0.05f;
            }
            if (Keyboard[Key.Right])
            {
                changePos -= 0.05f;
            }
        }

        /// <summary>
        /// Called when it is time to render the next frame. Add your rendering code here.
        /// </summary>
        /// <param name="e">Contains timing information.</param>
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            Thread.Sleep(10);           // save on CPU stress (pause for 10ms)
            base.OnRenderFrame(e);
            
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 modelview = Matrix4.LookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelview);

            #region unused Shapes
            #endregion
            
            GL.Begin(BeginMode.Triangles);

            GL.Color3(1.0f, 0.0f, 1.0f); GL.Vertex3(-1.0f + changePos, -1.0f, 4.0f);
            GL.Color3(1.0f, 0.0f, 0.0f); GL.Vertex3(1.0f + changePos, -1.0f, 4.0f);
            GL.Color3(0.2f, 0.9f, 1.0f); GL.Vertex3(0.0f + changePos, 1.0f, 4.0f);

            GL.End();

            
            GL.Begin(BeginMode.Lines);

            GL.Color3(changeColor, 0.0f, 0.0f); GL.Vertex3(0.0f, 0.0f, 4.0f);
            GL.Color3(1.0f, changeColor, 0.0f); GL.Vertex3(0.0f, -1.0f, 4.0f);

            GL.Color3(0.0f, 1.0f, 0.0f); GL.Vertex3(0.0f, 0.0f, 4.0f);
            GL.Color3(0.0f, 1.0f, 0.0f); GL.Vertex3(0.0f, 1.0f, 4.0f);

            GL.End();

            GL.Begin(BeginMode.Points);

            GL.Color3(0.0f, 0.0f, 1.0f);
            
            GL.PointSize(1.9f);
            GL.Vertex3(0.0f, 0.0f, 4.0f);

            GL.End();

            SwapBuffers();
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // The 'using' idiom guarantees proper resource cleanup.
            // We request 30 UpdateFrame events per second, and unlimited
            // RenderFrame events (as fast as the computer can handle).
            using (Game game = new Game())
            {
                game.Run(30.0);
            }
        }
    }
}