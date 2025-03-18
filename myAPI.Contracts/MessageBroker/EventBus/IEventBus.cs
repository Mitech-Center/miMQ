using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myAPI.Contracts.MessageBroker.EventBus
{
    public interface IEventBus
    {
        Task PublishAsync<T>(T message, CancellationToken cancellationToken = default)
            where T : class;
    }
}
