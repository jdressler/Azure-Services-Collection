using Azure.Identity;
using Azure.Messaging.ServiceBus;
using System.Net.NetworkInformation;

namespace ServiceBusConsole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var serviceBusUri = "";
            var queueName = "";

            ServiceBusClient client;

            ServiceBusProcessor processor;

            ServiceBusSender sender;
            

            var clientOptions = new ServiceBusClientOptions()
            {
                TransportType = ServiceBusTransportType.AmqpWebSockets
            };
            client = new ServiceBusClient(serviceBusUri,
                new DefaultAzureCredential(), clientOptions);

            Console.WriteLine("Connected to service bus: " + serviceBusUri);

            processor = client.CreateProcessor(queueName, new ServiceBusProcessorOptions());
            sender = client.CreateSender(queueName);

            
            Console.WriteLine("Connected to queue: " + queueName);

            try
            {

                processor.ProcessMessageAsync += MessageProcessor;
                processor.ProcessErrorAsync += ErrorProcessor;

                bool continuteLoop = true;

                while (continuteLoop)
                {
                    Console.WriteLine();
                    WriteMenu();

                    var menuSelection = Console.ReadLine();

                    Console.WriteLine();

                    switch (menuSelection)
                    {
                        case "1":
                            Console.WriteLine("Please enter message content");
                            var message = Console.ReadLine();
                            if (message != null && message.Length > 0)
                                await AddMessageToQueue(message, sender);
                            else
                                Console.WriteLine("No message content provided");
                            break;
                        case "2":
                            await processor.StartProcessingAsync();
                            Console.WriteLine("Processor started");
                            break;
                        case "3":
                            await processor.StopProcessingAsync();
                            Console.WriteLine("Processor stopped");
                            break;
                        case "4":
                            continuteLoop = false;
                            break;
                        default:
                            Console.WriteLine("Invalid option");
                            break;

                    }

                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error occured: " + ex.Message);
                Console.WriteLine("Closing application");
            }
            finally
            {
                await processor.DisposeAsync();
                await client.DisposeAsync();
            }
        
        }

        static void WriteMenu()
        {
            Console.WriteLine("Please select an option");
            Console.WriteLine("1. Add Message to queue");
            Console.WriteLine("2. Start processing messages");
            Console.WriteLine("3. Stop processing messages");
            Console.WriteLine("4. Exit application");

        }

        static async Task AddMessageToQueue(string message, ServiceBusSender sender)
        {
            using ServiceBusMessageBatch queueMessageBatch = await sender.CreateMessageBatchAsync();

            try
            {
                if (queueMessageBatch.TryAddMessage(new ServiceBusMessage(message)))
                {
                    await sender.SendMessagesAsync(queueMessageBatch);

                    Console.WriteLine("Message added to queue");
                }
                else
                {
                    Console.WriteLine("Unable to add message to queue. Message may be too big.");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to send message. Error: " + ex.Message);
            }


        }

        static async Task MessageProcessor(ProcessMessageEventArgs args)
        {
            Console.WriteLine();
            string messageBody = args.Message.Body.ToString();
            Console.WriteLine($"Message recieved. Message body: {messageBody} ");
            await args.CompleteMessageAsync(args.Message);
            Console.WriteLine();
            WriteMenu();
        }

        static Task ErrorProcessor(ProcessErrorEventArgs args)
        {
            Console.WriteLine($"Error occured while processing message. {args.Exception.Message} ");
            return Task.CompletedTask;
        }

       
    }
}