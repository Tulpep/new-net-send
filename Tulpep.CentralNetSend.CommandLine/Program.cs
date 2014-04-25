using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;

namespace CentralNetSend
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            if(args.Length != 0)
            {

                if(args[0].ToLower() == "send" && args.Count() == 3)
                {

                    string computerName = args[1];
                    string message = args[2];
                    string urlOfServer;

                    using(RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
                    {
                        using(RegistryKey centralNetSendKey = hklm.OpenSubKey(@"SOFTWARE\Tulpep\CentralNetSend")) 
	                    {
		                    urlOfServer = centralNetSendKey.GetValue("APIServer").ToString();
	                    } 

                    }
                    
                

                    try
                    {
                        var httpclient = new HttpClient(new HttpClientHandler { UseDefaultCredentials = true }) { BaseAddress = new Uri(urlOfServer) };

                        var response = httpclient.PostAsJsonAsync("/api/Message?computerName=" + computerName + "&message=" + message, "").Result;
                    }
                    catch 
                    {
                        Console.WriteLine("Cannot contact Central Net Service by Tulpep at " + urlOfServer);
                        return 1;
                    }



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

            return 0;
        }

        static void ShowInfoOfNet(object sender, DataReceivedEventArgs e)
        {
            Console.Write(e.Data + Environment.NewLine);
        }
    }
}
