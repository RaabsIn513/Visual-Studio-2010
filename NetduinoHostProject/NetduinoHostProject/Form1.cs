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
        public updateUIDelegate del;

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
            dgv_clientMess.Columns.Add("Time", "Time");
            dgv_clientMess.Columns.Add("Message", "Message");
            del = new updateUIDelegate(UpdateDataGridView);
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

        private void button2_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// role of the server
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
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
            Console.WriteLine("Local Machine's Host Name: " + strHostName);

            IPHostEntry remoteIP;

            //using host name, get the IP address list..
            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
            IPAddress[] addr = ipEntry.AddressList;

            int i = 0;
            while (i < addr.Length)
            {
                Console.WriteLine("IP Address {0}: {1} ", i, addr[i].ToString());
                //HostNames
                remoteIP = Dns.GetHostEntry((addr[i]));
                Console.WriteLine("HostName {0}: {1} ", i, remoteIP.HostName);
                i++;
            }

            //IPHostEntry hostEntry = Dns.GetHostEntry(server);

            IPAddress localNetduino = IPAddress.Parse("192.168.1.146");

            // Create socket and connect to the server's IP address and port
            Socket socket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(new IPEndPoint(localNetduino, port));

            return socket;
        }

        //private void txWorkThread(object obj)
        //{
        //    txClientSocket = ConnectSocketToServer(hostIP, txClientPort);

        //    while (txClientSocket.Connected)
        //    {
        //        // TODO: have method/delegate to when user sends something?
        //        if (Send(txClientSocket, "derp") == true)
        //        {
        //            Console.WriteLine("Successfully sent message");
        //            Thread.Sleep(1500);
        //        }
        //        else
        //        {
        //            MessageBox.Show("Can't transmit to client socket...");
        //            return;
        //        }
        //    }
        //    return;
        //}

        //private void rxWorkThread(object obj)
        //{
        //    rxClientSocket = ConnectSocketToClient(rxClientPort);       // Accept on rxClientPort

        //    updateUIDelegate del = (updateUIDelegate)obj;
        //    string message = null;
        //    const Int32 c_microsecondsPerSecond = 1000000;

        //    // 'using' ensures that the client's socket gets closed.
        //    using (rxClientSocket)
        //    {
        //        while (rxClientSocket.Connected)
        //        {
        //            // Wait for the client request to start to arrive.
        //            Byte[] buffer = new Byte[1024];
        //            if (rxClientSocket.Poll(5 * c_microsecondsPerSecond,
        //                SelectMode.SelectRead))
        //            {
        //                // If 0 bytes in buffer, then the connection has been closed, 
        //                // reset, or terminated.
        //                if (rxClientSocket.Available == 0)
        //                    return;

        //                // Read the first chunk of the request
        //                Int32 bytesRead = rxClientSocket.Receive(buffer,
        //                    rxClientSocket.Available, SocketFlags.None);
        //                message = Encoding.UTF8.GetString(buffer, 0, bytesRead);

        //                del.Invoke(message);
        //            }
        //        }
        //    }
        //    return;
        //}

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
            int rowNum = dgv_clientMess.Rows.Add();
            dgv_clientMess.Rows[rowNum].Cells["Time"].Value = DateTime.Now.ToLongTimeString();
            dgv_clientMess.Rows[rowNum].Cells["Message"].Value = text;
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
            Socket toRemServ = ConnectSocketToServer("192.168.1.147", 12001);
            if (textBox1.Text != null || textBox1.Text != "")
            {
                Send(toRemServ, textBox1.Text.ToString());
                int row = dgv_clientMess.Rows.Add();
                dgv_clientMess.Rows[row].Cells["Time"].Value = DateTime.Now.ToLongTimeString();
                dgv_clientMess.Rows[row].Cells["Message"].Value = "Server Sending: " + textBox1.Text.ToString();
                textBox1.Clear();
            }
        }


    }


}

