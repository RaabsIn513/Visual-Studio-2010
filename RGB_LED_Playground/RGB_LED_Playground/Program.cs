using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace RGB_LED_Playground
{
    public class Program
    {
        static OutputPort lead0 = new OutputPort(Pins.GPIO_PIN_D8, false);
        static OutputPort lead1 = new OutputPort(Pins.GPIO_PIN_D9, false);
        static OutputPort lead2 = new OutputPort(Pins.GPIO_PIN_D10, false);


        public static void Main()
        {
            // write your code here
            while (true)
            {
                Thread.Sleep(500);      // 010 - amber
                lead0.Write(false);
                lead1.Write(true);
                lead2.Write(false);
                Thread.Sleep(500);      // 011 - green
                lead0.Write(false);
                lead1.Write(true);
                lead2.Write(true);
                Thread.Sleep(500);      // 110 - red
                lead0.Write(true);
                lead1.Write(true);
                lead2.Write(false);               
            }
        }

    }
}
