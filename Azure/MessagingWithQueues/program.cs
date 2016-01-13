//---------------------------------------------------------------------------------
// Microsoft (R)  Windows Azure SDK
// Software Development Kit
// 
// Copyright (c) Microsoft Corporation. All rights reserved.  
//
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE. 
//---------------------------------------------------------------------------------

namespace Microsoft.Samples.MessagingWithQueues
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Microsoft.ServiceBus;
    using Microsoft.ServiceBus.Messaging;
    using Microsoft.WindowsAzure;
    using System.Threading.Tasks;

    public class program
    {
        private static QueueClient queueClient;
        private static string QueueName = "SampleQueue";
        private const Int16 maxTrials = 4;
        private const string topicName = "Business";
        private const string TechNewsTopic = "Tech";

        static void Main(string[] args)
        {
            if (args[1] == "-topic")
            {
                if (args[0] == "-write")
                {
                    if (args.Length == 4)
                        WriteTopic(args[2], args[3]);
                    else
                        Console.WriteLine("Example cmd -write -topic News Business,Sports,Travel");
                }
                else if (args[0] == "-read")
                {
                    if (args.Length == 4)
                        ReadSubscriptionTopic(args[2], args[3]);
                    else
                        Console.WriteLine("Example cmd -read -topic News Business");
                }
            }

            if (args[1] == "-queue")
            {

            }
            
            //TestServiceBusQueue();
            //TestServiceSubMessageSubscription(BusinessNewsTopic, "AllMessage");
            //Task.WaitAll(TestGetSubscriptonMessage(BusinessNewsTopic, "AllMessage"));
            //Console.WriteLine("Read to received. Press any key");
            //Console.ReadLine();
        }

        private static void ReadSubscriptionTopic(string topic, string subcriptionName)
        {
            Console.WriteLine("Please Ctrl+C to stop reading.");
            Console.WriteLine("Reading topic :" + topic);
            Console.WriteLine("Subscription is :" + subcriptionName);

            var conn = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
            SubscriptionClient client = SubscriptionClient.CreateFromConnectionString(conn, topic, subcriptionName);

            bool keepLooping = true;
            OnMessageOptions options = new OnMessageOptions();
            options.AutoComplete = false;
            options.AutoRenewTimeout = TimeSpan.FromMinutes(1);

            client.OnMessage(msg =>
            {
                Console.WriteLine("Message ");
                Console.WriteLine("Body " + msg.GetBody<string>());
                Console.WriteLine("Id " + msg.MessageId);
                msg.Complete();
            });

            while (keepLooping)
            {
                Thread.Sleep(1000);
            }
        }

        private static void TestServiceSubMessageSubscription(string topic, string filter)
        {
            var conn = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
            SubscriptionClient client = SubscriptionClient.CreateFromConnectionString(conn, topic, filter);
        }

        private static void WriteTopic(string topicName, string subscriptionType)
        {
            Console.WriteLine("Creating topic");
            TryCreateTopic(topicName);

            var types = subscriptionType.Split(',');
            foreach (var item in types)
            {
                CreateSubscriptionFilter(topicName, item);
            }

            Console.WriteLine("Sending message to topic");
            SendMessageToTopic(topicName);

            Console.WriteLine("Topic created :" + topicName);
            Console.WriteLine("Subscription types :" + subscriptionType);
        }

        private static void CreateSubscriptionFilter(string topicName, string filtername)
        {
            var namespaceManager = NamespaceManager.Create();
            if (!namespaceManager.SubscriptionExists(topicName, filtername))
            {
                namespaceManager.CreateSubscription(topicName, filtername);
            }
        }

        private static void SendMessageToTopic(string topicName)
        {
            var conn = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
            var topicClient = TopicClient.CreateFromConnectionString(conn, topicName);

            List<BrokeredMessage> messageList = new List<BrokeredMessage>();

            messageList.Add(CreateSampleMessage("1", "First message information"));
            messageList.Add(CreateSampleMessage("2", "Second message information"));
            messageList.Add(CreateSampleMessage("3", "Third message information"));
            messageList.Add(CreateSampleMessage("4", "Third message information"));
            messageList.Add(CreateSampleMessage("5", "Third message information"));
            messageList.Add(CreateSampleMessage("6", "Third message information"));
            messageList.Add(CreateSampleMessage("7", "Third message information"));
            messageList.Add(CreateSampleMessage("8", "Third message information"));
            messageList.Add(CreateSampleMessage("9", "Third message information"));
            messageList.Add(CreateSampleMessage("10", "Third message information"));
            messageList.Add(CreateSampleMessage("11", "Third message information"));

            foreach (var item in messageList)
            {
                topicClient.Send(item);
            }
        }

        private static void TestServiceBusQueue()
        {
            // Please see http://go.microsoft.com/fwlink/?LinkID=249089 for getting Service Bus connection string and adding to app.config
            Console.WriteLine("Creating a Queue");
            CreateQueue();
            Console.WriteLine("Press anykey to start sending messages ...");
            Console.ReadKey();
            SendMessages();
            Console.WriteLine("Press anykey to start receiving messages that you just sent ...");
            Console.ReadKey();
            ReceiveMessages();
            Console.WriteLine("\nEnd of scenario, press anykey to exit.");
            Console.ReadKey();
        }

        private static void TryCreateTopic(string topicName)
        {
            NamespaceManager namespaceManager = NamespaceManager.Create();
            if (!namespaceManager.TopicExists(topicName))
            {
                namespaceManager.CreateTopic(topicName);
            }
        }

        private static void CreateQueue()
        {
            NamespaceManager namespaceManager = NamespaceManager.Create();

            Console.WriteLine("\nCreating Queue '{0}'...", QueueName);

            // Delete if exists
            if (namespaceManager.QueueExists(QueueName))
            {
                namespaceManager.DeleteQueue(QueueName);
            }

            namespaceManager.CreateQueue(QueueName);
        }

        private static void SendMessages()
        {
            queueClient = QueueClient.Create(QueueName);

            List<BrokeredMessage> messageList = new List<BrokeredMessage>();

            messageList.Add(CreateSampleMessage("1", "First message information"));
            messageList.Add(CreateSampleMessage("2", "Second message information"));
            messageList.Add(CreateSampleMessage("3", "Third message information"));

            Console.WriteLine("\nSending messages to Queue...");

            foreach (BrokeredMessage message in messageList)
            {
                while (true)
                {
                    try
                    {
                        queueClient.Send(message);
                    }
                    catch (MessagingException e)
                    {
                        if (!e.IsTransient)
                        {
                            Console.WriteLine(e.Message);
                            throw;
                        }
                        else
                        {
                            HandleTransientErrors(e);
                        }
                    }
                    Console.WriteLine(string.Format("Message sent: Id = {0}, Body = {1}", message.MessageId, message.GetBody<string>()));
                    break;
                }
            }

        }

        private static void ReceiveMessages()
        {
            Console.WriteLine("\nReceiving message from Queue...");
            BrokeredMessage message = null;
            while (true)
            {
                try
                {
                    //receive messages from Queue
                    message = queueClient.Receive(TimeSpan.FromSeconds(5));
                    if (message != null)
                    {
                        Console.WriteLine(string.Format("Message received: Id = {0}, Body = {1}", message.MessageId, message.GetBody<string>()));
                        // Further custom message processing could go here...
                        message.Complete();
                    }
                    else
                    {
                        //no more messages in the queue
                        break;
                    }
                }
                catch (MessagingException e)
                {
                    if (!e.IsTransient)
                    {
                        Console.WriteLine(e.Message);
                        throw;
                    }
                    else
                    {
                        HandleTransientErrors(e);
                    }
                }
            }
            queueClient.Close();
        }

        private static BrokeredMessage CreateSampleMessage(string messageId, string messageBody)
        {
            BrokeredMessage message = new BrokeredMessage(messageBody);
            message.MessageId = messageId;
            return message;
        }

        private static void HandleTransientErrors(MessagingException e)
        {
            //If transient error/exception, let's back-off for 2 seconds and retry
            Console.WriteLine(e.Message);
            Console.WriteLine("Will retry sending the message in 2 seconds");
            Thread.Sleep(2000);
        }
    }
}
