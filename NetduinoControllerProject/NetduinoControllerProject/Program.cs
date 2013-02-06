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

        //private static DevService devService = new DevService();
        private static Devices ctrlDevices = new Devices();
        private static Devices.RGO_LED led = new Devices.RGO_LED(
            new OutputPort(Pins.GPIO_PIN_D8, false),
            new OutputPort(Pins.GPIO_PIN_D9, false),
            new OutputPort(Pins.GPIO_PIN_D10, false) );
   
        private static int servListenPort = 12001;
        //private static Devices.LCD_HD44780 = new Devices.LCD_HD44780(
        //);

        private void initDevices()
        {
            led.deviceInfo.DisplayText = "Red, Green, Orange LED";
            led.deviceInfo.Name = "RGO_LED";
            
        }
        
        public void initComm()
        {
        }
        
        public static void Main()
        {
            Server server = new Server(servListenPort);
            Client client = new Client("192.168.1.147", 12000);

            //server.serverDel = new Server.externThread(updatePinOutputs);
            //server.serverDel = new Server.externThread(devService.cmd);         // set commands recieved by the server to be handled by devService
            server.serverDel = new Server.externThread(Devices.serviceCmd);

            ctrlDevices.Start();                                   // start the thread that handles commands. 
            Debug.Print("Started serviceDevicesThread");
                       
            server.Start();                                                     // start the thread that recieves commands
            Debug.Print("Server started. Listening on Port: " + servListenPort.ToString());
           

            //char aChar = 'A';
            //byte test = (byte)aChar;
            //byte addr1 = 1;
            //byte addr2 = 4;
            //byte addr3 = 8;
            //byte addr4 = 16;

            //bool debug0 = ((test & addr1) > 0);
            //bool debug1 = ((test & addr2) > 0);

            while (true)
            {
                Debug.Print(DateTime.Now.ToLocalTime().ToString() + "   Netduino running... " + server.IPaddress);
                if (client.Send(DateTime.Now.ToLocalTime().ToString() + "- Netduino running(" + server.IPaddress + ")"))
                {
                    Debug.Print("Message Sent and Acknowledged!");
                    led.Amber();
                    Thread.Sleep(500);
                }
                led.Green();
                Thread.Sleep(2500);
                
            }
        }


        private static void updatePinOutputs(object obj)
        {
            //Command rxCMD = (Command)obj;
            //led.setCmd((object)rxCMD);
                    

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
