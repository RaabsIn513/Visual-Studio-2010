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
        MouseCoord mCo;
        Thread MouseCoordThread;

        private void OnWorkerProgressChanged(object sender, ProgressChangedArgs e)
        {
            //cross thread - so you don't get the cross theading exception
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    OnWorkerProgressChanged(sender, e);

                });
                return;
            }

            //change control
            this.label2.Text = e.Progress;
        }
        
        public Form1()
        {
            InitializeComponent();

            mCo = new MouseCoord();
            mCo.ProgressChanged += new EventHandler<ProgressChangedArgs>(OnWorkerProgressChanged);
            MouseCoordThread = new Thread(new ThreadStart(mCo.StartWork));

            MouseCoordThread.Start();
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            loaded = true;

            GL.ClearColor(Color.Black);         // world background color

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

            Axis.drawOrigin();

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
            Point actualGL_pos = new Point(e.Location.X - (int)xWCS, e.Location.Y - (int)yWCS);
            mouseDownLoc = actualGL_pos;

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
            Point actualGL_pos = new Point(e.Location.X - (int)xWCS, e.Location.Y - (int)yWCS);
            mouseUpLoc = actualGL_pos;
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

            #region rightClickVertexSnap
            //if (e.Button == MouseButtons.Right)
            //{   // use vertex snap
            //    deselectTools();
                
            //    Cursor.Position = glControl1.PointToScreen(ds.getNearestPoint(e.Location, 25.0));
            //    //Thread.Sleep(30);
            //}
            #endregion

            listView1.Items.Clear();
            listView1.Items.AddRange(ds.viewObjectListLVI().ToArray());
        }

        private void glControl1_MouseMove(object sender, MouseEventArgs e)
        {
            //if()
            //{
            //    Cursor.Position = glControl1.PointToScreen(ds.getNearestPoint(e.Location, 25.0));
            //    tcb = new System.Threading.TimerCallback(ProcessTimerEvent);
            //    clsTime     time = new clsTime();
            //    timer = new System.Threading.Timer(tcb, time, 4000, 1000);
            //}
            // show the line while the user is holding down the mouse button
            Point actualGL_pos = new Point(e.Location.X - (int)xWCS, e.Location.Y - (int)yWCS);

            label3.Text = "X: " + actualGL_pos.X.ToString() + " Y: " + actualGL_pos.Y.ToString();

            if (userLine && (mouseDownLoc != new Point()))
            {
                line temp = new line(mouseDownLoc, actualGL_pos);
                ds.setProgressObj(temp);
                ds.useDrawProgress = true;

            }
            if (userPoly && userPolyPts.Count > 0 && mouseDownLoc != new Point())
            {
                userPolyPts.Add(actualGL_pos); // mouse current location
                polygon temp = new polygon(userPolyPts);
                ds.setProgressObj(temp);
                ds.useDrawProgress = true;
            }
            if (userPoint)
            {
                point temp = new point(actualGL_pos);
                temp.size = 5;
                temp.propColor = Color.Azure;
                ds.setProgressObj(temp);
                ds.useDrawProgress = true;
            }
            if (userQuad && (mouseDownLoc != new Point()))
            {
                quad temp = new quad(mouseDownLoc, actualGL_pos ); // use the two point method
                ds.setProgressObj(temp);
                ds.useDrawProgress = true;
                prevDownLoc = mouseDownLoc;
            }
            if (userLoopLine && (mouseDownLoc != new Point()) && (userPoints.Count() > 0))
            {
                userPoints.Add(actualGL_pos);
                loopline temp = new loopline(userPoints);
                userPoints.RemoveAt(userPoints.Count() - 1);
                ds.setProgressObj(temp);
                ds.useDrawProgress = true;
            }
            //Cursor.Position = glControl1.PointToScreen(ds.getNearestPoint(e.Location, 25.0));
            //Thread.Sleep(30);
        }       

        private void glControl1_MouseHover(object sender, EventArgs e)
        {

        }
                
        #endregion
        
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

        private string getSelectedTool()
        {
            
            return "";
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

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            // The Opening of the context menu strip. 
            // We need to determine what options to give the user
            // eg. Points: color, size options
            //     Lines: color, width, point (show,size,color) etc
            //MessageBox.Show(listBox1.SelectedItem.GetType().ToString());
        }
  
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

        private void button_FromFile_Click(object sender, EventArgs e)
        {
            System.IO.Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            loopline fileDraw;
            List<Point> tempData = new List<Point>();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            // Insert code to read the stream here.
                            System.IO.StreamReader sr = new System.IO.StreamReader(myStream);
                            
                            string temp = null;
                            while (!sr.EndOfStream)
                            {
                                temp = sr.ReadLine();
                                if( temp.Contains(",") )
                                {
                                    int x = Convert.ToInt32(temp.Substring(0, temp.IndexOf(",") ));
                                    int y = Convert.ToInt32(temp.Substring(temp.IndexOf(",") +1 ));
                                    tempData.Add(new Point(x, y));
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
                fileDraw = new loopline(tempData);
                ds.Add(fileDraw);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selItems = listView1.SelectedItems;

            for (int i = 0; i < listView1.Items.Count; i++)
            {
                ds.getPrimByID(Convert.ToInt32(listView1.Items[i].Text.ToString())).isSelected = false;
            }
            
            string showSelItems = null;
            for (int i = 0; i < selItems.Count; i++)
            {
                showSelItems += selItems[i].SubItems[0].Text.ToString() + " : " + selItems[i].SubItems[1].Text.ToString() + "\n";

                ds.getPrimByID(Convert.ToInt32(selItems[i].SubItems[0].Text.ToString())).isSelected = true; // set as selected
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            MouseCoordThread.Abort();
            this.Dispose();
        }

    }
}
