using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;

namespace SendCaEvents
{
    class Program
    {
        private const string connectionString = ""; //To be set
        private const string eventHubName = "caevents";

        static  async Task Main(string[] args)
        {
            //Console.WriteLine("Waiting for 5 secs before we start");
            //Thread.Sleep(5000);
            await using (var producerClient = new EventHubProducerClient(connectionString, eventHubName))
            {
                for (int i = 0; i < 100; i++)
                {

                    // Get the JSOn content
                    var filePostFix = i % 5;
                    var filename = $@"..\..\..\data\LineItemCreated{filePostFix}";
                    var json = File.ReadAllText(filename);

                    // Create a batch of events 
                    using EventDataBatch eventBatch = await producerClient.CreateBatchAsync();
                     
                    // Add events to the batch. An event is a represented by a collection of bytes and metadata. 
                    var result = eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes(json)));

                    // Use the producer client to send the batch of events to the event hub
                    await producerClient.SendAsync(eventBatch);
                    Console.WriteLine($"Event added from {filename}");
                    Console.WriteLine("Sleeping for 2 seconds.");
                    Thread.Sleep(2000);
                }
                Console.WriteLine("Event sent completed");
            }
        }
    }
}
