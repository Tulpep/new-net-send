using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Tulpep.CentralNetSend.API.Controllers
{
    [AllowAnonymous]
    public class MessageController : ApiController
    {
        public HttpResponseMessage PostMessage(string computerName, string message)
        {
            File.AppendAllText(@"C:\Users\Ricardo\Dev\Newnetsend\hola.txt", computerName + " " + message + Environment.NewLine);
            return Request.CreateResponse<string>(HttpStatusCode.OK, "Sent");
        }
    }
}
