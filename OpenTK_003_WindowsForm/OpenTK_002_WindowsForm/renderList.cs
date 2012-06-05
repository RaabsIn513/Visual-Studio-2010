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
using OpenTK.Platform;

namespace OpenTK_002_WindowsForm
{
    class renderList : List<VertexBuffer>
    {
        public List<VertexBuffer> movingVBOs = new List<VertexBuffer>();
        public List<VertexBuffer> rotatingVBOs = new List<VertexBuffer>();
        private bool _moveVBO = false;
        private bool _rotateVBO = false;
        private bool _listLighting = false;

        public bool listLighting
        {
            get
            {
                return _listLighting;
            }
            set { _listLighting = value; }
        }

        public bool moveVBO
        {
            get { return _moveVBO; }
            set { _moveVBO = value; }
        }

        public string[] NamesInList()
        {
            List<string> result = new List<string>();

            for (int i = 0; i < this.Count; i++)
                result.Add(this[i].name);

            return result.ToArray();
        }

        public string NameInList(int index)
        {
            return this[index].name;
        }

        public void Render()
        {
            for (int i = 0; i < this.Count; i++)
            {
                this[i].Render();
                if (true)
                {
                    int idk = this[i].id;
                    GL.BindBuffer(BufferTarget.ArrayBuffer, idk);
                    
                    // Set the Pointer to the current bound array describing how the data ia stored
                    GL.NormalPointer(NormalPointerType.Float, OpenTK.Vector3.SizeInBytes, IntPtr.Zero);

                    // Enable the client state so it will use this array buffer pointer
                    GL.EnableClientState(EnableCap.NormalArray);
                }
            }
            
            for (int i = 0; i < movingVBOs.Count; i++)
                movingVBOs[i].Render();
            for (int i = 0; i < rotatingVBOs.Count; i++)
                rotatingVBOs[i].Render();
           
        }

        public List<ListViewItem> forListViewCtrl()
        {
            List<ListViewItem> result = new List<ListViewItem>();

            for (int i = 0; i < this.Count; i++)
            {
                ListViewItem temp = new ListViewItem(this[i].id.ToString());
                temp.SubItems.Add(this[i].name);
                temp.SubItems.Add(this[i].color.ToString());
                result.Add(temp);
            }
            return result;
        }

        public void deselectAll()
        {
            for (int i = 0; i < this.Count; i++)
                this[i].isSelected = false;
        }

        public int[] getSelectedIDs()
        {
            List<int> result = new List<int>();
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].isSelected == true)
                    result.Add(this[i].id);
            }
            return result.ToArray();
        }

        public VertexBuffer[] getSelected()
        {
            List<VertexBuffer> result = new List<VertexBuffer>();
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].isSelected == true)
                    result.Add(this[i]);
            }
            return result.ToArray();
        }

        public VertexBuffer getByID(int id)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].id == id)
                    return this[i];
            }
            throw new ArgumentException("getByID did not find id=" + id + " in the renderList");
        }

        public void deleteByID(int id)
        {
            try
            {
                for (int i = 0; i < this.Count; i++)
                {
                    if (this[i].id == id)
                    {
                        this.RemoveAt(i);
                        break;
                    }
                }
            }
            catch
            {
                throw new ArgumentException("deleteByID did not find id=" + id + " in the renderList");
            }
        }

        public void replaceByID(int id, VertexBuffer replaceWith)
        {
            this.deleteByID(id);
            this.Add(replaceWith);
        }

        public void copyByID(int id)
        {
            VertexBuffer newVBO = new VertexBuffer();
            newVBO.data = this.getByID(id).data;
            this.Add(newVBO);
        }

        public void moveByIDs(int[] id)
        {
            for (int i = 0; i < id.Length; i++)
                movingVBOs.Add(getByID(id[i]));
            for (int k = 0; k < id.Length; k++)
                deleteByID(id[k]);
            _moveVBO = true;
        }

        public void rotateByIDs(int[] id)
        {
            for (int i = 0; i < id.Length; i++)
                rotatingVBOs.Add(getByID(id[i]));
            for (int k = 0; k < id.Length; k++)
                deleteByID(id[k]);
            _rotateVBO = true;
        }

        public void movingVBOs_Translate(float x, float y, float z)
        {
            for (int i = 0; i < movingVBOs.Count(); i++)
                movingVBOs[i].Translate(x, y, z);
        }

        public void rotatingVBOs_Rotate(float angle, float x, float y, float z)
        {
            for (int i = 0; i < rotatingVBOs.Count(); i++)
                rotatingVBOs[i].Rotate(angle* 0.0025f, x, y, z);
        }

        public void place()
        {
            if (_moveVBO || _rotateVBO)
            {
                for (int i = 0; i < movingVBOs.Count; i++)
                {
                    this.Add(movingVBOs[i]);
                }
                for (int i = 0; i < rotatingVBOs.Count; i++)
                {
                    this.Add(rotatingVBOs[i]);
                }
            }
            movingVBOs = new List<VertexBuffer>();
            rotatingVBOs = new List<VertexBuffer>();
            _moveVBO = false;
            _rotateVBO = false;
        }

        public void deleteAll()
        {
                this.RemoveRange(0,this.Count);
        }
    }
}
