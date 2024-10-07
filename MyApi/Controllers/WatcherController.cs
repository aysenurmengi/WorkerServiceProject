using EntityLayer.WatcherDto;
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
        }//dto - enum

        [HttpGet("filter")] // get metodu ile filtreleme işlemi yapmak için - api/watcher/filter
        //[FromQuery] -> parametreleri query string olarak alıyor, HTTP isteğini alıp metoda veriyoruz
        public async Task<IActionResult> GetWatchersByFilter([FromQuery] WatcherRequestDto request)
        {
            //servisdeki GetWatchersByFilter'a gidip parametrelerin sonuçları veritabanından çekiliyor
            var result = await _watcherService.GetWatchersByFilter(request);
            if (result == null || !result.Any())
            {
                return NotFound("bu kriterlere uygun bir sonuç bulunamadı."); //HTTP 400 ile dönüyor
            }
            var response = result.Select(w => new WatcherResponseDto
            {
                OldPath = w.OldPath,
                NewPath = w.NewPath,
                Time = w.Time
            });

            return Ok(response); //HTTP 200 ile dönüyor
        }
    }
}
