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
    public class Devices
    {
        public struct DeviceInfo
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public int DeviceID { get; set; }
            public string Manufacturer { get; set; }
            public string PartNumber { get; set; }
            // other general information could go here;        
        };
        public DeviceInfo deviceInfo;         

    public class RGO_LED : Devices
    {
        private bool isSet = false;
        private led_state state = led_state.OFF;        // initial state
        private led_state prevState = led_state.GREEN;
        private string prevAction = "";
        private Thread RGO_LED_THREAD = null;
        public bool stopRequest = false;
        private Command iCmd = new Command("LED:off");
        //public DeviceInfo deviceInfo;

        enum led_state
        {
            OFF,
            AMBER,
            GREEN,
            RED,
            FLASH
        }

        public RGO_LED(string Name, string Description, int DeviceID, OutputPort pin0, OutputPort pin1, OutputPort pin2)
        {
            this.Pin0 = pin0;
            this.Pin1 = pin1;
            this.Pin2 = pin2;
            this.deviceInfo.Name = Name;
            this.deviceInfo.Description = Description;
            this.deviceInfo.DeviceID = DeviceID;

            isSet = true;

            this.stopRequest = false;
            this.RGO_LED_THREAD = new Thread(new ThreadStart(Run));
            this.RGO_LED_THREAD.Start();
        }

        // Sets the command
        private void cmd(object obj)
        {
            Command newCmd = (Command)obj;
            if ( newCmd.Device == "LED" )
            {
                lock (this)
                {
                    this.iCmd = newCmd;
                }
                return;
            }
        }
            
        /// <summary>
        /// Run Thread. Runs the command
        /// </summary>
        private void Run()
        {
            while (!stopRequest)
            {
                if (this.iCmd.Action != this.prevAction)
                {
                    switch (this.iCmd.Action)
                    {
                        case "green":
                            this.Green();
                            break;
                        case "red":
                            this.Red();
                            break;
                        case "amber":
                            this.Amber();
                            break;
                        default:
                            this.Off();
                            break;
                    }
                    this.prevAction = this.iCmd.Action;
                }
                if (this.iCmd.Action.ToLower() == "flash")
                {
                    this.Flash(250, 500);
                }
                Thread.Sleep(1);
            }                   
        }

        public void Flash(int timeOn, int timeOff)
        {
            // blink between prev state and state
            if (isSet)
            {
                if (this.state == led_state.OFF)
                {
                    switch (this.prevState)
                    {
                        case led_state.AMBER:
                            this.Amber();
                            Thread.Sleep(timeOn);
                            break;
                        case led_state.GREEN:
                            this.Green();
                            Thread.Sleep(timeOn);
                            break;
                        case led_state.RED:
                            this.Red();
                            Thread.Sleep(timeOn);
                            break;
                        default:
                            this.Off();
                            Thread.Sleep(timeOff);
                            break;
                    }
                }
                else
                {
                    this.Off();
                    Thread.Sleep(timeOff);
                }
            }
            else
            {
                Debug.Print("The led '" + this.deviceInfo.Name + "' is not configured correctly and can not be used");
            }
        }

        public void Off()
        {
            if (isSet)
            {
                this.prevState = this.state;
                this.Pin0.Write(false);
                this.Pin1.Write(false);
                this.Pin2.Write(false);
                this.state = led_state.OFF;
            }
            else
            {
                Debug.Print("The led '" + this.deviceInfo.Name + "' is not configured correctly and can not be used");
            }
        }
            
        public void Green()
        {
            if (isSet)
            {
                this.prevState = this.state;
                this.Pin0.Write(false);
                this.Pin1.Write(true);
                this.Pin2.Write(true);
                this.state = led_state.GREEN;
            }
            else
            {
                Debug.Print("The led '" + this.deviceInfo.Name + "' is not configured correctly and can not be used");
            }
        }

        public void Red()
        {
            if (isSet)
            {
                this.prevState = this.state;
                this.Pin0.Write(true);
                this.Pin1.Write(true);
                this.Pin2.Write(false);
                this.state = led_state.RED;
            }
            else
            {
                Debug.Print("The led '" + this.deviceInfo.Name + "' is not configured correctly and can not be used");
            }
        }

        public void Amber()
        {
            if (isSet)
            {
                this.prevState = this.state;
                this.Pin0.Write(false);
                this.Pin1.Write(true);
                this.Pin2.Write(false);
                this.state = led_state.AMBER;
            }
            else
            {
                Debug.Print("The led '" + this.deviceInfo.Name + "' is not configured correctly and can not be used");
            }
        }

        private OutputPort Pin0 { get; set; }
        private OutputPort Pin1 { get; set; }
        private OutputPort Pin2 { get; set; }
    }
    
    public class OutputRelay : Devices
    {
        private bool isSet = false;
        private bool pinState = false;
        
        public OutputRelay(string Name, string Description, int DeviceID, OutputPort drivePin)
        {
            this.drivePin = drivePin;
            this.deviceInfo.Name = Name;
            this.deviceInfo.Description = Description;
            this.deviceInfo.DeviceID = DeviceID;

            isSet = true;
        }

        public void On()
        {
            drivePin.Write(true);
            pinState = true;
        }

        public void Off()
        {
            drivePin.Write(false);
            pinState = false;
        }

        private OutputPort drivePin { get; set; }
    }

    public class LCD_HD44780 : Devices
    {
        private bool isSet = false;
        private Thread LCD_HD44780_THREAD = null;
            
        public bool stopRequest = false;
        private Command iCmd = new Command("LCD:reset");
        private string prevAction = "";
        enum lcd_state
        {
            OFF,
            ON,
        }

        public LCD_HD44780(OutputPort regSelect, OutputPort readWrite, OutputPort enable,
            OutputPort data0, OutputPort data1, OutputPort data2, OutputPort data3, OutputPort data4,
            OutputPort data5, OutputPort data6, OutputPort data7)
        {
            this.RegSelect = regSelect;
            this.ReadWrite = readWrite;
            this.Enable = enable;
            this.D0 = data0;
            this.D1 = data1;
            this.D2 = data2;
            this.D3 = data3;
            this.D4 = data4;
            this.D5 = data5;
            this.D6 = data6;
            this.D7 = data7;

            isSet = true;

            this.stopRequest = false;
            this.LCD_HD44780_THREAD = new Thread(new ThreadStart(Run));
            this.LCD_HD44780_THREAD.Start();
        }

        // Sets the command
        private void cmd(object obj)
        {
            Command newCmd = (Command)obj;
            if ( newCmd.Device == "LCD" )
            {
                lock (this)
                {
                    this.iCmd = newCmd;
                }
                return;
            }
        }
            
        /// <summary>
        /// Run Thread. Runs the command
        /// </summary>
        private void Run()
        {
            while (!stopRequest)
            {
                if (this.iCmd.Action != this.prevAction)
                {
                }
            }                   
        }

        public void Write(string toWrite)
        {
            byte aChar = new byte();
            if (toWrite.Length <= 16)
            {
                for (int i = 0; i < toWrite.Length; i++)
                {
                    WriteCharacter(toWrite[i]);
                }
            }
        }

        public void WriteCharacter( char aChar )
        {
            bool[] values = charToBitArray(aChar);
            this.D0.Write(values[0]);
            this.D1.Write(values[1]);
            this.D2.Write(values[2]);
            this.D3.Write(values[3]);
            this.D4.Write(values[4]);
            this.D5.Write(values[5]);
            this.D6.Write(values[6]);
            this.D7.Write(values[7]);
        }

        private bool[] charToBitArray(char aChar)
        {
            byte test = (byte)aChar;
            bool[] result = new bool[8];
            byte[] addresses = { 1, 2, 4, 8, 16, 32, 64, 128 };
                
            for( int i = 0; i < 8; i++ )
            {
                result[i] = (( test & addresses[i]) > 0 );
            }

            return result;
        }

        public void cursorProperties(bool displayOn, bool blinkCursor, bool cursorLine)
        {
            this.AllPinsLow();

            this.D0.Write(blinkCursor);
            this.D1.Write(cursorLine);
            this.D2.Write(displayOn);
            this.D3.Write(true);
            Thread.Sleep(5);
            this.Enable.Write(true);
            Thread.Sleep(5);
            this.Enable.Write(false); 
        }

        public void entryMode( bool scrollDisplay, bool incCursorPos)
        {
            this.AllPinsLow();

            this.D0.Write(scrollDisplay);
            this.D1.Write(incCursorPos);
            this.D2.Write(true);
            Thread.Sleep(5);
            this.Enable.Write(true);
            Thread.Sleep(5);
            this.Enable.Write(false);  
        }

        public void clearHome()
        {
            this.AllPinsLow();

            this.D1.Write(true);
            Thread.Sleep(5);
            this.Enable.Write(true);
            Thread.Sleep(5);
            this.Enable.Write(false);                
        }

        public void cursorRight()
        {
            this.AllPinsLow();

            this.D4.Write(true);
            this.D2.Write(true);
            Thread.Sleep(5);
            this.Enable.Write(true);
            Thread.Sleep(5);
            this.Enable.Write(false);
        }

        public void cursorLeft()
        {
            this.AllPinsLow();

            this.D4.Write(true);
            this.D2.Write(false);
            Thread.Sleep(5);
            this.Enable.Write(true);
            Thread.Sleep(5);
            this.Enable.Write(false);
        }

        /// <summary>
        /// (init)Sets default entry mode, cursor blink, cursor home
        /// </summary>
        public void Reset()
        {
            this.AllPinsLow();

            this.D0.Write(true);
            Thread.Sleep(5);
            this.Enable.Write(true);
            Thread.Sleep(5);
            this.Enable.Write(false);

            this.D1.Write(true);
            Thread.Sleep(5);
            this.Enable.Write(true);
            Thread.Sleep(5);
            this.Enable.Write(false);

            this.D2.Write(true);
            Thread.Sleep(5);
            this.Enable.Write(true);
            Thread.Sleep(5);
            this.Enable.Write(false);

            this.D3.Write(true);
            Thread.Sleep(5);
            this.Enable.Write(true);
            Thread.Sleep(5);
            this.Enable.Write(false);

        }

        private void AllPinsLow()
        {
            this.ReadWrite.Write(false);
            this.RegSelect.Write(false);
            this.Enable.Write(false);
            this.D0.Write(false);
            this.D1.Write(false);
            this.D2.Write(false);
            this.D3.Write(false);
            this.D4.Write(false);
            this.D5.Write(false);
            this.D6.Write(false);
            this.D7.Write(false);
        }

        private OutputPort RegSelect { get; set; }
        private OutputPort ReadWrite { get; set; }
        private OutputPort Enable { get; set; }
        private OutputPort D0 { get; set; }
        private OutputPort D1 { get; set; }
        private OutputPort D2 { get; set; }
        private OutputPort D3 { get; set; }
        private OutputPort D4 { get; set; }
        private OutputPort D5 { get; set; }
        private OutputPort D6 { get; set; }
        private OutputPort D7 { get; set; }
    }

    public class STD_LED : Devices
    {

    }
    
    }
}

