using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.NetduinoPlus;
using Blinq.Netduino.Web;

namespace ServerExample
{
    /// <summary>
    /// Example for using the Blinq.Netduino.Webserver class
    /// </summary>
    public class Program
    {

        static OutputPort onBoardLed = new OutputPort(Pins.ONBOARD_LED, false);

        public static void Main()
        {
            // Instantiate a new web server on port 80.
            WebServer server = new WebServer(80);

            // Add a handler for commands that are received by the server.
            server.CommandReceived += new WebServer.CommandReceivedHandler(server_CommandReceived);

            // Add a command that the server will parse.
            // Any command name is allowed; you will decide what the command does
            // in the CommandReceived handler. The server will only fire CommandReceived
            // for commands that are defined here and that are called with the proper
            // number of arguments.
            // In this example, I define a command 'setLed', which needs one argument (on/off).
            // With this statement, I defined that we can call our server on (for example)
            // http://[server-ip]/setled/on
            // http://[server-ip]/setled/off
            server.AllowedCommands.Add(new WebCommand("setled", 1));

            // Start the server.
            server.Start();

            // Make sure Netduino keeps running.
            while (true)
            {
                Debug.Print("Netduino still running...");
                Thread.Sleep(10000);
            }
        }

        /// <summary>
        /// Handles the CommandReceived event.
        /// </summary>
        private static void server_CommandReceived(object source, WebCommandEventArgs e)
        {

            Debug.Print("Command received:" + e.Command.CommandString);

            switch (e.Command.CommandString)
            {
                case "setled":
                    {
                        // Do you stuff with the command here. Set a led state, return a
                        // sampled value of an analog input, whatever.
                        // Use the ReturnString property to (optionally) return something
                        // to the web user.

                        // Read led state from command and set led state.
                        bool state = ( e.Command.Arguments[0].Equals("on") ? true : false);
                        onBoardLed.Write(state);

                        // Return feedback to web user.
                        e.ReturnString = "<html><body>You called SetLed with argument: " + e.Command.Arguments[0].ToString() + "</body></hmtl>";
                        break;
                    }
            }
        }
    }
}
