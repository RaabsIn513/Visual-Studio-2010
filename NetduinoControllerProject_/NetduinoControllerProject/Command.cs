using System;
using Microsoft.SPOT;

namespace NetduinoControllerProject
{
    public class Command
    {
        public string Device { get; set; }
        public string Action { get; set; }

        /// <summary>
        /// Instantiates a new web command definition.
        /// </summary>
        /// <param name="commandString">Command string, e.g. SetLedState</param>
        /// <param name="argumentCount">Number of arguments this command needs.</param>
        public Command(string commandString)
        {
            string[] cmdParams = commandString.Split(':');

            if (cmdParams.Length == 2)
            {
                this.Device = cmdParams[0];
                this.Action = cmdParams[1];
            }

            if (cmdParams.Length == 3)
            {
                this.Device = cmdParams[0];
                this.Action = cmdParams[1];
                this.Arguments = cmdParams[2].Split(',');
            }
        }

        public Command()
        {
            this.Device = null;
            this.Action = null;
        }
        
        /// <summary>
        /// Command string, e.g. SetLedState.
        /// </summary>
        public string CommandString { get; set; }

        /// <summary>
        /// When a command is received, this property holds the actual argument values.
        /// </summary>
        public object[] Arguments { get; set; }
    }
}
