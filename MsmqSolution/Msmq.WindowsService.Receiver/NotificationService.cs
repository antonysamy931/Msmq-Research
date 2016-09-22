using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Msmq.WindowsService.Receiver
{
    partial class NotificationService : ServiceBase
    {
        Thread _Thread;
        public NotificationService()
        {
            InitializeComponent();
        }

        public void DirectStart()
        {
            OnStart(new string[0]);
        }

        public void StopDirect()
        {
            OnStop();
        }

        protected override void OnStart(string[] args)
        {
            NotificationReader reader = new NotificationReader();
            ThreadStart start = new ThreadStart(reader.Read);
            _Thread = new Thread(start);
            _Thread.IsBackground = true;
            _Thread.Start();
            // TODO: Add code here to start your service.
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
            _Thread.Abort();
        }        
    }
}
