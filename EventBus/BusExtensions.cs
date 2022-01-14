﻿using EventBus.Models;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace EventBus
{
    public static class BusExtensions
    {
        private static string _host;

        public static void InitHost(string host) => _host = host;

        public static async Task SendAsync<T>(this IBus bus, T message, string queue)
        {
            var uri = new Uri($"{_host}/{queue}");
            var endPoint = await bus.GetSendEndpoint(uri);
            await endPoint.Send(message);
        }

        public static async Task<TResult> GetResponseAsync<TMessage, TResult>
            (this IBus bus, TMessage message, string queue)
            where TResult : class
            where TMessage : class
        {
            var uri = new Uri($"{_host}/{queue}");
            var client = bus.CreateRequestClient<TMessage>(uri);
            var response = await client.GetResponse<TResult, PersonNotFound>(message);

            if (response.Is(out Response<PersonNotFound> _))
            {
                return null;
            }

            if (response.Is(out Response<TResult> result))
            {
                return result.Message;
            }

            throw new InvalidOperationException();
        }
    }
}
