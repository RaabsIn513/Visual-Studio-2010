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
    class renderList : List<VertexBuffer>
    {
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
                this[i].Render();
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
    }
}
