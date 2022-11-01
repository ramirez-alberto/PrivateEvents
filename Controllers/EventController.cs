using Microsoft.AspNetCore.Mvc;
using PrivateEvents.Entities;
using PrivateEvents.Entities.Models;
using PrivateEvents.Entities.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using AutoMapper;

namespace PrivateEvents.Controllers;

[Authorize]
public class EventController : Controller
{
    private readonly RepositoryContext _context;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    public EventController(RepositoryContext context, IMapper mapper, UserManager<User> userManager)
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
    }


    public IActionResult OldIndex()
    {
        return View();
    }

    [ActionName("Index")]
    public async Task<IActionResult> GetAllEvents()
    {
        var userEvents = _context.Events.Include(e => e.User).AsNoTracking();
        return View(await userEvents.ToListAsync());
    }
    public async Task<IActionResult> UserEvents()
    {
        var userId = _userManager.GetUserId(User);
        var userEvents = _context.Events.Include(e => e.User).Where(e => e.Author == userId).AsNoTracking();
        return View(await userEvents.ToListAsync());
    }
    public async Task<IActionResult> TrackedEvents()
    {
        var userEvents = _context.Attendees
            .Include(a => a.Event)
                .ThenInclude(e => e.User)
            .Include(a => a.User)
            .AsNoTracking()
            .Where(a => a.UserId.Contains(_userManager.GetUserId(User)));

        return View(await userEvents.ToListAsync());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateEventDto eventDto)
    {
        if (!ModelState.IsValid)
            return View(eventDto);

        var userID = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //or inject usermanager invite.User = await _userManager.GetUserAsync(User); 
        var eventEntity = _mapper.Map<Event>(eventDto);

        eventEntity.Author = userID;
        _context.Add(eventEntity);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(UserEvents));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> FollowEvent(int? eventId)
    {
        if (eventId is null)
            return NotFound();

        var eventEntity = await _context.Events.FirstOrDefaultAsync(e => e.EventId == eventId);

        if (eventEntity is not null)
        {
            var attendee = new Attendee
            {
                EventId = eventEntity.EventId,
                UserId = _userManager.GetUserId(User)
            };

            _context.Add(attendee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return NotFound();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UnfollowEvent(int? eventId)
    {
        var userId = _userManager.GetUserId(User);
        if (eventId == null || _context.Attendees == null || userId is null)
        {
            return NotFound();
        }
        var attendee = await _context.Attendees.FindAsync(eventId,userId);

        if (attendee is not null)
        {
            _context.Remove(attendee);
        }

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));

    }


}