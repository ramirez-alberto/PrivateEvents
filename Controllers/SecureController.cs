using Microsoft.AspNetCore.Mvc;
using PrivateEvents.Entities;
using Microsoft.AspNetCore.Authorization;

namespace PrivateEvents.Controllers;

[Authorize]
public class SecureController : Controller
{
    private readonly RepositoryContext _context;
    public SecureController(RepositoryContext repocontext)
    {
        _context = repocontext;
    }

    
    public IActionResult Index()
    {
        return View();
    }

}