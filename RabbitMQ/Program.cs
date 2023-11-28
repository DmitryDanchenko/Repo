
using RabbitMQ.Client;
using System;
using System.Threading.Tasks;

namespace SendMsgToRabbit
{
    internal class Program
    {
        public static async Task Main(
            string hostName = "localhost",
            int port = 5672,
            string vHost = "Shovel3",
            string userName = "admin",
            string password = "12345",
            string exchangeName = "shovel")
        {
            var train = new DefoltTrain().CreateDto();

            Console.WriteLine($"Sending a message to RabbitMQ \n Сonnection string : amqp://{userName}:{password}@{hostName}:{port}/{vHost} " +
                $"\n Name exchange : {exchangeName} \n Type exchange : {ExchangeType.Fanout}");
            var rabbitOptions = new RabbitOptions() { HostName = hostName, Port = port, VHost = vHost, UserName = userName, Password = password };

            var rabbitModelPooledObjectPolicy = new RabbitModelPooledObjectPolicy(rabbitOptions);
            var rabbitManager = new RabbitManager(rabbitModelPooledObjectPolicy);
            rabbitManager.Publish(message: train, exchangeName: exchangeName, exchangeType: ExchangeType.Fanout, routeKey: "");
        }
    }
}
