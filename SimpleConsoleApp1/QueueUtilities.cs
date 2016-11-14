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
    class QueueUtilities
    {
        private readonly string _storageConnectionString;

        public QueueUtilities(string storageConnectionString)
        {
            _storageConnectionString = storageConnectionString;
        }

        public void QueueSimpleMessage(string cloudQueueName, string payload)
        {
            var queue = NewCloudQueueClient(cloudQueueName);
            var message = new CloudQueueMessage(payload);
            queue.AddMessage(message);
        }
        public void QueueMessage(string cloudQueueName, object payload)
        {
            var message = NewMessage(payload);
            var queue = NewCloudQueueClient(cloudQueueName);
            queue.AddMessage(message);
        }
        public CloudQueue NewCloudQueueClient(string cloudQueueName)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_storageConnectionString);
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue queue = queueClient.GetQueueReference(cloudQueueName);
            return queue;
        }

        public CloudQueueMessage NewMessage(object payload)
        {
            var message = new CloudQueueMessage(ToBinary(payload));
            return message;
        }

        private byte[] ToBinary(object payload)
        {
            BinaryFormatter bf = new BinaryFormatter();
            byte[] output = null;
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, payload);
                output = ms.ToArray();
            }
            return output;
        }

        private T FromBinary<T>(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                BinaryFormatter bf = new BinaryFormatter();
                T output = (T)bf.Deserialize(ms);
                return output;
            }
        }
    }
}
