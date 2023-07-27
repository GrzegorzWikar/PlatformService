using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;

namespace PlatformService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mappre;

        public PlatformsController(IPlatformRepo repository, IMapper mappre)
        {
            _repository = repository;
            _mappre = mappre;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDtos>> GetPlatforms()
        {
            Console.WriteLine("--> Geting Platforms....");

            var platformItems = _repository.GetAllPlatforms();

            return Ok(_mappre.Map<IEnumerable<PlatformReadDtos>>(platformItems));
        }
    }
}