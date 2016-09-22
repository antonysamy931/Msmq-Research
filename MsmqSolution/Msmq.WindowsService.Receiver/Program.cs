using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Msmq.WindowsService.Receiver
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            if (Environment.UserInteractive)
            {
                NotificationService notificationService = new NotificationService();
                notificationService.DirectStart();
                Console.WriteLine("Press any key to stop program");
                Console.Read();
                notificationService.StopDirect();
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] 
                { 
                    new NotificationService() 
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
