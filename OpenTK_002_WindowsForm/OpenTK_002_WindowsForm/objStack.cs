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
    class objStack
    {
        public static List<glPrimitives> toDraw;
        public static glPrimitives progressObj;
        private static bool drawProgressObj = false;
        private static bool isDrawing = false;
        private static int id = 100;
        public objStack()
        {
            toDraw = new List<glPrimitives>();
            toDraw.Clear();
        }

        public bool busyDrawing()
        {
            return isDrawing;
        }

        public bool useDrawProgress
        {
            get { return drawProgressObj; }
            set { drawProgressObj = value;}
        }

        public void drawProgress()
        {
            drawObject(progressObj);
        }

        public void drawPrimList()
        {
            for (int i = 0; i < toDraw.Count; i++)
                drawObject(toDraw[i]);                
            
            if (drawProgressObj)
                drawProgress();

        }

        public void drawObject(glPrimitives anyObject)
        {
            switch (anyObject.getPrimitiveType())
            {
                case "TRIANGLE":
                    isDrawing = true;
                    drawTriangle(anyObject);
                    break;
                case "LINE":
                    isDrawing = true;
                    drawLine(anyObject);
                    break;
                case "POINT":
                    isDrawing = true;
                    drawPoint(anyObject);
                    break;
                case "POLYGON":
                    isDrawing = true;
                    drawPolygon(anyObject);
                    break;
                case "QUAD":
                    isDrawing = true;
                    drawQuad(anyObject);
                    break;
                case "LOOPLINE":
                    isDrawing = true;
                    drawLoopLine(anyObject);
                    break;
                default:
                    isDrawing = false;
                    break;
            }
            isDrawing = false;
        }
        
        #region drawing Methods

        public void drawLoopLine(glPrimitives drawLoopLine)
        {
            loopline LOOPLINE = (loopline)drawLoopLine;
            List<Point> geoData = drawLoopLine.getGeoData(); 
            // If selected change to selected color
            if (LOOPLINE.isSelected)
                GL.Color3(LOOPLINE.selectedColor);
            else
                GL.Color3(LOOPLINE.propColor);
            // Draw
            GL.Begin(BeginMode.LineLoop);
            
            for( int i = 0; i < geoData.Count(); i++ )
                GL.Vertex3(geoData[i].X, geoData[i].Y, 0);
            GL.End();
        }

        public void drawQuad(glPrimitives drawQuad)
        {
            quad Quad = (quad)drawQuad;
            List<Point> geoData = drawQuad.getGeoData();

            if (Quad.isSelected)
                GL.Color3(Quad.selectedColor);
            else
                GL.Color3(Quad.propColor);
            GL.Begin(BeginMode.Quads);
            GL.Vertex3(geoData[0].X, geoData[0].Y, 0);
            GL.Vertex3(geoData[1].X, geoData[1].Y, 0);
            GL.Vertex3(geoData[2].X, geoData[2].Y, 0);
            GL.Vertex3(geoData[3].X, geoData[3].Y, 0);
            
            GL.End();

            if (Quad.showVerts)
            {
                GL.PointSize(5);
                GL.Color3(Color.Orange);
                GL.Begin(BeginMode.Points);
                
                GL.Vertex3(geoData[0].X, geoData[0].Y, 0);
                GL.Vertex3(geoData[1].X, geoData[1].Y, 0);
                GL.Vertex3(geoData[2].X, geoData[2].Y, 0);
                GL.Vertex3(geoData[3].X, geoData[3].Y, 0);
                GL.End();
            }
        }

        public void drawPoint(glPrimitives drawPt)
        {
            point Point = (point)drawPt;
            List<Point> geoData = drawPt.getGeoData();
            
            if (drawPt.isSelected)
                GL.Color3(drawPt.selectedColor);
            else
                GL.Color3(drawPt.propColor);
            GL.PointSize(Point.size);
            GL.Begin(BeginMode.Points);
            GL.Vertex3(geoData[0].X, geoData[0].Y, 0);
            GL.End();
        }

        public void drawTriangle(glPrimitives drawTri)
        {
            // draw as a triangle
            List<Point> geoData = drawTri.getGeoData();
            if (drawTri.isSelected)
                GL.Color3(drawTri.selectedColor);
            else
                GL.Color3(drawTri.propColor); 
            GL.Begin(BeginMode.Triangles);
            GL.Vertex3(geoData[0].X, geoData[0].Y, 0);
            GL.Vertex3(geoData[1].X, geoData[1].Y, 0);
            GL.Vertex3(geoData[2].X, geoData[2].Y, 0);
            GL.End();
            
        }

        private void drawLine(glPrimitives drawLine)
        {
            line Line = (line)drawLine;
            List<Point> geoData = drawLine.getGeoData();
            if (drawLine.isSelected)
                GL.Color3(drawLine.selectedColor);
            else
                GL.Color3(drawLine.propColor); 
            GL.Begin(BeginMode.Lines);
            GL.Vertex3(geoData[0].X, geoData[0].Y, 0);
            GL.Vertex3(geoData[1].X, geoData[1].Y, 0);
            GL.End();

            if (Line.showVerts)
            {
                GL.PointSize(5);
                GL.Color3(Color.Orange);
                GL.Begin(BeginMode.Points);

                GL.Vertex3(geoData[0].X, geoData[0].Y, 0);
                GL.Vertex3(geoData[1].X, geoData[1].Y, 0);

                GL.End();
            }
        }

        private void drawPolygon(glPrimitives drawPoly)
        {
            List<Point> geoData = drawPoly.getGeoData();
            if (drawPoly.isSelected)
                GL.Color3(drawPoly.selectedColor);
            else
                GL.Color3(drawPoly.propColor);
            GL.Begin(BeginMode.Polygon);
            for (int i = 0; i < geoData.Count(); i++)
            {
                GL.Vertex3(geoData[i].X, geoData[i].Y, 0);
            }
            GL.End();
        }

#endregion
        
        /// <summary>
        /// Add a glPrimities object to the list data structure.
        /// This list is drawn at Render()
        /// </summary>
        /// <param name="obj"></param>
        public void Add( glPrimitives obj )
        {
            id += 1;
            obj.ID = id;
            toDraw.Add(obj);
        }

        /// <summary>
        /// View the list of glPrimities that are drawn everytime
        /// Render() is called as human readable descriptions.
        /// </summary>
        /// <returns></returns>
        public string[] viewObjectList()
        {
            List<string> result = new List<string>();

            for (int i = 0; i < toDraw.Count; i++)
            {
                result.Add(toDraw[i].ID.ToString() + ": " + toDraw[i].getPrimitiveType());
            }
            return result.ToArray();
        }

        public glPrimitives getPrimByID(int IDobj)
        {
            glPrimitives result = new glPrimitives();
            for (int i = 0; i < toDraw.Count; i++)
            {
                if (toDraw[i].ID == IDobj)
                    return toDraw[i];
            }
            return toDraw[0];
        }

        public void selectPrimByID(int IDobj)
        {
            for (int i = 0; i < toDraw.Count; i++)
            {
                if (toDraw[i].ID == IDobj)
                    toDraw[i].isSelected = true;
            }
        }

        public void deselectAll()
        {
            for (int i = 0; i < toDraw.Count; i++)
                toDraw[i].isSelected = false;
        }

        public void removePrimByID(int IDobj)
        {
            for (int i = 0; i < toDraw.Count; i++)
            {
                if (toDraw[i].ID == IDobj)
                    toDraw.RemoveAt(i);
            }
        }
        
        public int getID(string listBoxItem)
        {
            string temp = listBoxItem.Substring(0, listBoxItem.IndexOf(":"));
            int result = 0;
            try
            {
                result = Convert.ToInt32(temp);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return result;
        }

        public void deleteLast()
        {
            toDraw.RemoveAt(toDraw.Count - 1);
        }

        public void deleteAll()
        {
            toDraw.Clear();
        }

        public void setProgressObj(glPrimitives obj)
        {
            progressObj = new glPrimitives();
            progressObj = obj;
        }
    }
}
