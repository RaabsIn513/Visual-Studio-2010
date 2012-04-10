using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dirFileHelper
{
    class Program
    {
        public static List<String> dirFiles = new List<string>();
        //public static System.IO.StreamWriter outFile;

        /// <summary>
        /// This program will assist in creating bat file that perform
        /// operations on files in a directory. 
        /// 
        /// For example: If you wanted to perform
        /// 'git add #allFilesInDir#
        /// The argument to this program (when executing from the dir)
        /// would be 'git add'. The output will be a bat file called
        /// 'git_add.bat'
        /// 
        /// Additionally if you perfer to only add certain files, the
        /// argument format would be as follows #COMMAND# #FILES_CONTAINING_IN_FILE_NAME#
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            dirFiles = System.IO.Directory.GetFiles(System.IO.Directory.GetCurrentDirectory()).ToList();

            if (args.Length == 1)       //Create a bat file that has args[0] as the prefix
            {
                args[0] = args[0].TrimEnd();        // trim the white space character if the user entered one. 
                string outFileName = args[0].Replace(' ', '_');
                outFileName = outFileName + ".bat";
                System.IO.StreamWriter outFile = new System.IO.StreamWriter(System.IO.Directory.GetCurrentDirectory() + "\\" + outFileName);
                
                string fileName = null;

                for (int i = 0; i < dirFiles.Count; i++)
                {
                    fileName = dirFiles[i].Substring(dirFiles[i].LastIndexOf("\\") + 1);
                    if (args.Length == 2)
                    {
                        if (fileName.Contains(args[1]))
                                outFile.WriteLine(args[0] + " " + "\"" + fileName + "\"");
                    }
                    else
                        outFile.WriteLine(args[0] + " " + "\"" + fileName + "\"");
                }

                outFile.Close();
            }
            else
            {
                Console.Write("This program will assist in creating bat file that perform" + 
                    "operations on files in a directory. \n" + 
                    "For example: If you wanted to perform 'git add #allFilesInDir# \n" + 
                    "The argument to this program (when executing from the dir) " + 
                    "would be 'git add'. The output will be a bat file called 'git_add.bat'\n" +
                    "Additionally if you perfer to only add certain files, the " +
                    "argument format would be as follows #COMMAND# #FILES_CONTAINING_IN_FILE_NAME#");
            }
        }
    }
}
