using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using myapp.Repositories;

namespace myapp.Controllers
{

    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly UserRepository _userRepository;
        public AccountController(ILogger<AccountController> logger, UserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PostSignIn(string username, string password)
        {
            var userFromDb = await _userRepository.GetUserDetails(username, password);

            if (userFromDb == null)
            {
                ModelState.AddModelError("Login", "Invalid credentials");
                return View();
            }

            HttpContext.Session.SetString("Username", userFromDb.UserName);

            return RedirectToAction("Index", "Home");
        }
        public ActionResult Logout()
        {
            HttpContext.Session.Remove("Username");
            return RedirectToAction(nameof(SignIn));
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}