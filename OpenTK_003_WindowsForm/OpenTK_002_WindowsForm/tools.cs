using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTK_002_WindowsForm
{
    class tools
    {
        private static string selectedTool = null;
        private static string selectedAction = null;
        private bool _pan;
        private bool _rotate;

        public string currentTool
        {
            get
            {
                if (selectedTool != null)
                    return selectedTool;
                else return null;
            }
        }

        public bool UserLine
        {
            get
            {
                if (this.currentTool == "USER_LINE")
                    return true;
                else
                    return false;
            }
            set
            {
                if( value )
                    selectedTool = "USER_LINE";
            }
        }

        public bool UserLoopLine
        {
            get
            {
                if (this.currentTool == "USER_LOOPLINE")
                    return true;
                else
                    return false;
            }
            set
            {
                if( value )
                    selectedTool = "USER_LOOPLINE";
            }
        }

        public bool UserPoly
        {
            get
            {
                if (this.currentTool == "USER_POLY")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    selectedTool = "USER_POLY";
            }
        }

        public bool UserQuad
        {
            get
            {
                if (this.currentTool == "USER_QUAD")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    selectedTool = "USER_QUAD";
            }
        }

        public bool UserPoint
        {
            get
            {
                if (this.currentTool == "USER_POINT")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    selectedTool = "USER_POINT";
            }
        }

        public bool Pan
        {
            get
            {
                return _pan;
            }
            set
            {
                _pan = value;
            }
        }

        public bool Rotate
        {
            get
            {
                return _rotate;
            }
            set
            {
                _rotate = value;
            }
        }

        public bool UserPan
        {
            get
            {
                if (this.currentTool == "USER_PAN")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    selectedTool = "USER_PAN";
            }
        }

        public bool UserRotate
        {
            get
            {
                if (this.currentTool == "USER_ROTATE")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    selectedTool = "USER_ROTATE";
            }
        }

        public void deselectAllTools()
        {
            selectedTool = null;
        }

        public string CurrentAction
        {
            get { return selectedAction; }
            set 
            {
                switch (value)
                {
                    case "MOVE":
                        selectedAction = "MOVE";
                        break;
                    default:
                        selectedAction = null;
                        break;
                }
            }
        }
    }
}
