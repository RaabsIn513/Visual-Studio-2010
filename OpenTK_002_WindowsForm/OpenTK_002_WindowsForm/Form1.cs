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
        bool userLoopLine = false;
        bool userPoly = false;
        bool userPoint = false;
        bool userQuad = false;
        bool userCircle = false;
        List<Point> userPoints = new List<Point>();
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
                temp.size = 5;
                ds.Add(temp);
                ds.useDrawProgress = true;  // continue showing the point as the mouse moves on screen
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
            if (userQuad && (mouseDownLoc != new Point()))
            {
                quad temp = new quad(mouseDownLoc, mouseUpLoc);
                ds.Add(temp);
                ds.useDrawProgress = false;

                prevDownLoc = mouseDownLoc;
                mouseDownLoc = new Point();
                mouseUpLoc = new Point();
            }
            if (userLoopLine && (mouseDownLoc != new Point()))
            {
                userPoints.Add(mouseDownLoc);
                userPoints.Add(mouseUpLoc);

                loopline temp = new loopline(userPoints);
                ds.Add(temp);
                ds.useDrawProgress = false;
            }

            //listBox1.Items.Clear();
            //listBox1.Items.AddRange(ds.viewObjectList());

            listView1.Items.Clear();
            listView1.Items.AddRange(ds.viewObjectListLVI().ToArray());
        }
        
        private Point withInRadius(Point centerPoint, float radius)
        {
            List<Point> pointL = ds.allPointData();
            Point result = centerPoint;
            double closestRad = new double();
            closestRad = Math.Pow(radius, 2);
            // Pick the closest one
            if (ds.allPointData() != null)
            {
                for (int i = 0; i < pointL.Count(); i++)
                {
                    double testRad = Math.Pow(pointL[i].X - centerPoint.X, 2) + Math.Pow(pointL[i].Y - centerPoint.Y, 2);

                    if (testRad < closestRad) // there is a point within the radius
                    {
                        closestRad = testRad;
                        result = new Point(pointL[i].X, pointL[i].Y);
                    }
                }
            }
            return result;
        }
        
        private Point vertTextSnap(Point currentPosition)
        {
            Point newLoc = withInRadius(currentPosition, 20);
            if (newLoc != currentPosition )
            {
                return newLoc;
            }
            else
                return currentPosition;
        }
        
        private void glControl1_MouseMove(object sender, MouseEventArgs e)
        {
            // Vertext Snap?
            
            // TODO: Vertext Snap: move mouse to vertex if it is close enough to it. (with in radius detection)
            if ( !userLine )
            {
                //Point idk = glControl1.PointToScreen(e.Location);
                //Cursor.Position = new Point(idk.X + 5, idk.Y);

                //Cursor.Position = glControl1.PointToScreen( vertTextSnap(e.Location) );
                Cursor.Position = glControl1.PointToScreen(ds.getClosestPointWithInRadius(e.Location, 20));

                //Thread.Sleep(1000);
            }

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
                temp.size = 5;
                temp.propColor = Color.Azure;
                ds.setProgressObj(temp);
                ds.useDrawProgress = true;
            }
            if (userQuad && (mouseDownLoc != new Point()))
            {
                quad temp = new quad(mouseDownLoc, e.Location); // use the two point method
                ds.setProgressObj(temp);
                ds.useDrawProgress = true;
                prevDownLoc = mouseDownLoc;
            }
            if (userLoopLine && (mouseDownLoc != new Point()) && (userPoints.Count() > 0))
            {
                userPoints.Add(e.Location);
                loopline temp = new loopline(userPoints);
                userPoints.RemoveAt(userPoints.Count() - 1);
                ds.setProgressObj(temp);
                ds.useDrawProgress = true;
            }

            //Cursor.Position = new Point(30, 30);

            //IntPtr idk = glControl1.Cursor.Handle;

            //idk = idk + 1;
        }       

        private void glControl1_MouseHover(object sender, EventArgs e)
        {

        }
                
        #endregion
        
        //private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        //{
        //    // change the color of the selected item
        //    ds.deselectAll();

        //    int selID = ds.getID(listBox1.SelectedItem.ToString());

        //    //glPrimitives sel = ds.getPrimByID(selID);
        //    ds.selectPrimByID(selID);
                                    
        //}
        
        //private void listBox1_MouseDown(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Right && (listBox1.SelectedItems.Count == 1))
        //    {
        //        Point pt = listBox1.PointToScreen(e.Location);
        //        contextMenuStrip1.Show(pt);
        //    }
        //}

        #region toolButtons

        private void deselectTools()
        {
            userLine = false;
            userLoopLine = false;
            userPoly = false;
            userPoint = false;
            userQuad = false;
            userCircle = false;
        }

        private void selectTool(string buttonText)
        {
            userLine = false;
            userLoopLine = false;
            userPoly = false;
            userPoint = false;
            userQuad = false;
            userCircle = false;

            switch (buttonText)
            {
                case "Line":
                    userLine = true;
                    break;
                case "LoopLine":
                    userLoopLine = true;
                    break;
                case "PolyLine":
                    userPoly = true;
                    break;
                case "Point":
                    userPoint = true;
                    break;
                case "Quad":
                    userQuad = true;
                    break;
                case "Circle":
                    userCircle = true;
                    break;
                default:
                    userLine = false;
                    userLoopLine = false;
                    userPoly = false;
                    userPoint = false;
                    userQuad = false;
                    userCircle = false;
                    break;
            }
        }
        
        private void buttonLine_Click(object sender, EventArgs e)
        {
            selectTool(btnLine.Text.ToString());
            mouseDownLoc = new Point();
            mouseUpLoc = new Point();
        }

        private void buttonLoopLine_Click(object sender, EventArgs e)
        {
            selectTool(btnLoopLine.Text.ToString());
            mouseDownLoc = new Point();
            mouseUpLoc = new Point();
        }

        private void buttonPolyLine_Click(object sender, EventArgs e)
        {
            selectTool(btnPolyLine.Text.ToString());
            userPolyPts = new List<Point>();
            mouseDownLoc = new Point();
            mouseUpLoc = new Point();
        }

        private void buttonPoint_Click(object sender, EventArgs e)
        {
            selectTool(btnPoint.Text.ToString());
            mouseDownLoc = new Point();
            mouseUpLoc = new Point();
        }

        private void buttonQuad_Click(object sender, EventArgs e)
        {
            selectTool(btnQuad.Text.ToString());
            mouseDownLoc = new Point();
            mouseUpLoc = new Point();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            deselectTools();
            ds.deleteAll();
            //listBox1.Items.Clear();
            //listBox1.Items.AddRange(ds.viewObjectList());

            listView1.Items.Clear();
            listView1.Items.AddRange(ds.viewObjectListLVI().ToArray());
            mouseDownLoc = new Point();
            mouseUpLoc = new Point();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            deselectTools();
        }

        #endregion

        #region listView Context Menu

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ds.removePrimByID( ds.getID( listBox1.SelectedItem.ToString()));

            //listBox1.Items.Clear();
            //listBox1.Items.AddRange(ds.viewObjectList());
            listView1.Items.Clear();
            listView1.Items.AddRange(ds.viewObjectListLVI().ToArray());

        }

        private void showVerticiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // show the verticies of the objecet
            //glPrimitives selectedObj = ds.getPrimByID(ds.getID(listBox1.SelectedItem.ToString()));
            //selectedObj.getPrimitiveType();

            //selectedObj.showVerts = true;
        }

        #endregion


        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            // The Opening of the context menu strip. 
            // We need to determine what options to give the user
            // eg. Points: color, size options
            //     Lines: color, width, point (show,size,color) etc
            //MessageBox.Show(listBox1.SelectedItem.GetType().ToString());
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //private void listView1_DoubleClick(object sender, EventArgs e)
        //{
        //    //MessageBox.Show("What's up");

        //    ListView.SelectedListViewItemCollection selectedItem = listView1.SelectedItems;

        //    if (selectedItem.Count == 1)
        //    {
        //        // Get the glPrimitive's ID and get the glPrimitive
        //        string primID = selectedItem[0].SubItems[0].Text;
        //        glPrimitives selectedObj = ds.getPrimByID(Convert.ToInt32(primID));
        //        string type = selectedObj.GetType().ToString();
        //        type = type.Substring( (type.IndexOf(".") + 1) );
                
        //        List<KeyValuePair<string, string>> loadOps = new List<KeyValuePair<string, string>>();
        //        type = type.ToUpper();
        //        switch (type)
        //        {
        //            case "TRIANGLE":
        //                loadOps.Add(new KeyValuePair<string, string>("TYPE", "TRIANGLE"));
        //                loadOps.Add(new KeyValuePair<string,string>("SHOW_VERTS_OP", "TRUE"));
        //                loadOps.Add(new KeyValuePair<string, string>("SHOW_LINES_OP", "TRUE"));
        //                selectedObj = (triangle)selectedObj;
        //                break;
        //            case "LINE":
        //                loadOps.Add(new KeyValuePair<string, string>("TYPE", "LINE"));
        //                loadOps.Add(new KeyValuePair<string,string>("SHOW_VERTS_OP", "TRUE"));
        //                loadOps.Add(new KeyValuePair<string, string>("SHOW_LINES_OP", "FALSE"));
        //                selectedObj = (line)selectedObj;
        //                break;
        //            case "POINT":
        //                loadOps.Add(new KeyValuePair<string, string>("TYPE", "POINT"));
        //                loadOps.Add(new KeyValuePair<string,string>("SHOW_VERTS_OP", "FALSE"));
        //                loadOps.Add(new KeyValuePair<string, string>("SHOW_LINES_OP", "FALSE"));
        //                break;
        //            case "POLYGON":
        //                loadOps.Add(new KeyValuePair<string, string>("TYPE", "POLYGON"));
        //                loadOps.Add(new KeyValuePair<string,string>("SHOW_VERTS_OP", "TRUE"));
        //                loadOps.Add(new KeyValuePair<string, string>("SHOW_LINES_OP", "TRUE"));
        //                break;
        //            case "QUAD":
        //                loadOps.Add(new KeyValuePair<string, string>("TYPE", "QUAD"));
        //                loadOps.Add(new KeyValuePair<string,string>("SHOW_VERTS_OP", "TRUE"));
        //                loadOps.Add(new KeyValuePair<string, string>("SHOW_LINES_OP", "TRUE"));
        //                break;
        //            case "LOOPLINE":
        //                loadOps.Add(new KeyValuePair<string, string>("TYPE", "LOOPLINE"));
        //                loadOps.Add(new KeyValuePair<string,string>("SHOW_VERTS_OP", "TRUE"));
        //                loadOps.Add(new KeyValuePair<string, string>("SHOW_LINES_OP", "TRUE"));
        //                break;
        //            default:
        //                loadOps.Add(new KeyValuePair<string,string>("SHOW_VERTS_OP", "FALSE"));
        //                loadOps.Add(new KeyValuePair<string, string>("SHOW_LINES_OP", "FALSE"));

        //                break;
        //        }
        //        // start dialog
        //        glPrimitiveDialog dia = new glPrimitiveDialog(loadOps);
        //        dia.StartPosition = FormStartPosition.CenterParent;
        //        dia.ShowDialog(this);

        //        List<KeyValuePair<string,string>> diaResult = dia.result;

        //        // Process the dialog results...
        //        if (getByKey(diaResult, "SHOW_VERTS").Value != null)
        //        {
        //            //(quad)selectedObj.showVerts = true;
        //        }
        //    }

        //    //MessageBox.Show("ID: " + selectedItem[0].SubItems[0].Text + " TYPE: " + selectedItem[0].SubItems[1].Text + " COLOR: " + selectedItem[0].SubItems[2].Text);
        //}
        
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListView.SelectedListViewItemCollection selectedItem = listView1.SelectedItems;

            if (selectedItem.Count == 1)
            {
                // Get the glPrimitive's ID and get the glPrimitive
                string primID = selectedItem[0].SubItems[0].Text;                       //Get the ID from the ID column
                glPrimitives selectedObj = ds.getPrimByID(Convert.ToInt32(primID));     //Get the glPrimitive Object from the list
                object passTo = (object)selectedObj;                                    //Cast it as a C# object
                glPrimitiveDialog dia = new glPrimitiveDialog(passTo);                  //Pass it to a new instance of glPrimitiveDialog (C# can't pass custom classes between forms)
                dia.ShowDialog(this);                                                   //ShowDialog (pauses here till user closes dialog)
                selectedObj = (glPrimitives)dia.getResult();                            //Set the selected object to the result from the dialog
            }
        }

        private KeyValuePair<string, string> getByKey(List<KeyValuePair<string, string>> kvlist, string Key)
        {
            for (int i = 0; i < kvlist.Count(); i++)
            {
                if (kvlist[i].Key == Key)
                    return kvlist[i];
            }
            return new KeyValuePair<string, string>();
        }

    }
}
