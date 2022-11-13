using PrivateEvents.Entities.Models;
using PrivateEvents.Entities;
using PrivateEvents.Contracts;
using Microsoft.EntityFrameworkCore;

namespace PrivateEvents.Repository;

public class EventRepository : RepositoryBase<Event>, IEventRepository
{
    public EventRepository(RepositoryContext context)
        : base(context) { }
    public async Task<IEnumerable<Event>> FindAllEventsAsync() =>
        await FindAll().Include(e => e.User).ToListAsync();
    public async Task<IEnumerable<Event>> FindEventsCreatedByUserAsync(string? userId) =>
        await FindByCondition(e => e.Author == userId).Include(u => u.User).ToListAsync();
    public async Task<Event?> FindEventByIdAsync(int? id) =>
        await FindByCondition(evento => evento.EventId == id)
                .Include(e => e.Attendees)
                .Include(e => e.User)
            .AsNoTracking()
            .FirstOrDefaultAsync();
    public void CreateEvent(Event evento) =>
        Create(evento);
    public void UpdateEvent(Event evento) =>
        Update(evento);
    public void DeleteEvent(Event evento) =>
        Delete(evento);
}