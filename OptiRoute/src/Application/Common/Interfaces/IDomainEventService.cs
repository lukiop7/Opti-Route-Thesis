using OptiRoute.Domain.Common;
using System.Threading.Tasks;

namespace OptiRoute.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
