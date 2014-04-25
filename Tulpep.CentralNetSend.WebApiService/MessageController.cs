using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Web.Http;
using System.Text;
using System.IO;

namespace Tulpep.CentralNetSend.WebApiService
{
    [AllowAnonymous]
    public class MessageController : ApiController
    {
        [DllImport("kernel32.dll", SetLastError=true)]
        public static extern int Wow64DisableWow64FsRedirection(ref IntPtr ptr);

        public HttpResponseMessage PostMessage(string computerName, string message)
        {
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
            string errors = process.StandardOutput.ReadToEnd();


            string pathOfLog = (Environment.SystemDirectory) + @"\Log-Central-Message.txt";
            if(errors != null)
            {
                File.AppendAllText(pathOfLog, DateTime.Now + "\t" + computerName + "\t" + message + "\t OK" + Environment.NewLine);
                return Request.CreateResponse<string>(HttpStatusCode.BadRequest, errors);

            }
            else
            {
                File.AppendAllText(pathOfLog, DateTime.Now + "\t" + computerName + "\t" + message + "\t" + errors + Environment.NewLine);
                return Request.CreateResponse<string>(HttpStatusCode.OK, "Sent");
            }
        }
    }
}
