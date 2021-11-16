using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;

namespace WebUI.Controllers
{
    public class LogoutController : Controller
    {
        private readonly SignInManager<Customer> _signInManager;
        private readonly ILogger<LogoutModel> _logger;

        public LogoutController(SignInManager<Customer> signInManager, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Index()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction("Index", "Login");
        }
    }
}