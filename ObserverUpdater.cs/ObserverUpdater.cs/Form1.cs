using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace ObserverUpdater.cs
{
    public partial class Form1 : Form
    {
        Worker worker;

        Thread workerThread;

        public Form1()
        {
            InitializeComponent();

            worker = new Worker();
            worker.ProgressChanged += new EventHandler<ProgressChangedArgs>(OnWorkerProgressChanged);
            workerThread = new Thread(new ThreadStart(worker.StartWork));
            
            workerThread.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void OnWorkerProgressChanged(object sender, ProgressChangedArgs e)
        {
            //cross thread - so you don't get the cross theading exception
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    OnWorkerProgressChanged(sender, e);
                    
                });
                return;
            } 

            //change control
            this.label1.Text = e.Progress;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // start count
            worker.count = true;
            //workerThread.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            worker.count = false;
            //workerThread.Abort();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }
    }

    public class Worker
    {
        public event EventHandler<ProgressChangedArgs> ProgressChanged;
        private int cnt = 0;
        static bool doCount = true;
        protected void OnProgressChanged(ProgressChangedArgs e)
        {
            if(ProgressChanged!=null)
            {
                ProgressChanged(this,e);
            }
        }

        public bool count
        {
            get { return doCount; }
            set { doCount = value; }
        }

        public void StartWork()
        {
            while (doCount)
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
        public string Progress {get;private set;}
        public ProgressChangedArgs(string progress)
        {
            Progress = progress;
        }

    }
}
