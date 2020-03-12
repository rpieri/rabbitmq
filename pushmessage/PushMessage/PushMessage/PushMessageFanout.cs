using RabbitMQ.Client;
using System;
using System.Text;

namespace PushMessage
{
    public class PushMessageFanout
    {
        public static void Push()
        {
            var userName = "testes";
            var password = "testes";
            var hostname = "localhost";
            var connectionFactory = new ConnectionFactory()
            {
                UserName = userName,
                Password = password,
                HostName = hostname
            };

            var connection = connectionFactory.CreateConnection();
            var model = connection.CreateModel();

            model.ExchangeDeclare("fanout.exchange", ExchangeType.Fanout);
            Console.WriteLine("Creating Exchange");


            model.QueueDeclare("Mumbai", true, false, false, null);
            model.QueueDeclare("Bangalore", true, false, false, null);
            model.QueueDeclare("Chennai", true, false, false, null);
            model.QueueDeclare("Hyderabad", true, false, false, null);
            Console.WriteLine("Creating Queues");


            model.QueueBind("Mumbai", "fanout.exchange","");
            model.QueueBind("Bangalore", "fanout.exchange", "");
            model.QueueBind("Chennai", "fanout.exchange", "");
            model.QueueBind("Hyderabad", "fanout.exchange", "");
            Console.WriteLine("Creating Bindings");


            var properties = model.CreateBasicProperties();
            properties.Persistent = false;

            byte[] messagebuffer = Encoding.Default.GetBytes("Message is of fanout Exchange type");
            model.BasicPublish("fanout.exchange", "", properties, messagebuffer);
            Console.WriteLine("Message Sent From : fanout.exchange");
            Console.WriteLine("Routing Key :  Routing key is not required for fanout exchange");
            Console.WriteLine("Message Sent");



            Console.ReadLine();
        }
    }
}
