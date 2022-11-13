using PrivateEvents.Entities.Models;
namespace PrivateEvents.Contracts;

public interface IEventRepository
{
    Task<IEnumerable<Event>> FindAllEventsAsync();
    Task<Event?> FindEventByIdAsync(int? id);
    Task<IEnumerable<Event>> FindEventsCreatedByUserAsync(string? userId);
    void CreateEvent(Event evento);
    void UpdateEvent(Event evento);
    void DeleteEvent(Event evento);
}