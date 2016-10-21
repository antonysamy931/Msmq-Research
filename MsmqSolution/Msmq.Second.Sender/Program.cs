using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Msmq.Second.Sender
{
    public class Program
    {
        private const string Path = ".\\Private$\\Antony";
        static void Main(string[] args)
        {
        Top:
            if (!CheckMSMQInstalled())
                //installer will work
                return;
            MessageQueue messageQueue = null;
            //Check msmq private path available or not
            if (!MessageQueue.Exists(Path))
            {
                //Create message queue with transaction enabled
                messageQueue = MessageQueue.Create(Path, true);
            }
            else
            {
                messageQueue = new MessageQueue(Path, QueueAccessMode.Send);
            }

            if (messageQueue.Transactional == true)
            {
                MessageQueueTransaction transaction = new MessageQueueTransaction();
                transaction.Begin();
                messageQueue.Send("Antony first send message to queue", transaction);
                transaction.Commit();
                messageQueue.Close();
            }
            Console.WriteLine("Send message type 1. For exit type 2.");
            var options = Console.ReadLine();
            if (Convert.ToInt32(options) == 1)
            {
                goto Top;
            }
            else
                return;
        }

        private static bool CheckMSMQInstalled()
        {
            bool available = false;
            List<ServiceController> services = ServiceController.GetServices().ToList();
            if (services != null && services.Count > 0)
            {
                ServiceController msQue = services.FirstOrDefault(o => o.ServiceName == "MSMQ");
                if (msQue != null)
                {
                    if (msQue.Status == ServiceControllerStatus.Running)
                    {
                        available = true;
                    }
                }
            }
            return available;
        }
    }
}
