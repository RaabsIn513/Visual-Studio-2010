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

        private static Devices.RGO_LED led = new Devices.RGO_LED(
            new OutputPort(Pins.GPIO_PIN_D8, false),
            new OutputPort(Pins.GPIO_PIN_D9, false),
            new OutputPort(Pins.GPIO_PIN_D10, false) );

        //private static Devices.LCD_HD44780 = new Devices.LCD_HD44780(
        //);

        public static void Main()
        {
            Server server = new Server(12001);
            Client client = new Client("192.168.1.147", 12000);
            
            server.serverDel = new Server.externThread(updatePinOutputs);
                       
            server.Start();



            while (true)
            {
                Debug.Print(DateTime.Now.ToLocalTime().ToString() + "   Netduino running... " + server.IPaddress);
                if (client.Send(DateTime.Now.ToLocalTime().ToString() + "- Netduino running(" + server.IPaddress + ")"))
                    Debug.Print("Message Sent and Acknowledged!");
                Thread.Sleep(2500);
            }
        }


        private static void updatePinOutputs(object obj)
        {
            Command rxCMD = (Command)obj;

            led.setCmd((object)rxCMD);

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
