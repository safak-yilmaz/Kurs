using Entities.PageUrls.PageUrlsDtos;
using Microsoft.AspNetCore.Mvc;
using Services.PageUrlsDataServices;

namespace Web.Controllers
{
    public class PageUrlController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly IPageUrlsDataService _pageUrlsDataService;

        public PageUrlController(IPageUrlsDataService pageUrlsDataService)
        {
            _pageUrlsDataService = pageUrlsDataService;
        }



        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetList()
        {
            var result = await _pageUrlsDataService.GetListAsync();
            if (result != null)
                return RedirectToAction("Privacy", "Home", result);
            return BadRequest();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _pageUrlsDataService.GetAsync(id);
            if (result != null)
                return Ok(result);
            return BadRequest();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetByUserId(Guid id)
        {
            var result = await _pageUrlsDataService.GetUserIdAsync(id);
            if (result != null)
                return Ok(result);
            return BadRequest();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddAsync([FromBody] PageUrlsAddDtos pageUrlsAddDtos)
        {
            var result = await _pageUrlsDataService.AddAsync(pageUrlsAddDtos);
            if (result != null)
                return RedirectToAction("Privacy", "Home", result);
            return BadRequest();

        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateAsync([FromBody] PageUrlsUpdateDtos pageUrlsUpdateDtos)
        {
            var result = await _pageUrlsDataService.UpdateAsync(pageUrlsUpdateDtos);
            if (result != null)
                return Ok(result);
            return BadRequest();
        }

        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _pageUrlsDataService.DeleteAsync(id);
            if (result != null)
                return Ok(result);
            return BadRequest();
        }
    }
}
