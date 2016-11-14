using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace SimpleConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var queueUtilities = new QueueUtilities("");
            var queueClient = queueUtilities.NewCloudQueueClient("queue");
            queueClient.CreateIfNotExists();
            var message = args.Any() ? args[0] : "PING";
            queueUtilities.QueueSimpleMessage("queue", message);
        }

    }
}
