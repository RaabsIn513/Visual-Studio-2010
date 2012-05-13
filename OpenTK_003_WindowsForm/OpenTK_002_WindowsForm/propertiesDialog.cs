using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OpenTK_002_WindowsForm
{
    public partial class propertiesDialog : Form
    {
        VertexBuffer input;
        VertexBuffer output;

        public propertiesDialog()
        {
            InitializeComponent();
        }

        public propertiesDialog(object VBtoEdit)
        {
            InitializeComponent();
            input = (VertexBuffer)VBtoEdit;
        }

        private void propertiesDialog_Load(object sender, EventArgs e)
        {
            output = input;
            this.Text = output.name + " properties";
            tb_Name.Text = output.name;
            btn_color.BackColor = output.color;
        }

        private void btn_color_Click(object sender, EventArgs e)
        {
            Color result;
            colorDialog1.ShowDialog(this);
            result = colorDialog1.Color;
            btn_color.BackColor = result;
            output.color = result;
        }

        private void tb_Name_TextChanged(object sender, EventArgs e)
        {
            this.Text = tb_Name.Text + " properties";
            output.name = tb_Name.Text;
        }

        public object getResult()
        {
            return (object)output;
        }

        private void btn_Okay_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            output = new VertexBuffer();
            output = input; // pass back what was passed in.
            this.Close();
        }


    }
}
