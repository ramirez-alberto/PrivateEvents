using PrivateEvents.Entities.Models;
namespace PrivateEvents.Contracts;
public interface IAttendeeRepository
{
    Task<IEnumerable<Attendee>> FindEventsAttendedByUser(string? userId);
    Task<Attendee> FindAttendeeByUserandEventId(int? eventId, string userId);
    void Add(Attendee attendee);
    void Remove(Attendee attendee);
}