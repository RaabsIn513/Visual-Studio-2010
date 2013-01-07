using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using Socket = System.Net.Sockets.Socket;
using Microsoft.SPOT.Net.NetworkInformation;

namespace NetduinoApplication1
{
    public class Program
    {
        private static OutputPort led = new OutputPort(Pins.ONBOARD_LED, false);
        
        private static bool toggle = false;
        private static int timer = 750;
        private static string hostIP = "192.168.1.140";
        
        private static bool txHostConnected = false;
        private static Int32 txHostPort = 12000;
        private static Socket txHostCon;

        private static bool rxHostConnected = false;
        private static Int32 rxHostPort = 12001;
        private static Socket rxHostCon;

        public static void Main()
        {
            #region ButtonInterupt
            // write your code here
            //InterruptPort btn = new InterruptPort(Pins.ONBOARD_BTN, false,
            //                            Port.ResistorMode.Disabled,
            //                            Port.InterruptMode.InterruptEdgeLow);

            //btn.OnInterrupt += new NativeEventHandler(btn_OnEdge);
            #endregion

            // Fetches the first network interface
            NetworkInterface NI = NetworkInterface.GetAllNetworkInterfaces()[0];

            //// DHCP
            //NI.EnableDhcp();
            //NI.ReleaseDhcpLease();
            //NI.RenewDhcpLease();


            // Wait for DHCP (on LWIP devices)
            while (true)
            {
                NI = NetworkInterface.GetAllNetworkInterfaces()[0];
                
                if (NI.IPAddress != "0.0.0.0") 
                    break;
                Debug.Print("Waiting for an IP Address...");
                Thread.Sleep(1000);
            }
            Debug.Print(NI.IPAddress.ToString());
            // Static
            NI.EnableStaticIP("192.168.2.75", "255.255.255.0", "192.168.2.1");
            Debug.Print("IPaddress: " + NI.IPAddress.ToString());            

            //Thread txThread = new Thread(SendLoopTesting);
            //txThread.Start();
            Thread rxThread = new Thread(rxWorker);
            rxThread.Start();

            while ( true )
            {
                Thread.Sleep(timer);
                led.Write(rxHostConnected);
            }        
            //txHostCon.Close();
            
        }

        /// <summary>
        /// Creates a socket and uses the socket to connect to the server's IP 
        /// address and port.
        /// </summary>
        /// <param name="server"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        private static Socket ConnectSocketToServer(String server, Int32 port)
        {
            // Get server's IP address.
            IPHostEntry hostEntry = Dns.GetHostEntry(server);

            // Create socket and connect to the server's IP address and port
            Socket socket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(new IPEndPoint(hostEntry.AddressList[0], port));
            
            return socket;
        }

        /// <summary>
        /// role of the server
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        private static Socket ConnectSocketToClient(Int32 port)
        {
            Socket result;
            try
            {
                result = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, port);
                result.Bind(localEndPoint);
                result.Listen(Int32.MaxValue);
                return result;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message.ToString());
                throw ex;
            }
            
        }

        private static void SendLoopTesting()
        {
            ///txHostCon = (Socket)soc;
            try
            {
                txHostCon = ConnectSocketToServer(hostIP, txHostPort);
                txHostConnected = true;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.ToString());
                txHostConnected = false;
            }

            int cnt = 0;
            while (txHostConnected == true)
            {
                if (txHostConnected == true)
                {
                    Thread.Sleep(timer);
                    //led.Write(true);

                    String message = "hello: " + cnt;
                    txHostConnected = Send(txHostCon, message);

                    Thread.Sleep(timer);
                    //led.Write(false);

                    cnt++;
                }
            }
            return;
        }

        private static bool Send(Socket soc, string message)
        {
            try
            {
                Byte[] bytesToSend = Encoding.UTF8.GetBytes(message);
                soc.Send(bytesToSend, bytesToSend.Length, 0);
                return true;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.ToString());
                return false;
            }
        }

        private static void rxWorker()
        {
            try
            {
                rxHostCon = ConnectSocketToClient( rxHostPort );
                rxHostConnected = true;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.ToString());
                rxHostConnected = false;
            }

            while (rxHostConnected)
            {
                Receive(rxHostCon, 30);
            }

            return;
        }
        
        private static void Receive(Socket soc, int timeOutSec)
        {
            const Int32 c_microsecondsPerSecond = 1000000;
            char[] message = null;

            while (rxHostConnected)
            {
                using (Socket clientSocket = soc.Accept())
                {
                    Byte[] buffer = new Byte[1024];
                    if (rxHostCon.Poll(timeOutSec * c_microsecondsPerSecond,
                        SelectMode.SelectRead))
                    {
                        if (rxHostCon.Available == 0)
                            return;

                        // Read the first chunk of the request
                        Int32 bytesRead = clientSocket.Receive(buffer,
                            clientSocket.Available, SocketFlags.None);
                        message = Encoding.UTF8.GetChars(buffer, 0, bytesRead);
                    }
                    else
                        rxHostConnected = false;
                }
            }
            return;
        }

        private static void btn_OnEdge(uint port, uint data, DateTime time)
        {
            uint temp = data;
            if (!txHostConnected)
            {
                try
                {
                    txHostCon = ConnectSocketToServer(hostIP, txHostPort);
                    txHostConnected = true;
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.ToString());
                    txHostConnected = false;
                }
            }
            else
            {
                try
                {
                    txHostCon.Close();
                    txHostConnected = false;
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.ToString());
                    txHostConnected = false;
                }

            }
            //led.Write(txHostConnected);
        }


    }

}
