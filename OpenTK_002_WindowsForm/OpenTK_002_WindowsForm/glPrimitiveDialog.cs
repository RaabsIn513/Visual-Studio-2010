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

        private void enableControls(bool SHOW_VERT_OP, bool SHOW_LINES_OP)
        {
            checkBox_showVerts.Show();
            checkBox_showLines.Show();
            if (SHOW_VERT_OP)
            {
                checkBox_showVerts.Enabled = true;
                
                // TODO: find out the state of the SHOW_VERT for this _result object
            }
            else
                checkBox_showVerts.Enabled = false;

            if (SHOW_LINES_OP)
            {
                checkBox_showLines.Enabled = true;
                // TODO: find out the state of the SHOW_VERT for this _result object
            }
            else
                checkBox_showLines.Enabled = false;

        }

        private void glPrimitiveDialog_Load(object sender, EventArgs e)
        {
            this.Text = _Type + " properties";
            _isOpen = true;

            switch (_Type)
            {
                case "TRIANGLE":
                    _aTri = (triangle)input;
                    enableControls(true, true);
                    checkBox_showVerts.Checked = _aTri.showVerts;
                    checkBox_showLines.Checked = _aTri.showLines;
                    break;
                case "LINE":
                    _aLine = (line)input;
                    enableControls(true, false);
                    checkBox_showVerts.Checked = _aLine.showVerts;
                    break;
                case "POINT":
                    _aPoint = (point)input;
                    enableControls(false, false);
                    break;
                case "POLYGON":
                    _aPoly = (polygon)input;
                    enableControls(true, true);
                    break;
                case "QUAD":
                    _aQuad = (quad)input;
                    enableControls(true, true);
                    checkBox_showVerts.Checked = _aQuad.showVerts;
                    checkBox_showLines.Checked = _aQuad.showLines;
                    break;
                case "LOOPLINE":
                    _aLoopLine = (loopline)input;
                    enableControls(true, true);
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
                    _aTri.showLines = checkBox_showLines.Checked;
                    output = _aTri;
                    break;
                case "LINE":
                    _aLine = (line)input;
                    _aLine.showVerts = checkBox_showVerts.Checked;
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
                    _aQuad.showLines = checkBox_showLines.Checked;
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
    }
}
