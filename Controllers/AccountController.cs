using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using PrivateEvents.Entities.Models;
using AutoMapper;

namespace PrivateEvents.Controllers;

public class AccountController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public AccountController(ILogger<HomeController> logger, UserManager<User> userManager, IMapper mapper)
    {
        _logger = logger;
        _userManager = userManager;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(UserRegistrationModel userRegistration)
    {
        if (!ModelState.IsValid)
            return View(userRegistration);

        var user = _mapper.Map<User>(userRegistration);
        var result = await _userManager.CreateAsync(user, userRegistration.Password);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.TryAddModelError(error.Code, error.Description);
            }
            return View(userRegistration);
        }
        await _userManager.AddToRoleAsync(user, "Visitor");
        return RedirectToAction(nameof(HomeController.Index), "Home");
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(UserLoginModel userLoginModel, string? returnUrl = null)
    {
        if (!ModelState.IsValid)
            return View(userLoginModel);
        User? user = await _userManager.FindByEmailAsync(userLoginModel.Email);
        if (user != null && await _userManager.CheckPasswordAsync(user, userLoginModel.Password))
        {
            var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));

            await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme,
                new ClaimsPrincipal(identity));

            return RedirectToLocal(returnUrl);
        }
        else
        {
            
            ModelState.AddModelError("", $"Invalid User or Password {user.UserName} {user.Email}");
            return View();
        }
    }
    private IActionResult RedirectToLocal(string? returnUrl)
    {
        if (Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);
        else
            return RedirectToAction(nameof(HomeController.Index), "Home");

    }
}
