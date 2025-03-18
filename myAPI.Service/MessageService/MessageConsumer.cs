using MassTransit;
using Microsoft.Extensions.Logging;
using myAPI.Contracts;
using myAPI.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myAPI.Service.MessageService
{
    public sealed class MessageConsumer : IConsumer<MessageForPublish>
    {
        private readonly ILogger<MessageConsumer> _logger;

        public MessageConsumer(ILogger<MessageConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<MessageForPublish> context)
        {
            _logger.LogInformation("Receive: {@message}", context.Message);
            return Task.CompletedTask;
        }
        
    }
}
