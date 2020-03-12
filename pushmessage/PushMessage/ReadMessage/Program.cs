using RabbitMQ.Client;
using System;

namespace ReadMessage
{
    public class Program
    {
        private const string UserName = "testes";
        private const string Password = "testes";
        private const string HostName = "localhost";



        static void Main(string[] args)

        {

            ConnectionFactory connectionFactory = new ConnectionFactory
            {
                HostName = HostName,
                UserName = UserName,
                Password = Password,
            };

            var connection = connectionFactory.CreateConnection();
            var channel = connection.CreateModel();


            // accept only one unack-ed message at a time

            // uint prefetchSize, ushort prefetchCount, bool global



            channel.BasicQos(0, 1, false);

            MessageReceiver messageReceiver = new MessageReceiver(channel);

            //channel.BasicConsume("demoqueue", false, messageReceiver); // direct
            //channel.BasicConsume("topic.bombay.queue", false, messageReceiver); //topic
            channel.BasicConsume("Mumbai", false, messageReceiver);

            Console.ReadLine();

        }
    }
}
