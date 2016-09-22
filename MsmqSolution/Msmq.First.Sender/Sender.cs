using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;

namespace Msmq.First.Sender
{
    public class Sender
    {
        private const string Path = ".\\Private$\\MyQueue";

        public static void Main(string[] args)
        {
            Top:
            MessageQueue messageQueue = null;
            if (!MessageQueue.Exists(Path))
            {
                messageQueue = MessageQueue.Create(Path);
            }
            else
            {
                messageQueue = new MessageQueue(Path, QueueAccessMode.Send);
            }
            messageQueue.Send("Antony first send message to queue");
            messageQueue.Close();

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
