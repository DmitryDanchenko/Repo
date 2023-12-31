﻿using System;
using System.Text;
using Microsoft.Extensions.ObjectPool;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace SendMsgToRabbit
{
    /// <summary>
    /// Релизует отправку сообщения
    /// </summary>
    public class RabbitManager : IRabbitManager
    {
        private readonly DefaultObjectPool<IModel> _objectPool;

        public RabbitManager(IPooledObjectPolicy<IModel> objectPolicy)
        {
            _objectPool = new DefaultObjectPool<IModel>(objectPolicy, Environment.ProcessorCount * 2);
        }

        public void Publish<T>(T message, string exchangeName, string exchangeType, string routeKey)
            where T : class
        {
            if (message == null)
                return;

            var channel = _objectPool.Get();

            try
            {
                var sendBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message, Formatting.Indented));

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(exchangeName, routeKey, properties, sendBytes);
                Console.WriteLine("A message has been sent");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"The message was not sent!! \n {ex.Message}");
            }
            finally
            {
                _objectPool.Return(channel);
            }
        }
    }
}
