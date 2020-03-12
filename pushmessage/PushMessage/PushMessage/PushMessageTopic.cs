using RabbitMQ.Client;
using System;
using System.Text;

namespace PushMessage
{
    public class PushMessageTopic
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

            model.ExchangeDeclare("topic.exchange", ExchangeType.Topic);
            Console.WriteLine("Creating Exchange");


            model.QueueDeclare("topic.bombay.queue", true, false, false, null);
            Console.WriteLine("Creating Queue Bombay");

            model.QueueBind("topic.bombay.queue", "topic.exchange", "*.Bombay.*");
            Console.WriteLine("Creating Binding Bombay");


            model.QueueDeclare("topic.delhi.queue", true, false, false, null);
            Console.WriteLine("Creating Queue Bombay");

            model.QueueBind("topic.delhi.queue", "topic.exchange", "Delhi.#");
            Console.WriteLine("Creating Binding Bombay");


            var properties = model.CreateBasicProperties();
            properties.Persistent = false;

            byte[] messagebuffer = Encoding.Default.GetBytes("Message from Topic Exchange 'Bombay' ");
            model.BasicPublish("topic.exchange", "Message.Bombay.Email", properties, messagebuffer);
            Console.WriteLine("Message Sent From: topic.exchange ");
            Console.WriteLine("Routing Key: Message.Bombay.Email");
            Console.WriteLine("Message Sent");



            Console.ReadLine();
        }
    }
}
