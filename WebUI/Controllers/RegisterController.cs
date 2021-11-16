using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace WebUI.Controllers
{
    public class RegisterController : Controller
    {
        //Required dependencies to create user for us and also authorize them
        private readonly UserManager<Customer> _userManager;
        private readonly SignInManager<Customer> _signInManager;

        public RegisterController(UserManager<Customer> userManager, SignInManager<Customer> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(RegisterVM p_registerVM)
        {
            if (ModelState.IsValid)
            {
                var _user = new Customer()
                {
                    UserName = p_registerVM.Email,
                    Email = p_registerVM.Email,
                    Name = p_registerVM.Name,
                    PhoneNumber = p_registerVM.Phone,
                    Address = p_registerVM.Address
                };

                var result = await _userManager.CreateAsync(_user, p_registerVM.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(_user, false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }
    }
}