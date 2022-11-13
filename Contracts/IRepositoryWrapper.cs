using PrivateEvents.Entities.Models;

namespace PrivateEvents.Contracts;

public interface IRepositoryWrapper
{
    IEventRepository Event { get; }
    IAttendeeRepository Attendee { get; }
    Task SaveAsync();
}