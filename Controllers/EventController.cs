using Microsoft.AspNetCore.Mvc;
using PrivateEvents.Entities;
using PrivateEvents.Repository;
using PrivateEvents.Contracts;
using PrivateEvents.Entities.Models;
using PrivateEvents.Entities.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using AutoMapper;

namespace PrivateEvents.Controllers;

[Authorize]
public partial class EventController : Controller
{
    private readonly RepositoryContext _context;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly IRepositoryWrapper _repo;
    public EventController(
        RepositoryContext context, IMapper mapper, UserManager<User> userManager, IRepositoryWrapper repo)
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
        _repo = repo;
    }
    // public EventController(IMapper mapper, UserManager<User> userManager, IRepositoryWrapper repo)
    // {
    //     _mapper = mapper;
    //     _userManager = userManager;
    //     _repo = repo;
    // }


    public IActionResult OldIndex()
    {
        return View();
    }

    [ActionName("Index")]
    public async Task<IActionResult> GetAllEvents()
    {
        var userEvents = _repo.Event.FindAllEventsAsync();
        return View(await userEvents);
    }
    public async Task<IActionResult> UserEvents()
    {
        var userId = _userManager.GetUserId(User);
        var userEvents = _repo.Event.FindAllUserEventsAsync(userId);
        return View(await userEvents);
    }
    public async Task<IActionResult> TrackedEvents()
    {
        var userId = _userManager.GetUserId(User);
        var userEvents = _context.Attendees
            .Include(a => a.Event)
                .ThenInclude(e => e.User)
            .Include(a => a.User)
            .AsNoTracking()
            .Where(a => a.UserId.Contains(userId));

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

        //var userID = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //or inject usermanager invite.User = await _userManager.GetUserAsync(User); 
        var userID = _userManager.GetUserId(User);
        var eventEntity = _mapper.Map<Event>(eventDto);

        eventEntity.Author = userID;

        _repo.Event.CreateEvent(eventEntity);
        //_context.Add(eventEntity);
        await _repo.SaveAsync();

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

    public async Task<IActionResult> Details(int? id)
    {
        if(id is null) return NotFound();

        Event? eventDetails = await _repo.Event.FindEventByIdAsync(id);

        if(eventDetails is null) return NotFound();

        ViewData["AttendeesCount"] = eventDetails.Attendees.Count;

        return View(eventDetails);
    }
}