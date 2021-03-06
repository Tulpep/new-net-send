﻿using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Web.Http;
using System.Text;
using System.IO;

namespace Tulpep.CentralNetSend.WebApiService
{
    [Authorize]
    public class MessageController : ApiController
    {
        [DllImport("kernel32.dll", SetLastError=true)]
        public static extern int Wow64DisableWow64FsRedirection(ref IntPtr ptr);

        public HttpResponseMessage PostMessage(string computerName, string message)
        {

            string pathOfLog = (Environment.SystemDirectory) + @"\Log-Central-Message.txt";

            IntPtr val = new IntPtr();

            Wow64DisableWow64FsRedirection(ref val);
            var process = new Process();
            process.StartInfo.FileName = "msg.exe";
            process.StartInfo.Arguments = "* /server:" + computerName + " " +  message;

            // set up output redirection
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            process.WaitForExit();
            string errors = process.StandardError.ReadToEnd();


            if(String.IsNullOrWhiteSpace(errors))
            {
                File.AppendAllText(pathOfLog, DateTime.Now + "\t" + User.Identity.Name + "\t" + computerName + "\t" + message + "\t OK" + Environment.NewLine);
                return Request.CreateResponse<string>(HttpStatusCode.OK, "Sent");
            }
            else
            {
                File.AppendAllText(pathOfLog, DateTime.Now + "\t" + User.Identity.Name + "\t" + computerName + "\t" + message + "\t" + errors + Environment.NewLine);
                return Request.CreateResponse<string>(HttpStatusCode.BadRequest, errors);
            }
        }
    }
}
