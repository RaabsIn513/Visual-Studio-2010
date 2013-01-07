using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace OpenTK_002_WindowsForm
{
    static class xmlExporter
    {
        public static void export(renderList anRL)
        {
            StreamWriter sw;
            sw = new StreamWriter(Directory.GetCurrentDirectory() + "\\xml_export.xml");
            sw.WriteLine("<xml_export>");
            for (int i = 0; i < anRL.Count; i++)
            {
                sw.WriteLine("<object id=\"" + anRL[i].id + "\"" + " color=\"" + anRL[i].color.ToString() + "\" name=\"" + anRL[i].name + "\">");
                sw.WriteLine("<Position>");
                sw.WriteLine();
                for (int c = 0; c < anRL[i].data.Count(); c++)
                {
                    sw.Write("<Point" + c + " X=\"" + anRL[i].data[c].Position.X.ToString() + "\" ");
                    sw.Write("Y=\"" + anRL[i].data[c].Position.Y.ToString() + "\" ");
                    sw.Write("Z=\"" + anRL[i].data[c].Position.Z.ToString() + "\"/>");
                    sw.WriteLine();
                }
                sw.WriteLine("</Position>");
                sw.WriteLine("<Normal>");
                sw.WriteLine();
                for (int c = 0; c < anRL[i].data.Count(); c++)
                {
                    sw.Write("<Normal" + c + "  X=\"" + anRL[i].data[c].Normal.X.ToString() + "\" ");
                    sw.Write("Y=\"" + anRL[i].data[c].Normal.Y.ToString() + "\" ");
                    sw.Write("Z=\"" + anRL[i].data[c].Normal.Z.ToString() + "\"/>");
                    sw.WriteLine();
                }
                sw.WriteLine("</Normal>");
                sw.WriteLine("</object>");
            }
            sw.WriteLine("</xml_export>");

            sw.Close();
        }
    }
}
