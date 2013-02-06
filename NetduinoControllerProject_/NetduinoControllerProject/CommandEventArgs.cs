using System;
using Microsoft.SPOT;

namespace NetduinoControllerProject
{
    class CommandEventArgs
    {
        public CommandEventArgs()
        {
        }

        public CommandEventArgs(Command command)
        {
            Command = command;

            this.ReturnString = "Returning this string";
        }

        public Command Command { get; set; }
        public string ReturnString { get; set; }
    }
}
