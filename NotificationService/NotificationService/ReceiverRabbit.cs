using System;
using System.Net.Http;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Threading;

namespace NotificationService
{
    public class ReceiverRabbit
    {
        public static void Receiver()
        {
            var client = new HttpClient();
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare("halopakpos", "fanout");

                channel.QueueDeclare(queue: "pakpos", durable: true, exclusive: false, autoDelete: false, arguments: null);
                channel.QueueBind("pakpos", "halopakpos", routingKey: "");

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += async (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    var content = new StringContent(message, Encoding.UTF8, "application/json");
                    Console.WriteLine("A message has been received.");

                    await client.PostAsync("http://localhost:5800/api/notification", content);
                };

                channel.BasicConsume(queue: "pakpos", autoAck: true, consumer: consumer);

                Thread.Sleep(8000);
            }
        }
    }
}
