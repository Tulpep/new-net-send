using Microsoft.Owin.Hosting;
using Microsoft.Win32;
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
            string urlOfServer;

            using (RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            {
                using (RegistryKey centralNetSendKey = hklm.OpenSubKey(@"SOFTWARE\Tulpep\CentralNetSend"))
                {
                    urlOfServer = centralNetSendKey.GetValue("APIServer").ToString();
                }

            }

            WebApp.Start<Startup>(url: urlOfServer);
        }

        protected override void OnStop()
        {
        }
    }
}
