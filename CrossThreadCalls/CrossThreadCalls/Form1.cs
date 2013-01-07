using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace CrossThreadCalls
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private delegate void ObjectDelegate(object obj);

        private void button1_Click(object sender, EventArgs e)
        {
            // first thing we do is create a delegate pointing to update method
            ObjectDelegate del = new ObjectDelegate(UpdateTextBox);

            // then simply enough, we invoke it
            //del.Invoke("Hello from button1_Click!");

            // if we wanted to create a thread to use it, we'd use a
            // params threadstart and pass the delegate as an object
            Thread th = new Thread(new ParameterizedThreadStart(WorkThread));
            th.Start(del);
        }

        private void WorkThread(object obj)
        {
            // we would then unbox the obj into the delegate
            ObjectDelegate del = (ObjectDelegate)obj;

            // and invoke it like before
            del.Invoke("Hello from WorkThread!");
        }

        private void UpdateTextBox(object obj)
        {
            // do we need to switch threads?
            if (InvokeRequired)
            {
                // we then create the delegate again
                // if you've made it global then you won't need to do this
                ObjectDelegate method = new ObjectDelegate(UpdateTextBox);

                // we then simply invoke it and return
                Invoke(method, obj);
                return;
            }

            // ok so now we're here, this means we're able to update the control
            // so we unbox the object into a string
            string text = (string)obj;

            // and update
            textBox1.Text += text + "\r\n";
        }
    }

}
