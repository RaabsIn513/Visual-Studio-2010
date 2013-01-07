using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetduinoHostProject
{
    class CommandEventArgs
    {
        public CommandEventArgs()
        {
        }

        public CommandEventArgs(Command command)
        {
            Command = command;

            switch (this.Command.CommandString.ToString())
            {
                case "derp":
                    this.ReturnString = "ACK";
                    break;
                case "herp":
                    this.ReturnString = "ACK";
                    break;
                default:
                    this.ReturnString = "ACK";
                    break;
            }
        }

        public Command Command { get; set; }
        public string ReturnString { get; set; }
    }
}
