using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using PrivateEvents.Entities.Models;

namespace PrivateEvents.Controllers;

public class AccountController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserManager<User> _userManager;

    public AccountController(ILogger<HomeController> logger, UserManager<User> userManager)
    {
        _logger = logger;
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(UserLoginModel userLoginModel, string returnUrl = null)
    {
        if (!ModelState.IsValid)
            return View(userLoginModel);
        var user = await _userManager.FindByEmailAsync(userLoginModel.Email);
        if (user is null && await _userManager.CheckPasswordAsync(user, userLoginModel.Password))
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
            ModelState.AddModelError("","Invalid User or Password");
            return View();
        }


        return View();
    }
    private IActionResult RedirectToLocal(string returnUrl)
    {
        if (Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);
        else
            return RedirectToAction(nameof(HomeController.Index), "Home");

    }
}
