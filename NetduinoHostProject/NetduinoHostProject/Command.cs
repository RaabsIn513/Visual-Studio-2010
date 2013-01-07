using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetduinoHostProject
{
    class Command
    {
        /// <summary>
        /// Instantiates a new web command definition.
        /// </summary>
        /// <param name="commandString">Command string, e.g. SetLedState</param>
        /// <param name="argumentCount">Number of arguments this command needs.</param>
        public Command(string commandString, int argumentCount)
        {
            CommandString = commandString;
            ArgumentCount = argumentCount;
        }

        /// <summary>
        /// Command string, e.g. SetLedState.
        /// </summary>
        public string CommandString { get; set; }

        /// <summary>
        /// Number of arguments needed with this command.
        /// </summary>
        public int ArgumentCount { get; set; }

        /// <summary>
        /// When a command is received, this property holds the actual argument values.
        /// </summary>
        public object[] Arguments { get; set; }
    }
}
