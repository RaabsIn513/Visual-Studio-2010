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
    public partial class glPrimitiveDialog : Form
    {
        private static string _Type = null;
        private bool _isOpen = true;
        /// <summary>
        /// To be returned upon close. Must use .ShowDialog(this) in the parent form.
        /// </summary>
        //public List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();
        //public glPrimitives result = null;
        private glPrimitives input = null;
        private glPrimitives output = null;
        private quad _aQuad = null;
        private triangle _aTri = null;
        private loopline _aLoopLine = null;
        private line _aLine = null;
        private point _aPoint = null;
        private polygon _aPoly = null;

        /// <summary>
        /// Load Options. Declare what shall be available in this instance of the primitives dialog
        /// </summary>
        public List<KeyValuePair<string, string>> _loadOptions = new List<KeyValuePair<string, string>>();

        private KeyValuePair<string, string> getByKey(List<KeyValuePair<string, string>> kvlist, string Key)
        {
            for (int i = 0; i < kvlist.Count(); i++)
            {
                if (kvlist[i].Key == Key)
                    return kvlist[i];
            }
            return new KeyValuePair<string, string>();
        }

        public glPrimitiveDialog()
        {
            InitializeComponent();
        }

        public glPrimitiveDialog(object glPrim)
        {
            input = (glPrimitives)glPrim;
            _Type = input.getPrimitiveType().ToUpper();

            InitializeComponent();
        }

        private void enableControls(bool SHOW_MAIN_COLOR_OP, bool SHOW_VERT_OP, bool SHOW_LINES_OP)
        {
            groupBox1.Show();
            groupBox2.Show();
            if (SHOW_VERT_OP)
            {
                groupBox1.Enabled = true;
                // TODO: find out the state of the SHOW_VERT for this _result object
            }
            else
                groupBox1.Enabled = false;

            if (SHOW_LINES_OP)
            {
                groupBox2.Enabled = true;
                // TODO: find out the state of the SHOW_VERT for this _result object
            }
            else
                groupBox2.Enabled = false;

        }

        private void glPrimitiveDialog_Load(object sender, EventArgs e)
        {
            this.Text = _Type + " properties";
            _isOpen = true;

            switch (_Type)
            {
                case "TRIANGLE":
                    _aTri = (triangle)input;
                    enableControls(true, true, true);
                    
                    checkBox_showVerts.Checked = _aTri.showVerts;
                    button_VertexColor.BackColor = _aTri.lineColor;
                    UpDown_VertextSize.Text = _aTri.vertSize.ToString();
                    
                    checkBox_showLines.Checked = _aTri.showLines;
                    button_LineColor.BackColor = _aTri.lineColor;
                    UpDown_LineWidth.Text = _aTri.lineWidth.ToString();

                    break;
                case "LINE":
                    _aLine = (line)input;
                    enableControls(true, true, false);
                    checkBox_showVerts.Checked = _aLine.showVerts;
                    UpDown_VertextSize.Text = _aLine.vertSize.ToString();
                    break;
                case "POINT":
                    _aPoint = (point)input;
                    enableControls(true, false, false);
                    break;
                case "POLYGON":
                    _aPoly = (polygon)input;
                    enableControls(true, true, true);
                    break;
                case "QUAD":
                    _aQuad = (quad)input;
                    enableControls(true, true, true);
                    checkBox_showVerts.Checked = _aQuad.showVerts;
                    button_VertexColor.BackColor = _aQuad.lineColor;
                    UpDown_VertextSize.Text = _aQuad.vertSize.ToString();

                    checkBox_showLines.Checked = _aQuad.showLines;
                    button_LineColor.BackColor = _aQuad.lineColor;
                    UpDown_LineWidth.Text = _aQuad.lineWidth.ToString();

                    button_ObjectColor.BackColor = _aQuad.propColor;
                    break;
                case "LOOPLINE":
                    _aLoopLine = (loopline)input;
                    enableControls(true, true, true);
                    break;
                default:

                    break;
            }
        }

        private void glPrimitiveDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            // TODO: Pass back user selected properites to the original function that called this form. 
            _isOpen = false;
        }

        private void buttonOkay_Click(object sender, EventArgs e)
        {
            //Gather up the information and set it
            output = new glPrimitives();
            output = input;

            switch (_Type)
            {
                case "TRIANGLE":
                    _aTri = (triangle)input;
                    _aTri.showVerts = checkBox_showVerts.Checked;
                    _aTri.lineColor = button_VertexColor.BackColor;
                    _aTri.vertSize = (float)Convert.ToDecimal(UpDown_VertextSize.Text.ToString());

                    _aTri.showLines = checkBox_showLines.Checked;
                    _aTri.lineColor = button_LineColor.BackColor;
                    _aTri.lineWidth = (float)Convert.ToDecimal(UpDown_LineWidth.Text.ToString());
                    output = _aTri;
                    break;
                case "LINE":
                    _aLine = (line)input;
                    _aLine.showVerts = checkBox_showVerts.Checked;
                    _aLine.propColor = button_ObjectColor.BackColor;
                    _aLine.vertColor = button_VertexColor.BackColor;
                    _aLine.vertSize = (float)Convert.ToDecimal(UpDown_VertextSize.Text.ToString());
                    output = _aLine;
                    break;
                case "POINT":
                    _aPoint = (point)input;
                    output = _aPoint;
                    break;
                case "POLYGON":
                    _aPoly = (polygon)input;
                    output = _aPoly;
                    break;
                case "QUAD":
                    _aQuad = (quad)input;
                    _aQuad.showVerts = checkBox_showVerts.Checked;
                    _aQuad.lineColor = button_VertexColor.BackColor;
                    _aQuad.vertSize = (float)Convert.ToDecimal(UpDown_VertextSize.Text.ToString());

                    _aQuad.showLines = checkBox_showLines.Checked;
                    _aQuad.lineColor = button_LineColor.BackColor;
                    _aQuad.lineWidth = (float)Convert.ToDecimal(UpDown_LineWidth.Text.ToString());

                    _aQuad.propColor = button_ObjectColor.BackColor;
                    output = _aQuad;
                    break;
                case "LOOPLINE":
                    _aLoopLine = (loopline)input;
                    output = _aLoopLine;
                    break;
                default:

                    break;
            }

            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {

        }

        public bool isOpen
        {
            get { return _isOpen; }
        }

        public object getResult()
        {
            return (object)output;
        }
        
        #region ColorButtons

        private void button_VertexColor_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog(this);
            button_VertexColor.BackColor = colorDialog1.Color;
        }

        private void button_LineColor_Click(object sender, EventArgs e)
        {
            colorDialog2.ShowDialog(this);
            button_LineColor.BackColor = colorDialog2.Color;
        }

        private void button_ObjectColor_Click(object sender, EventArgs e)
        {
            colorDialog3.ShowDialog(this);
            button_ObjectColor.BackColor = colorDialog3.Color;
        }
        
#endregion
    }
}
