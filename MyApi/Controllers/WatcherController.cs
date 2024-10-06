using Microsoft.AspNetCore.Mvc;
using ServiceLayer;
using ServiceLayer.Abstract;


namespace MyApi.Controllers
{
    [ApiController] //bunun bir api olduğunu söylüyoruz
    [Route("api/[controller]")] // yönlendirme için
    public class WatcherController : ControllerBase
    {
        private readonly IWatcherService _watcherService;
        public WatcherController(IWatcherService watcherService)
        {
            _watcherService = watcherService;
        }

        [HttpGet("filter")] // get metodu ile filtreleme işlemi yapmak için - api/watcher/filter
        //[FromQuery] -> parametreleri query string olarak alıyor, HTTP isteğini alıp metoda veriyoruz
        public async Task<IActionResult> GetWatchersByFilter([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] string type)
        {
            //servisdeki GetWatchersByFilter'a gidip parametrelerin sonuçları veritabanından çekiliyor
            var filteredWatchers = await _watcherService.GetWatchersByFilter(startDate, endDate, type);
            if (filteredWatchers == null || !filteredWatchers.Any())
            {
                return NotFound("bu kriterlere uygun bir sonuç bulunamadı."); //HTTP 400 ile dönüyor
            }

            return Ok(filteredWatchers); //HTTP 200 ile dönüyor
        }
    }
}
