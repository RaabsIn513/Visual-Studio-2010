using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SimpleMod
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "" && textBox2.Text != "")
                    textBox3.Text = Convert.ToString(Convert.ToDouble(textBox1.Text.ToString()) % Convert.ToDouble(textBox2.Text.ToString()));
            }
            catch
            {
                throw new Exception("You must enter number");
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "" && textBox2.Text != "")
                    textBox3.Text = Convert.ToString(Convert.ToDouble(textBox1.Text.ToString()) % Convert.ToDouble(textBox2.Text.ToString()));
            }
            catch
            {
                throw new Exception("You must enter number");
            }
        }
    }
}
