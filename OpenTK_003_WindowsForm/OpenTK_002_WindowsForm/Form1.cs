using System;
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
        bool userLine = false;
        bool userLoopLine = false;
        bool userPoly = false;
        bool userPoint = false;
        bool userQuad = false;
        bool userCircle = false;
        List<Point> userPoints = new List<Point>();
        List<Point> userPolyPts = new List<Point>();
        //Point actualGL_pos;
        static float rotateX = 0;
        static float rotateY = 0;
        static float rotateZ = 0;
        static float magnitudeX = 0;
        static float magnitudeY = 0;
        static float magnitudeZ = 0;
        static bool shftKey_state = false;
        static tools userTools;
        byte[] b = new byte[3];
        RandomNumberGenerator rndGen = System.Security.Cryptography.RandomNumberGenerator.Create();
        Matrix4 lookat;
        renderList rl = new renderList();
        VertexBuffer cube;
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
            double milliseconds = ComputeTimeSlice();
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

        private void SetupViewport()
        {
            OpenTK.GLControl c = glControl1;

            float[] mat_specular = { 1.0f, 1.0f, 1.0f, 1.0f };
            float[] mat_shininess = { 50.0f };
            float[] light_position = { 1.0f, 1.0f, 1.0f, 0.0f };
            float[] light_ambient = { 0.5f, 0.5f, 0.5f, 1.0f };

            //GL.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            //GL.ShadeModel(OpenTK.Graphics.OpenGL.ShadingModel.Smooth);

            GL.Material(OpenTK.Graphics.OpenGL.MaterialFace.Front, OpenTK.Graphics.OpenGL.MaterialParameter.Specular, mat_specular);
            GL.Material(OpenTK.Graphics.OpenGL.MaterialFace.Front, OpenTK.Graphics.OpenGL.MaterialParameter.Shininess, mat_shininess);
            GL.Light(OpenTK.Graphics.OpenGL.LightName.Light0, OpenTK.Graphics.OpenGL.LightParameter.Position, light_position);
            GL.Light(OpenTK.Graphics.OpenGL.LightName.Light0, OpenTK.Graphics.OpenGL.LightParameter.Ambient, light_ambient);
            GL.Light(OpenTK.Graphics.OpenGL.LightName.Light0, OpenTK.Graphics.OpenGL.LightParameter.Diffuse, mat_specular);

            //GL.Enable(OpenTK.Graphics.OpenGL.EnableCap.Lighting);
            //GL.Enable(EnableCap.Light0);
            //GL.Enable(EnableCap.DepthTest);
            //GL.Enable(EnableCap.ColorMaterial);
            //GL.Enable(EnableCap.CullFace);           

            if (c.ClientSize.Height == 0)
                c.ClientSize = new System.Drawing.Size(c.ClientSize.Width, 1);

            GL.Viewport(0, 0, c.ClientSize.Width, c.ClientSize.Height);

            float aspect_ratio = Width / (float)Height;
            Matrix4 perpective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspect_ratio, 1, 64);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perpective);

            rl.Add(Block(2, 2, 2));
            rl.Add(Block(2, 2, -2));
            rl.Add(Block(2, -2, 2));
            rl.Add(Block(2, -2,-2));
            rl.Add(Block(-2, 2, 2));
            rl.Add(Block(-2, 2, -2));
            rl.Add(Block(-2, -2, 2));
            rl.Add(Block(-2, -2, -2));
        }

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
            GL.PushAttrib(OpenTK.Graphics.OpenGL.AttribMask.LightingBit);
            GL.PushMatrix();

            

            if (!ds.busyDrawing())
                glControl1.SwapBuffers();            
        }

        private VertexBuffer twoByFour()
        {
            // A 2x4 in inches
            float width = 2.0f;
            float length = 4.0f;
            float height = 96f;

            VertexBuffer result = Block(width, length, height);
            result.Scale(0.125f);
            result.Translate(0, 0, 0);
            return result;
        }

        private VertexBuffer Block(float width, float length, float height)
        {
            GL.Color3(Color.Beige);
            VertexBuffer result = new VertexBuffer();
            Vertex[] data = new Vertex[24];

            data[0].Position = new Vector3(0, 0, 0);
            data[1].Position = new Vector3(0, length, 0);
            data[2].Position = new Vector3(width, length, 0);
            data[3].Position = new Vector3(width, 0, 0);
            data[4].Position = new Vector3(0, 0, 0);
            data[5].Position = new Vector3(width, 0, 0);
            data[6].Position = new Vector3(width, 0, height);
            data[7].Position = new Vector3(0, 0, height);
            data[8].Position = new Vector3(0, 0, 0);
            data[9].Position = new Vector3(0, 0, height);
            data[10].Position = new Vector3(0, length, height);
            data[11].Position = new Vector3(0, length, 0);
            data[12].Position = new Vector3(0, 0, height);
            data[13].Position = new Vector3(width, 0, height);
            data[14].Position = new Vector3(width, length, height);
            data[15].Position = new Vector3(0, length, height);
            data[16].Position = new Vector3(0, length, 0);
            data[17].Position = new Vector3(0, length, height);
            data[18].Position = new Vector3(width, length, height);
            data[19].Position = new Vector3(width, length, 0);
            data[20].Position = new Vector3(width, 0, 0);
            data[21].Position = new Vector3(width, length, 0);
            data[22].Position = new Vector3(width, length, height);
            data[23].Position = new Vector3(width, 0, height);

            result.data = data;
            return result;
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
            ListView.SelectedListViewItemCollection selectedItem = listView1.SelectedItems;
            VertexBuffer vb = new VertexBuffer();
            if (selectedItem.Count == 1)
            {
                string selectedID = selectedItem[0].SubItems[0].Text;
                vb = rl.getByID(Convert.ToInt32(selectedID));
            }
            
            switch (e.KeyCode)
            {
                case Keys.A:
                    if (userTools.CurrentAction == "MOVE")
                        vb.Translate(0.5f, 0, 0);
                    else
                        xWCS += 1;
                    break;
                case Keys.D:
                    if (userTools.CurrentAction == "MOVE")
                        vb.Translate(-0.5f, 0, 0);
                    else
                        xWCS -= 1;
                    break;
                case Keys.S:
                    if (userTools.CurrentAction == "MOVE")
                        vb.Translate(0, 0.5f, 0);
                    else
                        yWCS += 1;
                    break;
                case Keys.W:
                    if (userTools.CurrentAction == "MOVE")
                        vb.Translate(0, -0.5f, 0);
                    else
                        yWCS -= 1;
                    break;
                case Keys.E:
                    if (userTools.CurrentAction == "MOVE")
                        vb.Translate(0, 0, 0.5f);
                    else
                        zWCS += 1;
                    break;
                case Keys.Q:
                    if (userTools.CurrentAction == "MOVE")
                        vb.Translate(0, 0, -0.5f);
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
        
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selectedItem = listView1.SelectedItems;

            if (selectedItem.Count == 1)
            {
                int ID = Convert.ToInt32(selectedItem[0].SubItems[0].Text);
                rl.deleteByID(ID);
            }
            updateListView();
        }

        private void moveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selectedItem = listView1.SelectedItems;
            
            if (selectedItem.Count == 1)
            {
                //// Get the glPrimitive's ID and get the glPrimitive
                //string primID = selectedItem[0].SubItems[0].Text;                       //Get the ID from the ID column
                //glPrimitives selectedObj = ds.getPrimByID(Convert.ToInt32(primID));     //Get the glPrimitive Object from the list
                //Cursor.Position = glControl1.PointToScreen(selectedObj.getGeoData()[0]);//Move the cursor to the first point of the object
                //userTools.deselectAllTools();
                //ds.setMoveProgressObj(selectedObj);
                //ds.useMoveProgress = true;
            }
            MessageBox.Show("Not yet implemented");
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selectedItem = listView1.SelectedItems;

            if (selectedItem.Count == 1)
            {
                //// Get the glPrimitive's ID and get the glPrimitive
                //string primID = selectedItem[0].SubItems[0].Text;                       //Get the ID from the ID column
                //glPrimitives selectedObj = ds.getPrimByID(Convert.ToInt32(primID));     //Get the glPrimitive Object from the list
                //Cursor.Position = glControl1.PointToScreen(selectedObj.getGeoData()[0]);//Move the cursor to the first point of the object
                //userTools.deselectAllTools();
                //glPrimitives copy = new glPrimitives(selectedObj.getGeoData(), selectedObj.getPrimitiveType());
                //ds.setMoveProgressObj(copy);
                //ds.useMoveProgress = true;
            }
            MessageBox.Show("Not yet implemented");
        }
        
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            // The Opening of the context menu strip. 
            // We need to determine what options to give the user
            // eg. Points: color, size options
            //     Lines: color, width, point (show,size,color) etc

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

        public void refreshListView1()
        {
            listView1.Items.Clear();
            listView1.Items.AddRange(ds.viewObjectListLVI().ToArray());
            for (int i = 0; i < listView1.Items.Count; i++)
                listView1.Items[i].BackColor = ds.getByIndex(i).propColor;
        }

        public void updateListView()
        {
            listView1.Items.Clear();
            listView1.Items.AddRange(rl.forListViewCtrl().ToArray());
            for (int i = 0; i < listView1.Items.Count; i++)
                listView1.Items[i].BackColor = rl[i].color;
        }
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

            

            magnitudeX = value;
            tbar_rotateX.Value = value;
            lab_Xrotate.Text = "X Rotation: " + value;
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

        private void btn_buildWall(object sender, EventArgs e)
        {
            int sleepTime = 250;
            VertexBuffer vbA = twoByFour();
            vbA.color = Color.Brown;
            vbA.name = "2x4a";
            rl.Add(vbA);
            Thread.Sleep(sleepTime);
            vbA.Rotate(-(float)Math.PI / 2, 1, 0, 0);
            Thread.Sleep(sleepTime);
            Render();
            vbA.Rotate(-(float)Math.PI / 2, 0, 1, 0);
            Thread.Sleep(sleepTime);
            Render();
            vbA.Rotate(-(float)Math.PI / 2, 0, 0, 1);
            Thread.Sleep(sleepTime);
            Render();
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            rl.deleteAll();
            updateListView();
        }

        private void btn_move_obj_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selectedItem = listView1.SelectedItems;

            if (selectedItem.Count == 1)
            {
                if (userTools.CurrentAction == "MOVE")
                {
                    string selectedID = selectedItem[0].SubItems[0].Text;
                    VertexBuffer vb = rl.getByID(Convert.ToInt32(selectedID));
                    //object vb_asObject = (object)vb;
                    //propertiesDialog dia = new propertiesDialog(vb_asObject);
                    //dia.ShowDialog(this);
                    //VertexBuffer vb2 = (VertexBuffer)dia.getResult();
                    //rl.replaceByID(Convert.ToInt32(selectedID), vb2);
                    //vb.Translate(1.5f, 1.5f, 0);
                    userTools.CurrentAction = null;
                    btn_move_obj.BackColor = Color.Black;
                }
                else
                {
                    userTools.CurrentAction = "MOVE";
                    btn_move_obj.BackColor = Color.White;
                }
                //updateListView();            
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }


    }
}
