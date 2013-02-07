using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using Socket = System.Net.Sockets.Socket;


namespace NetduinoHostProject
{        
    public partial class Form1 : Form
    { 
        public delegate void updateUIDelegate(object obj);
        public updateUIDelegate updateDataGridTable;

        private static string hostIP = "192.168.1.146";
     
        public static Int32 txClientPort = 12001;
        public static Socket txClientSocket;

        public static Int32 rxClientPort = 12000;
        public static Socket rxClientSocket;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            updateDataGridTable = new updateUIDelegate(UpdateDataGridView);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Server server = new Server(12000);
            
            //server.CommandReceived += new Server.CommandReceivedHandler(server_CommandReceived);
            server.AllowedCommands.Add(new Command("setled", 1));
            server.serverDel = new Server.externThread(UpdateDataGridView);

            server.Start();
            lab_statMessage.Text = "Server started";
        }
        
        private static Socket ConnectSocketToClient(Int32 port)
        {
            Socket result = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, port);
            result.Bind(localEndPoint);
            result.Listen(Int32.MaxValue);

            return result.Accept();
        }

        private static Socket ConnectSocketToServer(String server, Int32 port)
        {
            string strHostName;

            // Getting Ip address of local machine...
            // First get the host name of local machine.
            strHostName = Dns.GetHostName();
            //Console.WriteLine("Local Machine's Host Name: " + strHostName);

            //IPHostEntry remoteIP;

            //using host name, get the IP address list..
            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
            IPAddress[] addr = ipEntry.AddressList;

            //int i = 0;
            //while (i < addr.Length)
            //{
            //    Console.WriteLine("IP Address {0}: {1} ", i, addr[i].ToString());
            //    //HostNames
            //    remoteIP = Dns.GetHostEntry((addr[i]));
            //    Console.WriteLine("HostName {0}: {1} ", i, remoteIP.HostName);
            //    i++;
            //}

            //IPHostEntry hostEntry = Dns.GetHostEntry(server);

            IPAddress localNetduino = IPAddress.Parse("192.168.1.146");

            // Create socket and connect to the server's IP address and port
            Socket socket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(new IPEndPoint(localNetduino, port));

            return socket;
        }

        private void UpdateDataGridView(object obj)
        {
            // do we need to switch threads?
            if (InvokeRequired)
            {
                // we then create the delegate again
                // if you've made it global then you won't need to do this
                updateUIDelegate method = new updateUIDelegate(UpdateDataGridView);

                // we then simply invoke it and return
                Invoke(method, obj);
                return;
            }

            // ok so now we're here, this means we're able to update the control
            // so we unbox the object into a string
            string text = (string)obj;
            //text = processRxStr(text);
            // and update
            try
            {
                int rowNum = dgv_clientMess.Rows.Add();
                this.dgv_clientMess.Rows[rowNum].Cells["Time"].Value = DateTime.Now.ToLongTimeString();
                this.dgv_clientMess.Rows[rowNum].Cells["Message"].Value = text;
                this.dgv_clientMess.FirstDisplayedScrollingRowIndex = rowNum;
            }
            catch (Exception ex)
            {
            }

        }
        
        private static bool Send(Socket soc, string message)
        {
            try
            {
                Byte[] bytesToSend = Encoding.UTF8.GetBytes(message);
                soc.Send(bytesToSend, bytesToSend.Length, 0);
                // Check if ACK
                if (soc.Poll(-1, SelectMode.SelectRead))
                {
                    byte[] bytes = new byte[soc.Available];
                    int count = soc.Receive(bytes);
                    string rawData = new string(Encoding.UTF8.GetChars(bytes));
                    if (rawData.ToUpper() == "ACK")
                        return true;
                    else
                        return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        private void btn_SendMessage_Click(object sender, EventArgs e)
        {
            sendToNetduino(this.textBox1.Text.ToString());
            this.textBox1.Clear();
        }

        private void sendToNetduino(string message)
        {
            Socket toRemServ = ConnectSocketToServer("192.168.1.147", 12001);
            if (message != null || message != "")
            {
                int row = dgv_clientMess.Rows.Add();
                this.dgv_clientMess.Rows[row].Cells["Time"].Value = DateTime.Now.ToLongTimeString();
                this.dgv_clientMess.Rows[row].Cells["Message"].Value = "Server Sending: " + message;

                if (Send(toRemServ, message))
                {
                    this.UpdateDataGridView("Server Recieved: ACK");
                }
                else
                {
                    this.UpdateDataGridView("Server Recieved: " + "NOTHING!");
                }
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13) //ENTER
            {
                sendToNetduino(this.textBox1.Text.ToString());

                this.textBox1.Clear();
            }
        }

    }


}

