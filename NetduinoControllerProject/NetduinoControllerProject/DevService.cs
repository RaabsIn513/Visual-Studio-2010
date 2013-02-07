using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using System.Threading;
using Toolbox.NETMF.Hardware;
using System.Collections;

namespace NetduinoControllerProject
{
    class DevService
    {
        private Thread serviceCtrl;
        private Thread perfCmdThd;
        public Devices[] deviceList = new Devices[4];
        public delegate void outputStatus(object obj);          // status output to another thread. 
        public outputStatus status;
        public delegate void inputCmd(object obj);
        public static inputCmd serviceCmd;
        public static int maxCommandsToService = 5;
        public bool cancel = false;
        private Command cmdInput = new Command();
        private Command currentCmd = new Command();
        private Devices currentDev = new Devices();
        private string result = null;
        private bool iCancel = false;
        static bool iCmdToService = false;

        public int TimeOut_ms { get; set; }

        #region Constructor
        
        public DevService()
        {
            serviceCmd = AcceptCmd;
            this.serviceCtrl = new Thread(service_Thread);
            this.TimeOut_ms = 5000;                         // default value
        }

        #endregion

        /// <summary>
        /// Accepts a command from an external thread. 
        /// </summary>
        /// <param name="aCmd"></param>
        public void AcceptCmd(object aCmd)
        {
            lock (this.cmdInput)
            {       // Possibly add to a Que structure for higher performance/less bottle neck
                this.currentCmd = (Command)aCmd;
                iCmdToService = true;
            }
        }

        /// <summary>
        /// Start the devices service thread.
        /// </summary>
        public void Start()
        {
            this.serviceCtrl.Start();
            this.perfCmdThd = new Thread(PerformActionThread);
        }

        /// <summary>
        /// Device's service therad. Takes commands recieved and determines 
        /// which device is to execute the action included within the command.
        /// (future) get information about result of execution of command 
        /// when applicable. 
        /// </summary>
        private void service_Thread()
        {
            while (!iCancel)
            {
                while (iCmdToService)
                {
                    Debug.Print("Command being serviced here...");
                                        
                    this.currentDev = getDeviceByID(this.cmdInput.DeviceID);        // Determine device
                    this.perfCmdThd.Start();                                        // Perform action for the device

                    //if (this.perfCmdThd.Join(TimeOut_ms))                                 // get results after TimeOut_ms 
                    //{
                    //}

                    iCmdToService = false;
                }
                Thread.Sleep(10);
            }
        }

        private void PerformActionThread()
        {
            string debugStr = this.currentDev.GetType().ToString();
            debugStr = this.currentDev.GetType().ToString().Split('+')[1];
                    
                switch (debugStr)
                {
                    case "RGO_LED":
                        Devices.RGO_LED rgoLED = (Devices.RGO_LED)this.currentDev;
                        if (RGO_LED_ACTION(rgoLED, this.currentCmd))
                            this.result = "LED command success!";
                        break;
                    case "OutputRelay":
                        Devices.OutputRelay outputRELAY = (Devices.OutputRelay)this.currentDev;
                        if (OUTPUT_RELAY_ACTION(outputRELAY, this.currentCmd))
                            this.result = "Output Relay command success!";
                        break;
                    default:
                        break;
                }
        }

        private bool RGO_LED_ACTION(Devices.RGO_LED rgoled, Command cmd )
        {
            // Need ability for controlled flashing of LED via a format found in cmd.Arguments
            switch (cmd.Action.ToLower())
            {
                case "red":
                    rgoled.Red();
                    return true;
                case "green":
                    rgoled.Green();
                    return true;
                case "amber":
                    rgoled.Amber();
                    return true;
                case "off":
                    rgoled.Off();
                    return true;
                default:
                    rgoled.Off();
                    return false;
            }
        }

        private bool OUTPUT_RELAY_ACTION(Devices.OutputRelay outputrelay, Command cmd)
        {
            switch (cmd.Action.ToLower())
            {
                case "on":
                    outputrelay.On();
                    return true;
                case "off":
                    outputrelay.Off();
                    return true;
                default:
                    return false;
            }
        }

        private Devices getDeviceByID(int ID)
        {
            for (int i = 0; i < this.deviceList.Length; i++)
            {
                if (this.deviceList[i].deviceInfo.DeviceID == ID)
                    return this.deviceList[i];              
            }
            return new Devices();
        }
    }
}
