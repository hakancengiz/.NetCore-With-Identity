using Entity.Concrete;
using Entity.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        
        private readonly UserManager<AppUser> _userManager; //IdentityManager
        private readonly RoleManager<AppRole> _roleManager;
        public RegisterController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            ViewBag.Roles = roles;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserRegisterDto registerDto)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName,
                    Email = registerDto.Email,
                    UserName = registerDto.Email
                };

                var result = await _userManager.CreateAsync(user, registerDto.Password);

                if (result.Succeeded)
                {
                    var role = await _userManager.AddToRoleAsync(user, registerDto.UserRole);

                    TempData["Message"] = "Kayıt işlemi gerçekleştirildi. Lütfen giriş yapın.";
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }

            return View(registerDto);
        }
    }
}
