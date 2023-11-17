using VoteAPI.Domain.Entities.Rabbit;

namespace VoteAPI.Domain.Interfaces.Rabbit
{
    public interface IRabbitRepository
    {
        void SendMessage(RabbitMessage message);
    }
}