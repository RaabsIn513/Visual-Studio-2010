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
        private bool _listLighting = false;

        public bool listLighting
        {
            get
            {
                return _listLighting;
            }
            set { _listLighting = value; }
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

        public void deleteAll()
        {
                this.RemoveRange(0,this.Count);
        }
    }
}
