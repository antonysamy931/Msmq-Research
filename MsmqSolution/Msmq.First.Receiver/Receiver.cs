using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;

namespace Msmq.First.Receiver
{
    public class Receiver
    {
        private const string Path = ".\\Private$\\MyQueue";
        public static void Main(string[] args)
        {
            MessageQueue messageQueue = null;
            if (MessageQueue.Exists(Path))
            {
                messageQueue = new MessageQueue(Path, QueueAccessMode.Receive);
                messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
                var receivedMessage = (string)messageQueue.Receive().Body;
                //string message = (string)receivedMessage.Body;
                Console.WriteLine(receivedMessage);
            }
        }
    }
}
