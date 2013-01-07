using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Diagnostics;
using System.Threading;
using System.Security.Cryptography;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Platform;
using System.Drawing;

namespace OpenTK_002_WindowsForm
{
    public partial class Form1 : Form
    {
        bool loaded = false;
        Stopwatch sw = new System.Diagnostics.Stopwatch();
        objStack ds = new objStack();
        float xWCS = 0;
        float yWCS = 0;
        float zWCS = 0;
        float zoomFactor = 1;
        List<Point> userPoints = new List<Point>();
        List<Point> userPolyPts = new List<Point>();
        static float magnitudeX = 0;
        static float magnitudeY = 0;
        static float magnitudeZ = 0;
        static bool shftKey_state = false;
        static tools userTools;
        byte[] b = new byte[3];
        RandomNumberGenerator rndGen = System.Security.Cryptography.RandomNumberGenerator.Create();
        Matrix4 lookat;
        renderList rl = new renderList();
        int texture;
        Bitmap bitmap = new Bitmap("E:/Visual Studio 2010/OpenTK_003_WindowsForm/OpenTK_002_WindowsForm/texture/wood24.bmp");


        public Form1()
        {
            InitializeComponent();

            bgw_vertexSnap.WorkerReportsProgress = true;

            rndGen.GetBytes(b);
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            loaded = true;

            GL.ClearColor(Color.Black);         // world background color

            float[] light_ambient = { 0.5f, 0.5f, 0.5f, 1.0f };
            float[] light_diffuse = { 1.0f, 1.0f, 1.0f, 1.0f };
            float[] light_position = { 0.0f, 0.0f, 10000.0f, 0.0f };

            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Lequal);

            GL.Light(LightName.Light0, LightParameter.Ambient, light_ambient);
            GL.Light(LightName.Light0, LightParameter.Diffuse, light_diffuse);
            GL.Light(LightName.Light0, LightParameter.Position, light_position);

            GL.Light(LightName.Light0, LightParameter.Position, new float[] { 1.0f, 1.0f, -0.5f });
            GL.Light(LightName.Light0, LightParameter.Ambient, new float[] { 0.3f, 0.3f, 0.3f, 1.0f });
            GL.Light(LightName.Light0, LightParameter.Diffuse, new float[] { 1.0f, 1.0f, 1.0f, 1.0f });
            GL.Light(LightName.Light0, LightParameter.Specular, new float[] { 1.0f, 1.0f, 1.0f, 1.0f });
            GL.Light(LightName.Light0, LightParameter.SpotExponent, new float[] { 1.0f, 1.0f, 1.0f, 1.0f });
            GL.LightModel(LightModelParameter.LightModelAmbient, new float[] { 0.2f, 0.2f, 0.2f, 1.0f });
            GL.LightModel(LightModelParameter.LightModelTwoSide, 1);
            GL.LightModel(LightModelParameter.LightModelLocalViewer, 1);
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);

            GL.Enable(EnableCap.Light0);
            GL.Enable(EnableCap.ColorMaterial);

            GL.Material(MaterialFace.Front, MaterialParameter.Ambient, new float[] { 0.3f, 0.3f, 0.3f, 1.0f });
            GL.Material(MaterialFace.Front, MaterialParameter.Diffuse, new float[] { 1.0f, 1.0f, 1.0f, 1.0f });
            GL.Material(MaterialFace.Front, MaterialParameter.Specular, new float[] { 1.0f, 1.0f, 1.0f, 1.0f });
            GL.Material(MaterialFace.Front, MaterialParameter.Emission, new float[] { 0.0f, 0.0f, 0.0f, 1.0f });

            SetupViewport();
            Application.Idle += Application_Idle; // press TAB twice after +=

            userTools = new tools();
            
        
        }

        void Application_Idle(object sender, EventArgs e)
        {
            double milliseconds = ComputeTimeSlice();       // measuring frames per second to display on UI
            Accumulate(milliseconds);
            Render();
        }

        #region FPS counter
        private double ComputeTimeSlice()
        {
            sw.Stop();
            double timeslice = sw.Elapsed.TotalMilliseconds;
            sw.Reset();
            sw.Start();
            return timeslice;
        }
        double accumulator = 0;
        int idleCounter = 0;
        private void Accumulate(double milliseconds)
        {
            idleCounter++;
            accumulator += milliseconds;
            if (accumulator > 1000)
            {
                label1.Text = idleCounter.ToString();
                accumulator -= 1000;
                idleCounter = 0; // don't forget to reset the counter!
            }
        }
        #endregion
        
        private void glControl1_Resize(object sender, EventArgs e)
        {
            SetupViewport();
            glControl1.Invalidate();
        }

        private void LoadTexture()
        {
            GL.Enable(EnableCap.Texture2D);

            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);

            GL.GenTextures(1, out texture);
            GL.BindTexture(TextureTarget.Texture2D, texture);

            BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            bitmap.UnlockBits(data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

        }

        /// <summary>
        /// Sets up view port and openGL for lighting and textures
        /// </summary>
        private void SetupViewport()
        {
            OpenTK.GLControl c = glControl1;

            float[] mat_specular = { 1.4f, 1.4f, 1.4f, 1.0f };
            float[] mat_shininess = { 25.0f };
            float[] light_position = { 5.0f, 5.0f, 5.0f, 0.0f };
            float[] light_ambient = { 0.5f, 0.5f, 0.5f, 1.0f };

            if( cb_textureONOFF.Checked )
                LoadTexture();
            

            GL.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            GL.ShadeModel(OpenTK.Graphics.OpenGL.ShadingModel.Smooth);

            GL.Material(OpenTK.Graphics.OpenGL.MaterialFace.Front, OpenTK.Graphics.OpenGL.MaterialParameter.Specular, mat_specular);
            GL.Material(OpenTK.Graphics.OpenGL.MaterialFace.Front, OpenTK.Graphics.OpenGL.MaterialParameter.Shininess, mat_shininess);
            GL.Light(OpenTK.Graphics.OpenGL.LightName.Light0, OpenTK.Graphics.OpenGL.LightParameter.Position, light_position);
            GL.Light(OpenTK.Graphics.OpenGL.LightName.Light0, OpenTK.Graphics.OpenGL.LightParameter.Ambient, light_ambient);
            GL.Light(OpenTK.Graphics.OpenGL.LightName.Light0, OpenTK.Graphics.OpenGL.LightParameter.Diffuse, mat_specular);

            GL.Enable(OpenTK.Graphics.OpenGL.EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.ColorMaterial);
            GL.Enable(EnableCap.CullFace);        

            if (c.ClientSize.Height == 0)
                c.ClientSize = new System.Drawing.Size(c.ClientSize.Width, 1);

            GL.Viewport(0, 0, c.ClientSize.Width, c.ClientSize.Height);

            float aspect_ratio = Width / (float)Height;
            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspect_ratio, 1, 128);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);
        }

        /// <summary>
        /// Renders Vertex Buffer Objects contained within renderList rl
        /// </summary>
        private void Render()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.DepthTest);
            GL.ClearColor(Color.DarkBlue);

            Matrix4 lookat = Matrix4.LookAt(15 / zoomFactor, 15 / zoomFactor, 15 / zoomFactor, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);

            GL.Translate(xWCS , yWCS , zWCS);
            GL.Rotate(magnitudeX, 1, 0, 0);
            GL.Rotate(magnitudeY, 0, 1, 0);
            GL.Rotate(magnitudeZ, 0, 0, 1);

            Axis.width = (float)1.5;
            Axis.drawOrigin();

            rl.Render();
            //cube();
            //float[] derp = new float[3];
            //derp[0] = -3;
            //derp[1] = 0;
            //derp[2] = 1;
            ////cylider(derp, 4, 1);
            //Cylider(derp, 4, 1).Render();
            GL.PushAttrib(OpenTK.Graphics.OpenGL.AttribMask.LightingBit);
            GL.PushMatrix();
            
            if (!ds.busyDrawing())
                glControl1.SwapBuffers();            
        }

        /// <summary>
        /// Creates a 2x4x96 block model. 
        /// </summary>
        /// <returns>2x4x96 block at 0,0,0</returns>
        private VertexBuffer twoByFour()
        {
            // A 2x4 in "inches"
            float width = 2.0f;     // 2 in
            float length = 4.0f;    // 4 in
            float height = 96f;     // 8 ft
            VertexBuffer result = Block(width, length, height);
            result.Scale(0.125f);
            result.Translate(0, 0, 0);
            return result;
        }

        /// <summary>
        /// Creates a block using given dimensions
        /// </summary>
        /// <param name="width">Width of the block</param>
        /// <param name="length">Length of the bock</param>
        /// <param name="height">Height of the block</param>
        /// <returns>Block with specified dimensions at 0,0,0</returns>
        private VertexBuffer Block(float width, float length, float height)
        {
            GL.Color3(Color.Beige);
            VertexBuffer result = new VertexBuffer();
            Vertex[] data = new Vertex[24];
            
            //Bottom Face
            data[0].TexCoord = new Vector2(0.0f, 0.0f);
            data[1].TexCoord = new Vector2(0.0f, 1.0f);
            data[2].TexCoord = new Vector2(1.0f, 0.0f);
            data[3].TexCoord = new Vector2(1.0f, 1.0f);
            data[0].Position = new Vector3(0, 0, 0);
            data[1].Position = new Vector3(0, length, 0);
            data[2].Position = new Vector3(width, length, 0);
            data[3].Position = new Vector3(width, 0, 0);
            data[0].Normal = VertexBuffer.getNormalVector(data[0].Position, data[1].Position);
            data[1].Normal = VertexBuffer.getNormalVector(data[1].Position, data[2].Position);
            data[2].Normal = VertexBuffer.getNormalVector(data[2].Position, data[3].Position);
            data[3].Normal = VertexBuffer.getNormalVector(data[3].Position, data[0].Position);
            
            // Left face
            data[4].TexCoord = new Vector2(0.0f, 0.0f);
            data[5].TexCoord = new Vector2(0.0f, 1.0f);
            data[6].TexCoord = new Vector2(1.0f, 0.0f);
            data[7].TexCoord = new Vector2(1.0f, 1.0f);
            data[4].Position = new Vector3(0, 0, 0);
            data[5].Position = new Vector3(width, 0, 0);
            data[6].Position = new Vector3(width, 0, height);
            data[7].Position = new Vector3(0, 0, height);
            data[4].Normal = VertexBuffer.getNormalVector(data[4].Position, data[5].Position);
            data[5].Normal = VertexBuffer.getNormalVector(data[5].Position, data[6].Position);
            data[6].Normal = VertexBuffer.getNormalVector(data[6].Position, data[7].Position);
            data[7].Normal = VertexBuffer.getNormalVector(data[7].Position, data[4].Position);

            // Back face
            data[8].TexCoord = new Vector2(0.0f, 0.0f);
            data[9].TexCoord = new Vector2(0.0f, 1.0f);
            data[10].TexCoord = new Vector2(1.0f, 0.0f);
            data[11].TexCoord = new Vector2(1.0f, 1.0f);
            data[8].Position = new Vector3(0, 0, 0);
            data[9].Position = new Vector3(0, 0, height);
            data[10].Position = new Vector3(0, length, height);
            data[11].Position = new Vector3(0, length, 0);
            data[8].Normal = VertexBuffer.getNormalVector(data[8].Position, data[9].Position);
            data[9].Normal = VertexBuffer.getNormalVector(data[9].Position, data[10].Position);
            data[10].Normal = VertexBuffer.getNormalVector(data[10].Position, data[11].Position);
            data[11].Normal = VertexBuffer.getNormalVector(data[11].Position, data[8].Position);

            // Top face
            data[12].TexCoord = new Vector2(0.0f, 0.0f);
            data[13].TexCoord = new Vector2(0.0f, 1.0f);
            data[14].TexCoord = new Vector2(1.0f, 0.0f);
            data[15].TexCoord = new Vector2(1.0f, 1.0f);
            data[12].Position = new Vector3(0, 0, height);
            data[13].Position = new Vector3(width, 0, height);
            data[14].Position = new Vector3(width, length, height);
            data[15].Position = new Vector3(0, length, height);
            data[12].Normal = VertexBuffer.getNormalVector(data[12].Position, data[13].Position);
            data[13].Normal = VertexBuffer.getNormalVector(data[13].Position, data[14].Position);
            data[14].Normal = VertexBuffer.getNormalVector(data[14].Position, data[15].Position);
            data[15].Normal = VertexBuffer.getNormalVector(data[15].Position, data[12].Position);

            // Right face
            data[16].TexCoord = new Vector2(0.0f, 0.0f);
            data[17].TexCoord = new Vector2(0.0f, 1.0f);
            data[18].TexCoord = new Vector2(1.0f, 0.0f);
            data[19].TexCoord = new Vector2(1.0f, 1.0f);
            data[16].Position = new Vector3(0, length, 0);
            data[17].Position = new Vector3(0, length, height);
            data[18].Position = new Vector3(width, length, height);
            data[19].Position = new Vector3(width, length, 0);
            data[16].Normal = VertexBuffer.getNormalVector(data[16].Position, data[17].Position);
            data[17].Normal = VertexBuffer.getNormalVector(data[17].Position, data[18].Position);
            data[18].Normal = VertexBuffer.getNormalVector(data[18].Position, data[19].Position);
            data[19].Normal = VertexBuffer.getNormalVector(data[19].Position, data[16].Position);

            // Front face
            data[20].TexCoord = new Vector2(0.0f, 0.0f);
            data[21].TexCoord = new Vector2(0.0f, 1.0f);
            data[22].TexCoord = new Vector2(1.0f, 0.0f);
            data[23].TexCoord = new Vector2(1.0f, 1.0f);
            data[20].Position = new Vector3(width, 0, 0);
            data[21].Position = new Vector3(width, length, 0);
            data[22].Position = new Vector3(width, length, height);
            data[23].Position = new Vector3(width, 0, height);
            data[20].Normal = VertexBuffer.getNormalVector(data[20].Position, data[21].Position);
            data[21].Normal = VertexBuffer.getNormalVector(data[21].Position, data[22].Position);
            data[22].Normal = VertexBuffer.getNormalVector(data[22].Position, data[23].Position);
            data[23].Normal = VertexBuffer.getNormalVector(data[23].Position, data[20].Position);

            result.type = "BLOCK";
            result.data = data;
            return result;
        }

        /// <summary>
        /// Creates a Cylinder using given dimensions
        /// </summary>
        /// <param name="height">Height of the cylinder</param>
        /// <param name="radius">Radius of the cylinder</param>
        /// <returns>Cylinder with specified dimensions at 0,0,0</returns>
        private VertexBuffer Cylider( float height, float radius)
        {
            VertexBuffer result = new VertexBuffer();
            List<Vector3> points = new List<Vector3>();
            List<Vertex> data = new List<Vertex>(); // must be even size. 
            int segments = 12;  //Higher numbers improve quality 
            List<Vertex> vertices = new List<Vertex>();     // list to hold verticies
            
            for (double y = 0; y < 2; y++)
            {
                for (double x = 0; x < segments; x++)
                {
                    double theta = (x / (segments - 1)) * 2 * Math.PI;

                    Vertex toAdd = new Vertex();
                    toAdd.Position.X = (float)(radius * Math.Cos(theta) );
                    toAdd.Position.Y = (float)(radius * Math.Sin(theta) );
                    toAdd.Position.Z = (float)(height * y );
                    toAdd.Normal = toAdd.Position;
                    toAdd.Normal.Normalize();
                    vertices.Add(toAdd);
                }
            }

            var indices = new List<int>();
            for (int x = 0; x < segments - 1; x++)
            {
                indices.Add(x);
                indices.Add(x + segments);
                indices.Add(x + segments + 1);

                indices.Add(x + segments + 1);
                indices.Add(x + 1);
                indices.Add(x);
            }

            int i = 0;
            foreach (int index in indices)
            {
                data.Add(vertices[index]);
                i++;
            }
            
            result.data = data.ToArray();
            result.type = "CYLINDER";
            return result;
        }

        private void cube()
        {
            GL.Begin(BeginMode.Quads);
            {
                // front face
                GL.Normal3(0.0, 0.0, 1.0);
                GL.Vertex3(-0.5, -0.5, 0.5);
                GL.Vertex3(0.5, -0.5, 0.5);
                GL.Vertex3(0.5, 0.5, 0.5);
                GL.Vertex3(-0.5, 0.5, 0.5);
 
                // back face
                GL.Normal3(0.0, 0.0, -1.0);
                GL.Vertex3(-0.5, -0.5, -0.5);
                GL.Vertex3(-0.5, 0.5, -0.5);
                GL.Vertex3(0.5, 0.5, -0.5);
                GL.Vertex3(0.5, -0.5, -0.5);
 
                // top face
                GL.Normal3(0.0, 1.0, 0.0);
                GL.Vertex3(-0.5, 0.5, -0.5);
                GL.Vertex3(-0.5, 0.5, 0.5);
                GL.Vertex3(0.5, 0.5, 0.5);
                GL.Vertex3(0.5, 0.5, -0.5);
 
                // bottom face
                GL.Normal3(0.0, -1.0, 0.0);
                GL.Vertex3(-0.5, -0.5, -0.5);
                GL.Vertex3(0.5, -0.5, -0.5);
                GL.Vertex3(0.5, -0.5, 0.5);
                GL.Vertex3(-0.5, -0.5, 0.5);
 
                // right face
                GL.Normal3(1.0, 0.0, 1.0);
                GL.Vertex3(0.5, -0.5, -0.5);
                GL.Vertex3(0.5, 0.5, -0.5);
                GL.Vertex3(0.5, 0.5, 0.5);
                GL.Vertex3(0.5, -0.5, 0.5);
 
                // left face
                GL.Normal3(-1.0, 0.0, 0.0);
                GL.Vertex3(-0.5, -0.5, -0.5);
                GL.Vertex3(-0.5, -0.5, 0.5);
                GL.Vertex3(-0.5, 0.5, 0.5);
                GL.Vertex3(-0.5, 0.5, -0.5);
            }
            GL.End();
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            if (!loaded)
                return;
            Render();
        }

        #region KeyControls

        private void glControl1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    userTools.CurrentAction = null;         // user is done doing w/e action he/she was doing, ASDQWE keys will pan camera
                    rl.place();                             // done moving
                    rl.deselectAll();                       // delect all
                    updateListView();                       // update list
                    break;
                case Keys.A:
                    if (userTools.CurrentAction == "MOVE")
                        rl.movingVBOs_Translate(0.5f, 0f, 0f);
                    else
                        xWCS += 1;
                    break;
                case Keys.D:
                    if (userTools.CurrentAction == "MOVE")
                        rl.movingVBOs_Translate(-0.5f, 0, 0);
                    else
                        xWCS -= 1;
                    break;
                case Keys.S:
                    if (userTools.CurrentAction == "MOVE")
                        rl.movingVBOs_Translate(0, 0.5f, 0);
                    else
                        yWCS += 1;
                    break;
                case Keys.W:
                    if (userTools.CurrentAction == "MOVE")
                        rl.movingVBOs_Translate(0, -0.5f, 0);
                    else
                        yWCS -= 1;
                    break;
                case Keys.E:
                    if (userTools.CurrentAction == "MOVE")
                        rl.movingVBOs_Translate(0, 0, 0.5f);
                    else
                        zWCS += 1;
                    break;
                case Keys.Q:
                    if (userTools.CurrentAction == "MOVE")
                        rl.movingVBOs_Translate(0, 0, -0.5f);
                    else
                        zWCS -= 1;
                    break;
                case Keys.H:
                    userTools.UserPan = true;
                    glControl1.Cursor = Cursors.Hand;
                    break;
                case Keys.R:
                    userTools.UserRotate = true;
                    glControl1.Cursor = Cursors.VSplit;
                    break;
                case Keys.ShiftKey:
                    shftKey_state = true;
                    glControl1.Cursor = Cursors.No;
                    break;
                default:
                    glControl1.Invalidate();
                    glControl1.Cursor = Cursors.Default;
                    break;
            }

        }
        
        private void glControl1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.ShiftKey:
                    shftKey_state = false;
                    break;
                case Keys.LControlKey:
                    shftKey_state = false;
                    break;

                default:
                    shftKey_state = false;
                    break;
            }
        }

        #endregion

        #region MouseData

        Point mouseDownLoc = new Point();
        Point mouseUpLoc = new Point();
        Point prevDownLoc = new Point();
        Point prevUpLoc = new Point();
        
        private void glControl1_MouseDown(object sender, MouseEventArgs e)
        {   
            prevDownLoc = mouseDownLoc;
            //Point actualGL_pos = new Point(e.Location.X - (int)xWCS, e.Location.Y - (int)yWCS);
            mouseDownLoc = e.Location;
            
            switch (userTools.currentTool)
            {
                case "USER_PAN":
                    //glControl1.Cursor = Cursors.GRABHAND; // closed hand?
                    break;

                case "USER_ROTATE":
                    //glControl1.Cursor = Cursors.ROTATING; // ?
                    break;

                default:
                    glControl1.Cursor = Cursors.Cross;
                    break;
            }
            updateListView();
        }
        
        private void glControl1_MouseUp(object sender, MouseEventArgs e)
        {
            prevUpLoc = mouseUpLoc;
            Point actualGL_pos = new Point(e.Location.X - (int)xWCS, e.Location.Y - (int)yWCS);
            mouseUpLoc = actualGL_pos;

            switch (userTools.currentTool)
            {
                case "USER_PAN":
                    xWCS += (mouseUpLoc.X - mouseDownLoc.X) * (float)0.025;
                    yWCS += (mouseUpLoc.Y - mouseDownLoc.Y) * (float)0.025;
                    break;
                case "USER_ROTATE":
                    magnitudeX += (mouseUpLoc.X - mouseDownLoc.X) * (float)0.0025;
                    magnitudeY += (mouseUpLoc.Y - mouseDownLoc.Y) * (float)0.0025;
                    tbar_rotateX.Value = Convert.ToInt32(magnitudeX);
                    tbar_rotateY.Value = Convert.ToInt32(magnitudeY);
                    break;

                default:
                    glControl1.Cursor = Cursors.Cross;
                    break;
            }

            updateListView();
        }

        private void glControl1_MouseMove(object sender, MouseEventArgs e)
        {
            //actualGL_pos = new Point(e.Location.X - (int)xWCS, e.Location.Y - (int)yWCS);
            label3.Text = "X: " + e.Location.X.ToString() + " Y: " + e.Y.ToString();

            if (ds.useMoveProgress)
                Render();
            else if ( userTools.currentTool != null )
            {
                switch (userTools.currentTool)
                {
                    case "USER_PAN":
                        if (e.Button == MouseButtons.Left)
                        {
                            xWCS += (e.Location.X - mouseDownLoc.X) * (float)0.025;
                            yWCS += (e.Location.Y - mouseDownLoc.Y) * (float)0.025;                        
                        }
                        break;
                    case "USER_ROTATE":
                        if (e.Button == MouseButtons.Left)
                        {
                            magnitudeX += (e.Location.X - mouseDownLoc.X) * (float)0.0025;
                            magnitudeY += (e.Location.Y - mouseDownLoc.Y) * (float)0.0025;
                            tbar_rotateX.Value = Convert.ToInt32(magnitudeX);
                            tbar_rotateY.Value = Convert.ToInt32(magnitudeY);
                        }
                        break;
                    default:
                        glControl1.Cursor = Cursors.Cross;
                        break;
                }
            }

        }

        private void glControl1_MouseHover(object sender, EventArgs e)
        {
            //if (!bgw_vertexSnap.IsBusy)
            //{
            //    bgw_vertexSnap.RunWorkerAsync(actualGL_pos);
            //}
        }
        
        /// <summary>
        /// Shift + Scroll to zoom. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glControl1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (shftKey_state)
            {
                if (e.Delta > 0)
                    zoomFactor += (float)0.05;
                if (e.Delta < 0)
                    zoomFactor -= (float)0.05;
            }
            glControl1.Invalidate();
        }

        #endregion
        
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        private double distance(Point A, Point B)
        {
            return Math.Sqrt(Math.Pow(A.X - B.X, 2) + Math.Pow(A.Y - B.Y, 2));
        }

        #region backgroundWorkers
        
        Point prev = new Point();
        
        private void bgw_vertexSnap_DoWork(object sender, DoWorkEventArgs e)
        {
            Point pos = (Point)e.Argument;                  // Assume real gl point
            Point snapTo = new Point();
            List<Point> rawPointData = ds.allPointData();
            double radius = 50;
            if (prev == pos)
            {
                Thread.Sleep(50);
                e.Result = new Point();
            }
            else
            {// Algorithm: 
                List<Point> results = new List<Point>();
                if (rawPointData != null)
                {
                    results = new List<Point>(rawPointData.FindAll(
                        delegate(Point pt)
                        {
                            return (Math.Pow(pos.X - pt.X, 2) + Math.Pow(pos.Y - pt.Y, 2) < Math.Pow(radius, 2));
                        }
                        ));
                }
                if (results.Count() == 1)
                    snapTo = new Point(results[0].X, results[0].Y);
                else if (results.Count() > 1)
                {
                    int i = 0;
                    Point result = new Point();
                    for (; i < results.Count(); i++)
                    {
                        double dist = distance(pos, results[i]);
                        if (dist < radius)
                        {
                            radius = dist;
                            result = results[i];
                        }
                    }
                    snapTo = new Point(result.X, result.Y);
                }
                else
                    snapTo = new Point();         // no points within range, send back nothing...  

                e.Result = snapTo;
            }
            Thread.Sleep(150);
            prev = pos;
        }

        private void bgw_vertexSnap_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Point result = new Point();
            //result = (Point)e.Result;

            //if (result != new Point())       // If there is a result, use it (vertex snap)
            //    if (result != actualGL_pos)
            //        Cursor.Position = glControl1.PointToScreen(result);

            //Thread.Sleep(200);
        }

        #endregion

        #region listViewActions 

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(listView1.PointToScreen( e.Location ));
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListView.SelectedListViewItemCollection selectedItem = listView1.SelectedItems;

            if (selectedItem.Count == 1)
            {
                string selectedID = selectedItem[0].SubItems[0].Text;
                VertexBuffer vb = rl.getByID(Convert.ToInt32(selectedID));
                object vb_asObject = (object)vb;
                propertiesDialog dia = new propertiesDialog(vb_asObject);
                dia.ShowDialog(this);
                VertexBuffer vb2 = (VertexBuffer)dia.getResult();
                rl.replaceByID(Convert.ToInt32(selectedID), vb2);
                updateListView();
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selItems = listView1.SelectedItems;

            for (int i = 0; i < listView1.Items.Count; i++)
            {
                rl.getByID(Convert.ToInt32(listView1.Items[i].Text.ToString())).isSelected = false;
            }

            string showSelItems = null;
            for (int i = 0; i < selItems.Count; i++)
            {
                showSelItems += selItems[i].SubItems[0].Text.ToString() + " : " + selItems[i].SubItems[1].Text.ToString() + "\n";

                rl.getByID(Convert.ToInt32(selItems[i].SubItems[0].Text.ToString())).isSelected = true; // set as selected
            }

        }

        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                ListView.SelectedListViewItemCollection selectedItem = listView1.SelectedItems;

                if (selectedItem.Count >= 1)
                {
                    for (int i = 0; i < selectedItem.Count; i++)
                    {
                        int ID = Convert.ToInt32(selectedItem[i].SubItems[0].Text);
                        rl.deleteByID(ID);
                    }
                }
                updateListView();
            }
            
        }

        public void updateListView()
        {
            listView1.Items.Clear();
            listView1.Items.AddRange(rl.forListViewCtrl().ToArray());
            for (int i = 0; i < listView1.Items.Count; i++)
                listView1.Items[i].BackColor = rl[i].color;
        }

        #region listView Context Menu

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selectedItem = listView1.SelectedItems;

            if (selectedItem.Count >= 1)
            {
                for (int i = 0; i < selectedItem.Count; i++)
                    rl.deleteByID(Convert.ToInt32(selectedItem[i].SubItems[0].Text));
            }
            updateListView();
        }

        private void moveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selectedItem = listView1.SelectedItems;

            if (selectedItem.Count >= 1)
            {
                int[] IDs = new int[selectedItem.Count];
                for (int i = 0; i < IDs.Length; i++)
                    IDs[i] = Convert.ToInt32(selectedItem[i].SubItems[0].Text);
                rl.moveByIDs(IDs);
                userTools.CurrentAction = "MOVE";
                glControl1.Focus();
            }
            //MessageBox.Show("Not yet implemented");
        }

        private void rotateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selectedItem = listView1.SelectedItems;

            if (selectedItem.Count >= 1)
            {
                int[] IDs = new int[selectedItem.Count];
                for (int i = 0; i < IDs.Length; i++)
                    IDs[i] = Convert.ToInt32(selectedItem[i].SubItems[0].Text);
                rl.rotateByIDs(IDs);
                userTools.CurrentAction = "ROTATE";
                glControl1.Focus();
            }
            //MessageBox.Show("Not yet implemented");
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selectedItem = listView1.SelectedItems;

            if (selectedItem.Count >= 1)
            {
                for (int i = 0; i < selectedItem.Count; i++)
                    rl.copyByID(Convert.ToInt32(selectedItem[i].SubItems[0].Text));
            }
            updateListView();
        }
        
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            // The Opening of the context menu strip. 
            // We need to determine what options to give the user
            // eg. Points: color, size options
            //     Lines: color, width, point (show,size,color) etc

        }

        #endregion
        
        #endregion

        #region OnClosing

        protected override void OnClosing(CancelEventArgs e)
        {
            Application.Idle -= Application_Idle;

            base.OnClosing(e);
        }

        #endregion
        
        #region private void DrawCube()

        private void DrawCube()
        {
            GL.Begin(BeginMode.Quads);

            GL.Color3(Color.Orange);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);

            GL.Color3(Color.Honeydew);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);

            GL.Color3(Color.Moccasin);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);

            GL.Color3(Color.IndianRed);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);

            GL.Color3(Color.PaleVioletRed);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);

            GL.Color3(Color.ForestGreen);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);

            GL.End();            
        }

        #endregion

        #region track bar controls

        private void tbar_rotateX_ValueChanged(object sender, EventArgs e)
        {
            int value = tbar_rotateX.Value;
            int angleSnap = 5;
            if ((Math.Abs(value) < angleSnap) && (Math.Abs(value) > -angleSnap))
                value = 0;
            if ((Math.Abs(value) < (45 + angleSnap)) && (Math.Abs(value) > (45 - angleSnap)))
            {
                if (value < 0) // if negative
                    value = -45;
                else
                    value = 45;
            }
            if ((Math.Abs(value) < (90 + angleSnap)) && (Math.Abs(value) > (90 - angleSnap)))
            {
                if (value < 0) // if negative
                    value = -90;
                else
                    value = 90;
            }
            if ((Math.Abs(value) < (135 + angleSnap)) && (Math.Abs(value) > (135 - angleSnap)))
            {
                if (value < 0) // if negative
                    value = -135;
                else
                    value = 135;
            }
            if ((Math.Abs(value) < (180 + angleSnap)) && (Math.Abs(value) > (180 - angleSnap)))
            {
                if (value < 0) // if negative
                    value = -180;
                else
                    value = 180;
            }

            if (userTools.CurrentAction == "ROTATE")
            {
                rl.rotatingVBOs_Rotate(value, 1, 0, 0);
                //rl.place();
                //rl.Render();
            }
            else
            {
                magnitudeX = value;
                tbar_rotateX.Value = value;
                lab_Xrotate.Text = "X Rotation: " + value;
            }
        }

        private void tbar_rotateY_ValueChanged(object sender, EventArgs e)
        {
            int value = tbar_rotateY.Value;
            magnitudeY = value;

            int angleSnap = 5;
            if ((Math.Abs(value) < angleSnap) && (Math.Abs(value) > -angleSnap))
                value = 0;
            if ((Math.Abs(value) < (45 + angleSnap)) && (Math.Abs(value) > (45 - angleSnap)))
            {
                if (value < 0) // if negative
                    value = -45;
                else
                    value = 45;
            }
            if ((Math.Abs(value) < (90 + angleSnap)) && (Math.Abs(value) > (90 - angleSnap)))
            {
                if (value < 0) // if negative
                    value = -90;
                else
                    value = 90;
            }
            if ((Math.Abs(value) < (135 + angleSnap)) && (Math.Abs(value) > (135 - angleSnap)))
            {
                if (value < 0) // if negative
                    value = -135;
                else
                    value = 135;
            }
            if ((Math.Abs(value) < (180 + angleSnap)) && (Math.Abs(value) > (180 - angleSnap)))
            {
                if (value < 0) // if negative
                    value = -180;
                else
                    value = 180;
            }

            magnitudeY = value;
            tbar_rotateY.Value = value;
            lab_Yrotate.Text = "Y Rotation: " + value;
        }

        private void tbar_rotateZ_ValueChanged(object sender, EventArgs e)
        {
            int value = tbar_rotateZ.Value;
            magnitudeZ = value;

            int angleSnap = 5;
            if ((Math.Abs(value) < angleSnap) && (Math.Abs(value) > -angleSnap))
                value = 0;
            if ((Math.Abs(value) < (45 + angleSnap)) && (Math.Abs(value) > (45 - angleSnap)))
            {
                if (value < 0) // if negative
                    value = -45;
                else
                    value = 45;
            }
            if ((Math.Abs(value) < (90 + angleSnap)) && (Math.Abs(value) > (90 - angleSnap)))
            {
                if (value < 0) // if negative
                    value = -90;
                else
                    value = 90;
            }
            if ((Math.Abs(value) < (135 + angleSnap)) && (Math.Abs(value) > (135 - angleSnap)))
            {
                if (value < 0) // if negative
                    value = -135;
                else
                    value = 135;
            }
            if ((Math.Abs(value) < (180 + angleSnap)) && (Math.Abs(value) > (180 - angleSnap)))
            {
                if (value < 0) // if negative
                    value = -180;
                else
                    value = 180;
            }

            magnitudeZ = value;
            tbar_rotateZ.Value = value;
            lab_Zrotate.Text = "Z Rotation: " + value;
        }

        #endregion

        private void btn_HOME_coord_Click(object sender, EventArgs e)
        {
            magnitudeX = 0;
            magnitudeY = 0;
            magnitudeZ = 0;
            tbar_rotateX.Value = 0;
            tbar_rotateY.Value = 0;
            tbar_rotateZ.Value = 0;
        }

        /// <summary>
        /// Builds chair from midterm test using block and cylinder objects.
        /// Change sleepTime variable to change speed of build
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_buildChair_Click(object sender, EventArgs e)
        {
            int sleepTime = 25;     // so we can show the steps of each 2x4 that is created, rotated and translated
            float scale = 0.125f;
            VertexBuffer leg1 = Cylider( 32 * scale, 1.0f * scale);
            leg1.color = Color.AliceBlue;
            leg1.name = "LEG1";
            rl.Add(leg1);
            Thread.Sleep(sleepTime);
            Render();
            leg1.Translate(0, 0, 0);
            Thread.Sleep(sleepTime);
            Render();
            leg1.Rotate(-(float)Math.PI / 2, 1, 0, 0);   // rotate

            VertexBuffer leg2 = Cylider( 32 * scale, 1.0f * scale);
            leg2.color = Color.Aqua;
            leg2.name = "LEG2";
            rl.Add(leg2);
            Thread.Sleep(sleepTime);
            Render();
            leg2.Translate(0, 26 * scale , 0);
            Thread.Sleep(sleepTime);
            Render();
            leg2.Rotate(-(float)Math.PI / 2, 1, 0, 0);   // rotate

            VertexBuffer leg3 = Cylider( 32 * scale, 1.0f * scale);
            leg3.color = Color.Aquamarine;
            leg3.name = "LEG3";
            rl.Add(leg3);
            Thread.Sleep(sleepTime);
            Render();
            leg3.Translate(28 * scale , 26 * scale , 0);
            Thread.Sleep(sleepTime);
            Render();
            leg3.Rotate(-(float)Math.PI / 2, 1, 0, 0);   // rotate

            VertexBuffer leg4 = Cylider( 32 * scale, 1.0f * scale);
            leg4.color = Color.MediumAquamarine;
            leg4.name = "LEG4";
            rl.Add(leg4);
            Thread.Sleep(sleepTime);
            Render();
            leg4.Translate(28 * scale, 0, 0);
            Thread.Sleep(sleepTime);
            Render();
            leg4.Rotate(-(float)Math.PI / 2, 1, 0, 0);   // rotate

            VertexBuffer seat = Block(30 * scale, 28 * scale, 2 * scale);
            seat.color = Color.BlueViolet;
            seat.name = "SEAT";
            rl.Add(seat);
            Thread.Sleep(sleepTime);
            Render();
            seat.Translate(-1 * scale, -1 * scale, 32 * scale);
            Thread.Sleep(sleepTime);
            Render();
            seat.Rotate(-(float)Math.PI / 2, 1, 0, 0);   // rotate

            VertexBuffer back = Block(25 * scale, 28 * scale, 2 * scale);
            back.color = Color.Violet;
            back.name = "BACK";
            rl.Add(back);
            Thread.Sleep(sleepTime);
            Render();
            back.Rotate(-(float)Math.PI/2, 1, 0, 0);   // rotate
            Thread.Sleep(sleepTime);
            Render();
            back.Rotate((float)Math.PI / 2, 0, 0, 1);   // rotate
            Thread.Sleep(sleepTime);
            Render();
            back.Translate(1 * scale, 34 * scale, 1 * scale);
            Thread.Sleep(sleepTime);
            Render();
            back.Translate(-1 * scale, -34 * scale, -1 * scale);// move to origin to rotate it
            back.Rotate(0.125f, 0, 0, 1);                       // rotate
            back.Translate(1 * scale, 34 * scale, 1 * scale);   // then move back to original positon
            Thread.Sleep(40);
            Render();
            back.Translate(-1 * scale, -34 * scale, -1 * scale);// move to origin to rotate it
            back.Rotate(0.125f, 0, 0, 1);                       // rotate
            back.Translate(1 * scale, 34 * scale, 1 * scale);   // then move back to original positon
            Thread.Sleep(40);
            Render();
            back.Translate(-1 * scale, -34 * scale, -1 * scale);// move to origin to rotate it
            back.Rotate(0.125f, 0, 0, 1);                       // rotate
            back.Translate(1 * scale, 34 * scale, 1 * scale);   // then move back to original positon
            Thread.Sleep(40);
            Render();

            updateListView();
        }

        /// <summary>
        /// Builds the wall from homework 3 using 2x4 models derived from
        /// block object. 
        /// Change sleepTime to change speed of the build.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_buildWall(object sender, EventArgs e)
        {
            int sleepTime = 25;     // so we can show the steps of each 2x4 that is created, rotated and translated
            VertexBuffer vbA = twoByFour();             // gets a cube that has been shaped and scaled to 2x4 dimensions
            vbA.color = Color.Brown;                    // give it a unique color
            vbA.name = "2x4a";                          // give it a unique name
            rl.Add(vbA);                                // add this VBO to our render list
            Thread.Sleep(sleepTime);                    // pause to show the process
            vbA.Rotate(-(float)Math.PI / 2, 1, 0, 0);   // rotate
            Thread.Sleep(sleepTime);
            Render();                                   // show the process
            vbA.Rotate(-(float)Math.PI / 2, 0, 1, 0);   // rotate
            Thread.Sleep(sleepTime);
            Render();                                   // show the process
            vbA.Rotate(-(float)Math.PI / 2, 0, 0, 1);   // rotate
            Thread.Sleep(sleepTime);            
            Render();                                   // show the process
            vbA.Translate(0, 0, 0);
            

            VertexBuffer vbC = twoByFour();
            vbC.color = Color.Yellow;
            vbC.name = "2x4c";
            rl.Add(vbC);
            vbC.Rotate(0, 1, 0, 0);
            vbC.Rotate(0, 0, 1, 0);
            vbC.Rotate(0, 0, 0, 1);
            Thread.Sleep(sleepTime);
            Render();
            vbC.Translate(0, -(float)(4 * 0.125), (float)(2 * 0.125));
            

            VertexBuffer vbD = twoByFour();
            vbD.color = Color.LemonChiffon;
            vbD.name = "2x4d";
            rl.Add(vbD);
            vbD.Rotate(0, 1, 0, 0);
            vbD.Rotate(0, 0, 1, 0);
            vbD.Rotate(0, 0, 0, 1);
            Thread.Sleep(sleepTime);
            Render();
            vbD.Translate((float)(16 * 0.125), -(float)(4 * 0.125), (float)(2 * 0.125));
            

            VertexBuffer vbE = twoByFour();
            vbE.color = Color.LawnGreen;
            vbE.name = "2x4e";
            rl.Add(vbE);
            vbE.Rotate(0, 1, 0, 0);
            vbE.Rotate(0, 0, 1, 0);
            vbE.Rotate(0, 0, 0, 1);
            Thread.Sleep(sleepTime);
            Render();
            vbE.Translate((float)(32 * 0.125), -(float)(4 * 0.125), (float)(2 * 0.125));
            

            VertexBuffer vbF = twoByFour();
            vbF.color = Color.Khaki;
            vbF.name = "2x4f";
            rl.Add(vbF);
            vbF.Rotate(0, 1, 0, 0);
            vbF.Rotate(0, 0, 1, 0);
            vbF.Rotate(0, 0, 0, 1);
            Thread.Sleep(sleepTime);
            Render();
            vbF.Translate((float)(48 * 0.125), -(float)(4 * 0.125), (float)(2 * 0.125));
            

            VertexBuffer vbG = twoByFour();
            vbG.color = Color.Indigo;
            vbG.name = "2x4g";
            rl.Add(vbG);
            vbG.Rotate(0, 1, 0, 0);
            vbG.Rotate(0, 0, 1, 0);
            vbG.Rotate(0, 0, 0, 1);
            Thread.Sleep(sleepTime);
            Render();
            vbG.Translate((float)(64 * 0.125), -(float)(4 * 0.125), (float)(2 * 0.125));
            

            VertexBuffer vbH = twoByFour();
            vbH.color = Color.Honeydew;
            vbH.name = "2x4h";
            rl.Add(vbH);
            vbH.Rotate(0, 1, 0, 0);
            vbH.Rotate(0, 0, 1, 0);
            vbH.Rotate(0, 0, 0, 1);
            Thread.Sleep(sleepTime);
            Render();
            vbH.Translate((float)(80 * 0.125), -(float)(4 * 0.125), (float)(2 * 0.125));
            

            VertexBuffer vbI = twoByFour();
            vbI.color = Color.Gray;
            vbI.name = "2x4i";
            rl.Add(vbI);
            vbI.Rotate(0, 1, 0, 0);
            vbI.Rotate(0, 0, 1, 0);
            vbI.Rotate(0, 0, 0, 1);
            Thread.Sleep(sleepTime);
            Render();
            vbI.Translate((float)(94 * 0.125), -(float)(4 * 0.125), (float)(2 * 0.125));
            

            VertexBuffer vbJ = twoByFour();
            vbJ.color = Color.Tan;
            vbJ.name = "2x4j";
            rl.Add(vbJ);
            Thread.Sleep(sleepTime);
            vbJ.Rotate(-(float)Math.PI / 2, 1, 0, 0);
            Thread.Sleep(sleepTime);
            Render();
            vbJ.Rotate(-(float)Math.PI / 2, 0, 1, 0);
            Thread.Sleep(sleepTime);
            Render();
            vbJ.Rotate(-(float)Math.PI / 2, 0, 0, 1);
            Thread.Sleep(sleepTime);
            Render();
            vbJ.Translate(0, 0, (float)(98*0.125));

            updateListView();
        }

        /// <summary>
        /// Delete all items in the renderList rl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            rl.deleteAll();
            updateListView();
        }

        /// <summary>
        /// When user interface focus leaves glControl1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glControl1_Leave(object sender, EventArgs e)
        {
            if (userTools.CurrentAction == "MOVE")
            {
                userTools.CurrentAction = null;         // user is done doing w/e action he/she was doing, ASDQWE keys will pan camera
                rl.place();                             // done moving
                rl.deselectAll();                       // delect all
            }
            updateListView();                       // update list
        }

        private void cb_textureONOFF_CheckedChanged(object sender, EventArgs e)
        {
            SetupViewport();
        }

        #region menuStrip
        private void exportXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xmlExporter.export(rl);
        }
        #endregion
    }
}
