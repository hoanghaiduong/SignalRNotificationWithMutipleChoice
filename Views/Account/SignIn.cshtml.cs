using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using myapp.Data;

namespace myapp.Views.Account
{
    public class SignIn : PageModel
    {
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }
        private readonly ILogger<SignIn> _logger;
        private readonly AppDbContext _context;
        public SignIn(ILogger<SignIn> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {
        }
        // public IActionResult OnPostSignIn()
        // {
        //     // Implement your authentication logic here
           
        //     // if (Username == "test" && Password == "password") // Example condition
        //     // {
        //     //     // Set session or authentication cookie
        //     //     return RedirectToPage("/Index");
        //     // }

        //     // // Invalid login attempt
        //     // ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        //     // return Page();
        // }
    }
}