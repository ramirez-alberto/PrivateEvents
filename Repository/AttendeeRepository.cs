using PrivateEvents.Entities.Models;
using PrivateEvents.Entities;
using PrivateEvents.Contracts;
using Microsoft.EntityFrameworkCore;

namespace PrivateEvents.Repository;

public class AttendeeRepository : RepositoryBase<Attendee>, IAttendeeRepository
{
    public AttendeeRepository(RepositoryContext context) 
        :base(context)
    {}
    public async Task<IEnumerable<Attendee>> FindEventsAttendedByUser(string? userId)
    {
        return await FindByCondition(a => a.UserId == userId)
        .Include(a => a.Event)
            .ThenInclude(e => e.User)
        .ToListAsync();
    }

    public async Task<Attendee> FindAttendeeByUserandEventId(int? eventId, string userId) =>
        await FindByCondition(a => a.EventId == eventId && a.UserId == userId)
            .FirstOrDefaultAsync();
    
    public void Add(Attendee attendee) =>
        Create(attendee);
    public void Remove(Attendee attendee) =>
        Delete(attendee);
}