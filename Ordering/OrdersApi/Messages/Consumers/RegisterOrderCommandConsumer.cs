using MassTransit;
using Messaging.InterfacesConstants.Commands;
using OrdersApi.Models;
using OrdersApi.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersApi.Messages.Consumers
{
    public class RegisterOrderCommandConsumer : IConsumer<IRegisterOrderCommad>
    {
        private readonly IOrderRepository _orderRepo;
        public RegisterOrderCommandConsumer(IOrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
        }
        public Task Consume(ConsumeContext<IRegisterOrderCommad> context)
        {
            var result = context.Message;
            if(result.OrderId != null && result.ImageUrl != null && result.UserEmail != null && result.ImageData != null)
            {
                SaveOrder(result);
            }
            return Task.FromResult(true);
        }

        private void SaveOrder(IRegisterOrderCommad result)
        {
            Order order = new Order
            {
                OrderId = result.OrderId,
                UserEmail = result.UserEmail,
                Status = Status.Registered,
                ImageUrl = result.ImageUrl,
                ImageData = result.ImageData
            };
            _orderRepo.RegisterOrder(order);
        }
    }
}
