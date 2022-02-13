using Entities.Users.UserDtos;
using Microsoft.AspNetCore.Mvc;
using Services.LoginServices;
using Services.PageUrlsDataServices;
using Services.TokenServices;
using Services.UserDataServices;
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
        private readonly IUserDataService _userService;

        public HomeController(ILogger<HomeController> logger, ILoginService loginService,
                ITokenService tokenService, IPageUrlsDataService pageUrlsDataService, IUserDataService userService)
        {
            _logger = logger;
            _loginService = loginService;
            _tokenService = tokenService;
            _pageUrlsDataService = pageUrlsDataService;
            _userService = userService;

        }
        public IActionResult create()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
       
        public async Task<IActionResult> updateAsync(Guid id)
        {
            var result = await _pageUrlsDataService.GetAsync(id);
            if (result != null)
                return View(result.Data.PageUrl);
            return BadRequest();
        }

        public async Task<IActionResult> deleteAsync(Guid id)
        {
            var result = await _pageUrlsDataService.GetAsync(id);
            if (result != null)
                return View(result.Data.PageUrl);
            return BadRequest();
        }


        public async Task<IActionResult> Login(string userName, string password)
        {
            
        
            try
            {
                var users = await _loginService.LoginAsync(userName, password);
                return RedirectToAction("Privacy", "Home");
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Home");
            }



        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetList()
        {
            var result = await _pageUrlsDataService.GetListAsync();
            if (result != null)
                return View(result);
            return BadRequest();
        }

        public async Task<IActionResult> PrivacyAsync()
        {
            var result = await _pageUrlsDataService.GetListAsync();

            return View(result.Data.PageUrls);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}