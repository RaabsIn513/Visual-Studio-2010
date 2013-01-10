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
    class Server
    {
        private bool cancel = false;
        private Thread serverThread = null;
        public delegate void externThread(object obj);
        public externThread serverDel;

        #region Constructors

        /// <summary>
        /// Instantiates a new server.
        /// </summary>
        /// <param name="port">Port number to listen on.</param>
        public Server(int port)
        {
            this.Port = port;
            this.serverThread = new Thread(new ThreadStart(StartServer));

            Console.WriteLine("Control started on " + this.IPaddress.ToString() +  ":" + port.ToString());
        }

        #endregion

        #region Public and private methods
        /// <summary>
        /// Start the multithreaded server.
        /// </summary>
        public void Start()
        {
            // List ethernet interfaces, so we can determine the server's address
            //ListInterfaces();

            // start server
            cancel = false;
            serverThread.Start();
            serverThread.Name = "Server Thread";
            Console.WriteLine("Started server in thread " + serverThread.GetHashCode().ToString());
        }

        /// <summary>
        /// Starts the server.
        /// </summary>
        private void StartServer()
        {
            using (var server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                server.Bind(new IPEndPoint(IPAddress.Any, this.Port));
                server.Listen(10);

                while (!cancel)
                {
                    //Thread.Sleep(1);
                    using (Socket connection = server.Accept())
                    {
                        if (connection.Poll(-1, SelectMode.SelectRead))
                        {
                            // Create buffer and receive raw bytes.
                            byte[] bytes = new byte[connection.Available];
                            int count = connection.Receive(bytes);

                            string rawData = new string(Encoding.UTF8.GetChars(bytes));

                            if (rawData != null)
                            {
                                CommandEventArgs args = new CommandEventArgs(new Command(rawData, 1));
                                if ( args.ReturnString != null )
                                {
                                    byte[] returnBytes = Encoding.UTF8.GetBytes(args.ReturnString);
                                    this.serverDel("Server Recieved: " + rawData);
                                    this.serverDel("Server Sending: " + args.ReturnString);
                                    connection.Send(returnBytes, 0, returnBytes.Length, SocketFlags.None);
                                }
                            }
                        }
                    }
                }
            }
        }

        //private void ListInterfaces()
        //{
        //    //NetworkInterface[] ifaces = NetworkInterface.GetAllNetworkInterfaces();
        //    //Console.WriteLine("Number of Interfaces: " + ifaces.Length.ToString());
        //    //foreach (NetworkInterface iface in ifaces)
        //    //{
        //    //    Console.WriteLine("IP:  " + iface.IPAddress + "/" + iface.SubnetMask);
        //    //}
        //}
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the port the server listens on.
        /// </summary>
        public int Port { get; set; }

        public string IPaddress
        {
            get
            {
                //NetworkInterface NI = NetworkInterface.GetAllNetworkInterfaces()[0];

                return LocalIPAddress();
            }
        }

        private string LocalIPAddress()
        {
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                }
            }
            return localIP;
        }

        /// <summary>
        /// List of commands that can be handled by the server.
        /// Because of the limited support for generics on the .NET micro framework,
        /// this property is implemented as an ArrayList. Make sure you only add
        /// objects of type Blinq.Netduino.WebCommand to this list.
        /// </summary>
        public readonly System.Collections.ArrayList AllowedCommands = new System.Collections.ArrayList();

        #endregion
    }
}
