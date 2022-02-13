using Entities.Users.UserDtos;
using Microsoft.AspNetCore.Mvc;
using Services.LoginServices;
using Services.PageUrlsDataServices;
using Services.TokenServices;
using System.Diagnostics;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILoginService _loginService;
        private readonly ITokenService _tokenService;
        private readonly IPageUrlsDataService _pageUrlsDataService;

        public HomeController(ILogger<HomeController> logger, ILoginService loginService,
                ITokenService tokenService, IPageUrlsDataService pageUrlsDataService)
        {
            _logger = logger;
            _loginService = loginService;
            _tokenService = tokenService;
            _pageUrlsDataService = pageUrlsDataService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Login(string userName, string password)
        {
            var users = await _loginService.LoginAsync(userName, password);
            if (users.Data.User.UserName == userName)
            {
                return RedirectToAction("Privacy", "Home");
            }
            else {
                return View();
            }
            



        }

        public async Task<IActionResult> PrivacyAsync()
        {
            var result = await _pageUrlsDataService.GetListAsync();
            return View(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}