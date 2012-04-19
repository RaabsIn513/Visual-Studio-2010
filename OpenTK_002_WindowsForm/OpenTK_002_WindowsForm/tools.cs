using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenTK_002_WindowsForm
{
    class tools
    {
        public List<KeyValuePair<string, bool>> tool_sel = new List<KeyValuePair<string, bool>>();

        public tools()
        {
            tool_sel.Add(new KeyValuePair<string, bool>("USER_LINE", false));
            tool_sel.Add(new KeyValuePair<string, bool>("USER_LOOPLINE", false));
            tool_sel.Add(new KeyValuePair<string, bool>("USER_POLY", false));
            tool_sel.Add(new KeyValuePair<string, bool>("USER_QUAD", false));
            tool_sel.Add(new KeyValuePair<string, bool>("USER_POINT", false));
            tool_sel.Add(new KeyValuePair<string, bool>("USER_CIRCLE", false));
        }

        public static string fuck
        {
            get { return "FUCK!"; }
        }

        public bool UserLine
        {
            get { return getByKey("USER_LINE").Value; }
          //set { tool_sel[0].Value = value; }
        }

        private KeyValuePair<string, bool> getByKey(string Key)
        {
            for (int i = 0; i < tool_sel.Count(); i++)
            {
                if (tool_sel[i].Key == Key)
                    return tool_sel[i];
            }
            return new KeyValuePair<string, bool>();
        }
    }
}
