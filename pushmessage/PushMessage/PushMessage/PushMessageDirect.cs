using RabbitMQ.Client;
using System;
using System.Text;

namespace PushMessage
{
    public class PushMessageDirect
    {
        public static void Push()
        {
            var userName = "testes";
            var password = "testes";
            var hostname = "localhost";
            var connectionFactory = new RabbitMQ.Client.ConnectionFactory()
            {
                UserName = userName,
                Password = password,
                HostName = hostname
            };

            var connection = connectionFactory.CreateConnection();
            var model = connection.CreateModel();

            model.ExchangeDeclare("demoExchange", ExchangeType.Direct);
            Console.WriteLine("Creating Exchange");


            model.QueueDeclare("demoqueue", true, false, false, null);
            Console.WriteLine("Creating Queue");

            model.QueueBind("demoqueue", "demoExchange", "directexchange_key");
            Console.WriteLine("Creating Binding");

            var properties = model.CreateBasicProperties();
            properties.Persistent = false;



            byte[] messagebuffer = Encoding.Default.GetBytes("Direct Message");
            model.BasicPublish("demoExchange", "directexchange_key", properties, messagebuffer);
            Console.WriteLine("Message Sent");

            Console.ReadLine();
        }
    }
}
