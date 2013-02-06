using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using System.Threading;
using Toolbox.NETMF.Hardware;


namespace NetduinoControllerProject
{
    class DevService 
    {
        private Devices currentDevice;
        private Command pendingCmds;
        private Thread serviceDevicesThd = null;
        public delegate void outputStatus(object obj);          // status output to another thread. 
        public delegate void inputCmd(object obj);
        public outputStatus status;
        public inputCmd cmd;
        public static int maxCommandsToService = 5;
        public Command[] commands = new Command[maxCommandsToService];             // accept up to maxCommandsToService commands at a time...
        public bool cancel = false;

        public DevService()
        {
            this.serviceDevicesThd = new Thread(startServiceDevices);
            cmd = acceptCommand;

            for (int i = 0; i < maxCommandsToService; i++)
                commands[i] = null;
        }


        public void acceptCommand(object aCmd)
        {
            for (int i = 0; i < maxCommandsToService; i++)
            {
                if (commands[i] == null)                                    // i'd rather have a push here. 
                {
                    commands[i] = new Command(aCmd.ToString());
                    Debug.Print("Accepted new command to command array");
                }
            }
        }

        public void Start()
        {
            serviceDevicesThd.Start();
            Debug.Print("Started serviceDevicesThd");
        }

        private void startServiceDevices()
        {            
            int servIndex = 0;
            while (!cancel)
            {
                if (commands[0] != null)        // then we've got something to do... i'd rather have a pop here
                {
                    switch (commands[0].Device.ToLower())
                    {
                        case "led":
                            
                            break;

                        default:
                            Debug.Print("Unable to interpret device command recieved in device service thread for device type : " + commands[0].Device.ToUpper() );
                            break;
                    }
                    
                }
                Thread.Sleep(5);
                Debug.Print("In device service thread");
            }
        }
    }
}
