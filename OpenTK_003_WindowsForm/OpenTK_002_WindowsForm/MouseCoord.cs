using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace OpenTK_002_WindowsForm
{
    public class MouseCoord
    {
        public event EventHandler<ProgressChangedArgs> ProgressChanged;
        private int cnt = 0;

        protected void OnProgressChanged(ProgressChangedArgs e)
        {
            if (ProgressChanged != null)
            {
                ProgressChanged(this, e);
            }
        }

        public void StartWork()
        {
            while (true)
            {
                Thread.Sleep(500);
                cnt++;
                
                OnProgressChanged(new ProgressChangedArgs("Progress Changed: " + cnt.ToString()));
                Thread.Sleep(500);
            }
        }

    }

    public class ProgressChangedArgs : EventArgs
    {
        public string Progress { get; private set; }
        public ProgressChangedArgs(string progress)
        {
            Progress = progress;
        }

    }
}
