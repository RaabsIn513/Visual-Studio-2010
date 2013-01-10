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
        static OutputPort lead0 = new OutputPort(Pins.GPIO_PIN_D8, false);
        static OutputPort lead1 = new OutputPort(Pins.GPIO_PIN_D9, false);
        static OutputPort lead2 = new OutputPort(Pins.GPIO_PIN_D10, false);
        
        public static void Main()
        {
            Server server = new Server(12001);
            Client client = new Client("192.168.1.147", 12000);
            
            server.AllowedCommands.Add(new Command("setled", 1));
            server.serverDel = new Server.externThread(updatePinOutputs);
                       
            server.Start();
            while (true)
            {
                Debug.Print(DateTime.Now.ToLocalTime().ToString() + "\tNetduino running...\t" + server.IPaddress);
                if (client.Send(DateTime.Now.ToLocalTime().ToString() + "\tNetduino running!..\t" + server.IPaddress))
                    Debug.Print("Message Sent and Acknowledged!");
                Thread.Sleep(2500);
            }
        }

        private static void updatePinOutputs(object obj)
        {
            // 
            int[] pins = (int[])obj;
            if (pins[0] == 1)
                lead0.Write(true);
            else
                lead0.Write(false);
            if (pins[1] == 1)
                lead1.Write(true);
            else
                lead1.Write(false);
            if (pins[2] == 1)
                lead2.Write(true);
            else
                lead2.Write(false);

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
