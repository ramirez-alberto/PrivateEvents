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
        return View( await userEvents.ToListAsync());
    }
    public async Task<IActionResult> UserEvents()
    {
        var userId = _userManager.GetUserId(User);
        var userEvents = _context.Events.Include(e => e.User).Where( e => e.Author == userId).AsNoTracking();
        return View( await userEvents.ToListAsync());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateEventDto eventDto)
    {
        if(!ModelState.IsValid)
            return View(eventDto);

        var userID = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //or inject usermanager invite.User = await _userManager.GetUserAsync(User); 
        var eventEntity = _mapper.Map<Event>(eventDto);

        eventEntity.Author = userID;
        _context.Add(eventEntity);
        await _context.SaveChangesAsync();
        
        return RedirectToAction(nameof(UserEvents));
    }


}