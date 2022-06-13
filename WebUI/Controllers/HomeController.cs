using Business.Abstract;
using DataAccess.Concrete.EntityFrameworkCore.Context;
using Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebUI.Models;
using WebUI.ViewModels;

namespace WebUI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;
        public HomeController(IArticleService articleService, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, ICategoryService categoryService)
        {
            _articleService = articleService;
            _userManager = userManager;
            _roleManager = roleManager;
            _categoryService = categoryService;
        }

        [Authorize(Roles = "Yazar, Okur")]
        public IActionResult Index()
        {
            HomeIndexViewModel homeIndexViewModel = new HomeIndexViewModel()
            {
                ArticleList = _articleService.GetListArticle().Data
            };

            return View(homeIndexViewModel);
        }

        public async Task GetAuthUserInfos()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
        }

        [Authorize(Roles = "Yazar")]
        public IActionResult AddArticle()
        {
            var categories = _categoryService.GetListCategory().Data;
            ViewBag.Categories = categories;

            return View();
        }

        [Authorize(Roles = "Yazar")]
        [HttpPost]
        public async Task<IActionResult> AddArticle(Article article)
        {
            var findUserId = await _userManager.FindByNameAsync(User.Identity.Name);
            article.UserId = findUserId.Id;

            if (!ModelState.IsValid)
                return View();

            var result = _articleService.AddArticle(article);
            TempData["Message"] = result.Message;

            return RedirectToAction("Index", "Home");
        }
    }
}