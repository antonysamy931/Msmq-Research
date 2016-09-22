using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
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
            MessageQueue messageQueue = null;
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
    }
}
