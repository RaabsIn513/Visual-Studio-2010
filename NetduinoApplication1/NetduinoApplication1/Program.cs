﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace NetduinoApplication1
{
    public class Program
    {
        public static OutputPort led = new OutputPort(Pins.ONBOARD_LED, false);
        public static void Main()
        {
            // write your code here

            //while (true)
            //{
            //    led.Write(true);
            //    Thread.Sleep(1000);
            //    led.Write(false);
            //    Thread.Sleep(750);
            //}
        }

    }
}
