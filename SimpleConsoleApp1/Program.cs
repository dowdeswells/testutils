using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace SimpleConsoleApp1
{
    class Program
    {
        private const string StorageAccountConnectionString = "StorageAccountConnectionString";
        static void Main(string[] args)
        {
            try
            {
                var message = args.Any() ? args[0] : "PING";
                Console.Out.WriteLine("Message: {0}", message);
                var connectionString = CloudConfigurationManager.GetSetting(StorageAccountConnectionString);
                Console.Out.WriteLine("Connection String: {0}", connectionString);

                var queueUtilities = new QueueUtilities(connectionString);
                var queueClient = queueUtilities.NewCloudQueueClient("queue");
                queueClient.CreateIfNotExists();
                queueUtilities.QueueSimpleMessage("queue", message);
            }
            catch (Exception exc)
            {
                Console.Error.WriteLine(exc.ToString());
            }
        }

    }
}
