using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;

namespace Msmq.WindowsService.Receiver
{
    public class NotificationReader
    {
        private const string Path = ".\\Private$\\Antony";
        public void Read()
        {
            while (true)
            {
                MessageQueue queue = null;
                if (MessageQueue.Exists(Path))
                {
                    queue = new MessageQueue(Path, QueueAccessMode.Receive);
                    queue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });

                    MessageQueueTransaction transaction = new MessageQueueTransaction();

                    transaction.Begin();
                    try
                    {
                        var obj = queue.Receive().Body;

                        if (obj != null)
                        {
                            Console.WriteLine(obj);
                        }
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Abort();
                    }
                }
            }
        }
    }
}
