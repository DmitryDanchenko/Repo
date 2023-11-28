
using System;
using Microsoft.Extensions.ObjectPool;
using RabbitMQ.Client;

namespace SendMsgToRabbit
{
    /// <summary>
    /// Определяет политику канала
    /// </summary>
    public class RabbitModelPooledObjectPolicy : IPooledObjectPolicy<IModel>
    {
        private readonly IConnection _connection;

        public RabbitModelPooledObjectPolicy(RabbitOptions optionsAccs)
        {
            try
            {
                _connection = GetConnection(optionsAccs);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"The connection was not established, check the input parameters!! \n {ex.Message}");
            }
            
        }

        /// <summary>
        ///  Cообщает пулу, как создать объект канала
        /// </summary>
        public IModel Create()
        {
            return _connection.CreateModel();
        }

        /// <summary>
        /// Определяет возможность повторного использования канала
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>bool</returns>
        public bool Return(IModel obj)
        {
            if (obj.IsOpen)
                return true;
 
            obj?.Dispose();
            return false;  
        }

        private IConnection GetConnection(RabbitOptions _options)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _options.HostName,
                UserName = _options.UserName,
                Password = _options.Password,
                Port = _options.Port,
                VirtualHost = _options.VHost,
            };

            return factory.CreateConnection();
        }
    }
}
