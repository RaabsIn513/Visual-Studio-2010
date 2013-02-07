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
        public Devices[] deviceList = new Devices[4];
        public delegate void outputStatus(object obj);          // status output to another thread. 
        public outputStatus status;
        public delegate void inputCmd(object obj);
        public static inputCmd serviceCmd;
        public static int maxCommandsToService = 5;
        public bool cancel = false;
        private Command cmdInput = new Command();
        private bool iCancel = false;
        static bool iCmdToService = false;

        #region Constructor
        
        public DevService()
        {
            serviceCmd = AcceptCmd;
            this.serviceCtrl = new Thread(service_Thread);
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
                this.cmdInput = (Command)aCmd;
                iCmdToService = true;
            }
        }

        /// <summary>
        /// Start the devices service thread.
        /// </summary>
        public void Start()
        {
            this.serviceCtrl.Start();
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
                    // Determine device
                    Devices debugDev = getDeviceByID(this.cmdInput.DeviceID);

                    string debugStr = debugDev.GetType().ToString();
                    debugStr = debugDev.GetType().ToString().Split( '+' )[1];
                    
                    switch (debugStr)
                    {
                        case "RGO_LED":
                            Devices.RGO_LED temp = (Devices.RGO_LED)debugDev;
                            RGO_LED_ACTION(temp, this.cmdInput);
                            break;
                        default:
                            break;
                    }

                    iCmdToService = false;
                }
                Thread.Sleep(10);
            }
        }

        private void RGO_LED_ACTION(Devices.RGO_LED rgoled, Command cmd )
        {
            switch (cmd.Action.ToLower())
            {
                case "red":
                    rgoled.Red();
                    break;
                case "green":
                    rgoled.Green();
                    break;
                case "amber":
                    rgoled.Amber();
                    break;
                default:
                    rgoled.Off();
                    break;
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
