using Entities.Users.UserDtos;
using Microsoft.AspNetCore.Mvc;

using Services.UserDataServices;
using System.Diagnostics;
using Web.Models;

namespace Web.Controllers
{
   
    public class UserController : Controller
    {
        private readonly IUserDataService _userService;

        public UserController(IUserDataService userService)
        {
            _userService = userService;
        }

        public IActionResult UserCreate()
        {
            return View();
        }
        public async Task<IActionResult> UserDeleteAsync(Guid id)
        {
            var result = await _userService.GetAsync(id);
            if (result != null)
                return View(result.Data.User);
            return BadRequest();
        }
        public async Task<IActionResult> UserUpdateAsync(Guid id)
        {
            var result = await _userService.GetAsync(id);
            if (result != null)
                return View(result.Data.User);
            return BadRequest();
        }
        public async Task<IActionResult> UserListAsync()
        {
            var result = await _userService.GetListAsync();

            return View(result.Data.Users);
        }
 

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddAsync(UserAddDto userAddDto)
        {
            
            var result = await _userService.AddAsync(userAddDto);
           
            if (result != null)
                return RedirectToAction("UserList", "User");
            return BadRequest();

        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> UpdateAsync(UserUpdateDto userUpdateDto)
        {
            var result = await _userService.UpdateAsync(userUpdateDto);
            if (result != null)
                return RedirectToAction("UserList", "User");
            return BadRequest();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _userService.DeleteAsync(id);
            if (result != null)
                return RedirectToAction("UserList", "User");
            return BadRequest();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}