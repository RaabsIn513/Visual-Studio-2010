using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Net.NetworkInformation;


namespace NetduinoControllerProject
{
    class Client
    {
        //private bool cancel = false;
        //private Thread clientThread = null;
        private static Socket client;
                
        #region Constructors

        public Client(string server, int port)
        {
            this.Port = port;
            this.Server = server;

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

        public bool Send(string message)
        {
            // TODO: Consider making this its own thread just to send and ACK message then terminate the thread. 
            try
            {
                using (client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    // TODO: try catch etc.
                    try
                    {
                        client.SendTimeout = 5000;
                        IPAddress tServer = IPAddress.Parse(this.Server);
                        Debug.Print("Before Connect");
                        client.Connect(new IPEndPoint(tServer, this.Port));
                        Debug.Print("After Connect");
                        Byte[] bytesToSend2 = Encoding.UTF8.GetBytes(message);
                        client.SendTo(bytesToSend2, new IPEndPoint(tServer, this.Port));
                    }
                    catch (Exception ex)
                    {
                        Debug.Print(ex.ToString());
                    }

                    //Byte[] bytesToSend = Encoding.UTF8.GetBytes(message);
                    //client.Send(bytesToSend, bytesToSend.Length, 0);
                    // Check if ACK
                    if (client.Poll(-1, SelectMode.SelectRead))
                    {
                        byte[] bytes = new byte[client.Available];
                        int count = client.Receive(bytes);
                        string rawData = new string(Encoding.UTF8.GetChars(bytes));
                        if (rawData == string.Empty || rawData == null)
                            return false;
                        if (rawData.ToUpper() == "ACK")
                            return true;
                        else
                            return false;
                    }
                    else
                        return false;

                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.ToString());
                return false;
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

        public string Server { get; set; }

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
