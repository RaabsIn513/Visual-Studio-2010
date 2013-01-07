using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Net.NetworkInformation;


namespace NetduinoControllerProject
{
    class Server
    {
        private bool cancel = false;
        private Thread serverThread = null;
        public delegate void externThread(object obj);
        public externThread serverDel;

        #region Constructors

        /// <summary>
        /// Instantiates a new webserver.
        /// </summary>
        /// <param name="port">Port number to listen on.</param>
        public Server(int port)
        {
            this.Port = port;
            this.serverThread = new Thread(StartServer);

            NetworkInterface ni = NetworkInterface.GetAllNetworkInterfaces()[0];

            while (ni.IPAddress.ToString() == "0.0.0.0")
            {
                ni.RenewDhcpLease();

                Thread.Sleep(1000);
            }

            Debug.Print("Control started on " + ni.IPAddress + ":" + port.ToString());
        }

        #endregion
        
        #region Public and private methods
        /// <summary>
        /// Start the multithreaded server.
        /// </summary>
        public void Start()
        {
            // List ethernet interfaces, so we can determine the server's address
            ListInterfaces();

            // start server
            cancel = false;
            serverThread.Start();
            Debug.Print("Started server in thread " + serverThread.GetHashCode().ToString());
        }

        /// <summary>
        /// Starts the server.
        /// </summary>
        private void StartServer()
        {
            using (var server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                server.Bind(new IPEndPoint(IPAddress.Any, this.Port));
                server.Listen(1);

                while (!cancel)
                {
                    using (Socket connection = server.Accept())
                    {
                        if (connection.Poll(-1, SelectMode.SelectRead))
                        {
                            // Create buffer and receive raw bytes.
                            byte[] bytes = new byte[connection.Available];
                            int count = connection.Receive(bytes);

                            // Convert to string, will include HTTP headers.
                            string rawData = new string(Encoding.UTF8.GetChars(bytes));
                            
                            Byte[] bytesToSend;
                            switch (rawData)
                            {
                                case "derp":
                                    bytesToSend = Encoding.UTF8.GetBytes("ACK");
                                    this.serverDel("Server Recieved: " + rawData);
                                    this.serverDel("Server Sending: " + "ACK");
                                    connection.Send(bytesToSend, bytesToSend.Length, 0);
                                    break;
                                default:
                                    bytesToSend = Encoding.UTF8.GetBytes("ACK");
                                    this.serverDel("Server Recieved: " + rawData);
                                    this.serverDel("Server Sending: " + "ACK");
                                    connection.Send(bytesToSend, bytesToSend.Length, 0);
                                    break;
                            }
                        }
                    }
                }
            }
        }

        private void ListInterfaces()
        {
            NetworkInterface[] ifaces = NetworkInterface.GetAllNetworkInterfaces();
            Debug.Print("Number of Interfaces: " + ifaces.Length.ToString());
            foreach (NetworkInterface iface in ifaces)
            {
                Debug.Print("IP:  " + iface.IPAddress + "/" + iface.SubnetMask);
            }
        }
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
                NetworkInterface NI = NetworkInterface.GetAllNetworkInterfaces()[0];
                return NI.IPAddress.ToString();
            }
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
