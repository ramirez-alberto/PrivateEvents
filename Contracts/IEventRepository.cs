using PrivateEvents.Entities.Models;
namespace PrivateEvents.Contracts;

public interface IEventRepository
{
    Task<IEnumerable<Event>> FindAllEventsAsync();
    Task<Event?> FindEventByIdAsync(EventId id);
    Task<IEnumerable<Event>> FindAllUserEventsAsync(string? userId);
    void CreateEvent(Event evento);
    void UpdateEvent(Event evento);
    void DeleteEvent(Event evento);
}