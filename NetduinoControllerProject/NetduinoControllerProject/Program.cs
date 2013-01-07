using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace NetduinoControllerProject
{
    public class Program
    {
        static OutputPort onBoardLed = new OutputPort(Pins.ONBOARD_LED, false);
        public delegate void UpdateDebugOutputDelegate(object obj);
        public UpdateDebugOutputDelegate del;

        public static void Main()
        {
            Server server = new Server(12001);
            Client client = new Client("192.168.1.147", 12000);
            
            server.AllowedCommands.Add(new Command("setled", 1));
            server.serverDel = new Server.externThread(UpdateDebugOutput);
                       
            server.Start();
            while (true)
            {
                Debug.Print(DateTime.Now.ToLocalTime().ToString() + "\tNetduino running...\t" + server.IPaddress);
                if (client.Send(DateTime.Now.ToLocalTime().ToString() + "\tNetduino running!..\t" + server.IPaddress))
                    Debug.Print("Message Sent and Acknowledged!");
                Thread.Sleep(5000);
            }
        }

        private static void UpdateDebugOutput(object obj)
        {
            // ok so now we're here, this means we're able to update the control
            // so we unbox the object into a string
            string text = (string)obj;
            Debug.Print(text);
        }
    }
}
