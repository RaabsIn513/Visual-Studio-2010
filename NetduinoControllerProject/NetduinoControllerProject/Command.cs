using System;
using Microsoft.SPOT;

namespace NetduinoControllerProject
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

            switch (commandString.ToLower())
            {
                case "green":
                    pinMap = new int[] { 0, 1, 1 };
                    break;
                case "red":
                    pinMap = new int[] { 1, 1, 0 };
                    break;
                case "amber":
                    pinMap = new int[] { 0, 1, 0 };
                    break;
                default:
                    pinMap = new int[] { 0, 1, 1 };
                    break;                

            }
        }

        /// <summary>
        /// Command string, e.g. SetLedState.
        /// </summary>
        public string CommandString { get; set; }

        /// <summary>
        /// Number of arguments needed with this command.
        /// </summary>
        public int ArgumentCount { get; set; }

        public int[] pinMap { get; set; }
        /// <summary>
        /// When a command is received, this property holds the actual argument values.
        /// </summary>
        public object[] Arguments { get; set; }
    }
}
