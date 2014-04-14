using System;
using System.Diagnostics;
using System.Linq;

namespace CentralNetSend
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length != 0)
            {
                if(args[0].ToLower() == "send" && args.Count() == 3)
                {
                    string computerName = args[1];
                    string message = args[2];

                    //MAndar mensaje al servidor
                }
                else
                {
                    var process = new Process();
                    process.StartInfo.FileName = "net.exe";
                    process.StartInfo.Arguments = String.Join(" ", args);

                    // set up output redirection
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.StartInfo.CreateNoWindow = true;
                    process.StartInfo.UseShellExecute = false;
                    process.EnableRaisingEvents = true;

                    // see below for output handler
                    process.ErrorDataReceived += ShowInfoOfNet;
                    process.OutputDataReceived += ShowInfoOfNet;

                    process.Start();

                    process.BeginErrorReadLine();
                    process.BeginOutputReadLine();

                    process.WaitForExit();
                }
            }
        }

        static void ShowInfoOfNet(object sender, DataReceivedEventArgs e)
        {
            Console.Write(e.Data + Environment.NewLine);
        }
    }
}
