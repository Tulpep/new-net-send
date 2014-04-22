using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Tulpep.CentralNetSend.WebApiService
{
    public partial class CentralNetSend : ServiceBase
    {
        public CentralNetSend()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            string baseAddress = "http://localhost:56890/";
            WebApp.Start<Startup>(url: baseAddress);
        }

        protected override void OnStop()
        {
        }
    }
}
