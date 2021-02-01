using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SocialMediaLinkedIn.Models;
using SocialMediaLinkedIn.ViewModels;
using Microsoft.AspNetCore.Session;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SocialMediaLinkedIn.Controllers
{   [Authorize]
    public class AccountController : Controller
    {
        // GET: /<controller>/
        public UserManager<IdentityUser> userManager;
        public EmployeeRepository _employeeRepository;

        public SignInManager<IdentityUser> signInManager;
        public AccountController( UserManager<IdentityUser> userManager,EmployeeRepository _employeeRepository, SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this._employeeRepository = _employeeRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            ViewBag.PageTitle = "Register";
            return View();
        }



        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            AccountController _accountController = null;
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                var users = userManager.Users;
                int count = users.Count();
                Console.WriteLine(count);
                Console.WriteLine(users);
                if(count==5)
                {
                    ViewBag.ErrorMessage = "You cannot register, Maximum no of users reached";
                    ViewBag.PageTitle = "Register";
                    return View(model);                 
                }
               else if (count <5)
                {
                    var result = await userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        IEnumerable<ActiveUsers> activeusers = _employeeRepository.AddActiveUser(model.Email);
                        HttpContext.Session.SetString("Email", model.Email);
                        if (activeusers.Count()==4)
                        {
                            await signInManager.SignInAsync(user, isPersistent: false);
                            return RedirectToAction("details", "home");

                        }
                        if(activeusers.Count()>4)
                        {
                            await _accountController.Logout();
                            _employeeRepository.RemoveActiveUsers(model.Email);
                            return RedirectToAction("login", "account");
                        }
                        
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "You cannot register, Maximum no of users reached";
                }
            }
            else
            {
                ViewBag.PageTitle = "Register";
            }
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("Email");
            await signInManager.SignOutAsync();
            return RedirectToAction("details", "home");
        }

        [HttpGet]
        public IActionResult ListUsers()
        {
            var users= userManager.Users;
           // ListOfIdentityUsers userslist = (ListOfIdentityUsers)users;
            Console.WriteLine(users);
            return View(users);
        }



        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            string val = null;
            val=HttpContext.Session.GetString("Email");
            if(val!=null)
            {
                return Ok(RedirectToAction("details", "home"));
            }
            else
            {
                val = null;
                ViewBag.PageTitle = "Login";
                return Ok(View());
            }
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
           
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                HttpContext.Session.SetString("Email",model.Email);
                if (result.Succeeded)
                {
                    return RedirectToAction("details", "home");
                }
                ModelState.AddModelError(string.Empty, "invalid Login Attempt");
            }
            return View(model);
        }




    }
}
/*
*/
