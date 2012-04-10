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

namespace OpenTK_002_WindowsForm
{
    public partial class Form1 : Form
    {
        bool loaded = false;
        Stopwatch sw = new System.Diagnostics.Stopwatch();
        objStack ds = new objStack();
        float xWCS = 0;
        float yWCS = 0;
        bool userLine = false;
        bool userPoly = false;
        bool userPoint = false;
        bool userQuad = false;
        List<Point> userPolyPts = new List<Point>();

        public Form1()
        {
            InitializeComponent();
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            loaded = true;

            GL.ClearColor(Color.SlateGray);         // world background color


            SetupViewport();
            Application.Idle += Application_Idle; // press TAB twice after +=
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
            int w = glControl1.Width;
            int h = glControl1.Height;
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, w, h, 0, -1, 1); // Upper-left corner pixel has coordinate (0, 0)
            GL.Viewport(0, 0, w, h);     // Use all of the glControl painting area
        }

        private void Render()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.Translate(xWCS, yWCS, 0);

            //ds.drawPrimList();
            ds.drawPrimList();

            if (!ds.busyDrawing())
                glControl1.SwapBuffers();
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            if (!loaded)
                return;
            Render();
        }

        private void glControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                xWCS += 1;
                glControl1.Invalidate();
            }
            if (e.KeyCode == Keys.D)
            {
                xWCS -= 1;
                glControl1.Invalidate();
            }
            if (e.KeyCode == Keys.S)
            {
                yWCS += 1;
                glControl1.Invalidate();
            }
            if (e.KeyCode == Keys.W)
            {
                yWCS -= 1;
                glControl1.Invalidate();
            }
        }

        private void drawLine(float X1, float Y1, float X2, float Y2)
        {
            GL.Begin(BeginMode.Lines);

            GL.Vertex3(X1, Y1, 0.0f);
            GL.Vertex3(X2, Y2, 0.0f);

            GL.End();

            glControl1.SwapBuffers();
        }

        #region MouseData
 
        Point mouseDownLoc = new Point();
        Point mouseUpLoc = new Point();
        Point prevDownLoc = new Point();
        Point prevUpLoc = new Point();
        private void glControl1_MouseDown(object sender, MouseEventArgs e)
        {   
            prevDownLoc = mouseDownLoc;
            mouseDownLoc = e.Location;

            if (userPoint)
            {
                point temp = new point(mouseDownLoc);
                temp.glSize = 5;
                ds.Add(temp);
                ds.useDrawProgress = true;  // continue showing the point as the mouse moves on screen
            }
            if (userQuad && (mouseUpLoc != new Point()) && (mouseDownLoc != new Point())) // down, up, down
            {
                quad temp = new quad(prevDownLoc, prevUpLoc, mouseDownLoc, mouseUpLoc);
                ds.Add(temp);
                ds.useDrawProgress = false;

                prevDownLoc = mouseDownLoc;
                mouseDownLoc = new Point();
                mouseUpLoc = new Point();
                
            }
        }
        
        private void glControl1_MouseUp(object sender, MouseEventArgs e)
        {
            prevUpLoc = mouseUpLoc;
            mouseUpLoc = e.Location;
            if (userLine)
            {
                line temp = new line(mouseDownLoc, mouseUpLoc);
                temp.propColor = Color.GreenYellow;
                ds.Add(temp);
                ds.useDrawProgress = false;
                mouseDownLoc = new Point();
            }
            if (userPoly)
            {
                //userPolyPts.Add(mouseDownLoc);
                userPolyPts.Add(mouseUpLoc);
                polygon temp = new polygon(userPolyPts);
                ds.Add(temp);
                ds.useDrawProgress = false;
                mouseDownLoc = new Point();
                //mouseUpLoc = mouseDownLoc;
            }
            
            listBox1.Items.Clear();
            listBox1.Items.AddRange(ds.viewObjectList());
        }

        private void glControl1_MouseMove(object sender, MouseEventArgs e)
        {
            // show the line while the user is holding down the mouse button
            if (userLine && (mouseDownLoc != new Point()))
            {
                line temp = new line(mouseDownLoc, e.Location);
                ds.setProgressObj(temp);
                ds.useDrawProgress = true;
            }
            if (userPoly && userPolyPts.Count > 0 && mouseDownLoc != new Point())
            {
                userPolyPts.Add(e.Location); // mouse current location
                polygon temp = new polygon(userPolyPts);
                ds.setProgressObj(temp);
                ds.useDrawProgress = true;
            }
            if (userPoint)
            {
                point temp = new point(e.Location);
                temp.glSize = 5;
                temp.propColor = Color.Azure;
                ds.setProgressObj(temp);
                ds.useDrawProgress = true;
            }
            if (userQuad && (mouseUpLoc != new Point()) && (mouseDownLoc != new Point()) )
            {
                quad temp = new quad(mouseDownLoc, mouseUpLoc, e.Location, e.Location);
                ds.setProgressObj(temp);
                ds.useDrawProgress = true;
            }
        }       

        private void glControl1_MouseHover(object sender, EventArgs e)
        {

        }
                
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            int testSize = 0;
            Point[] pts = new Point[3];
            pts[0] = new Point(0 + testSize, 0);
            pts[1] = new Point(100 + testSize, 0);
            pts[2] = new Point(50 + testSize, 50);
            triangle idk = new triangle(pts[0], pts[1], pts[2]);
            line idk2 = new line(pts[2], new Point(400, 400));
            idk.propColor = Color.Red;
            idk2.propColor = Color.Green;

            ds.Add(idk);
            ds.Add(idk2);

            listBox1.Items.Clear();
            listBox1.Items.AddRange(ds.viewObjectList());       
        }
        
        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            // change the color of the selected item
            ds.deselectAll();

            int selID = ds.getID(listBox1.SelectedItem.ToString());

            //glPrimitives sel = ds.getPrimByID(selID);
            ds.selectPrimByID(selID);
                                    
        }
        
        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && (listBox1.SelectedItems.Count == 1))
            {
                Point pt = listBox1.PointToScreen(e.Location);
                contextMenuStrip1.Show(pt);
            }
        }

        #region toolButtons
        private void buttonLine_Click(object sender, EventArgs e)
        {
            userLine = true;
            userPoly = false;
            userPoint = false;
            userQuad = false;
            mouseDownLoc = new Point();
            mouseUpLoc = new Point();
        }

        private void buttonPolyLine_Click(object sender, EventArgs e)
        {
            userLine = false;
            userPoly = true;
            userPoint = false;
            userQuad = false;
            userPolyPts = new List<Point>();
            mouseDownLoc = new Point();
            mouseUpLoc = new Point();
        }

        private void buttonPoint_Click(object sender, EventArgs e)
        {
            userLine = false;
            userPoly = false;
            userPoint = true;
            userQuad = false;
            mouseDownLoc = new Point();
            mouseUpLoc = new Point();
        }

        private void buttonQuad_Click(object sender, EventArgs e)
        {
            userLine = false;
            userPoly = false;
            userPoint = false;
            userQuad = true;
            mouseDownLoc = new Point();
            mouseUpLoc = new Point();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            userLine = false;
            userPoly = false;
            userPoint = false;
            ds.deleteAll();
            listBox1.Items.Clear();
            listBox1.Items.AddRange(ds.viewObjectList());
            mouseDownLoc = new Point();
            mouseUpLoc = new Point();
        }
        #endregion

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ds.removePrimByID( ds.getID( listBox1.SelectedItem.ToString()));


            listBox1.Items.Clear();
            listBox1.Items.AddRange(ds.viewObjectList());    
        }









    }
}
