using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

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

        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<PlatformReadDtos> GetPlatformById(int id)
        {
            var platformItem = _repository.GetPlatformById(id);
            if (platformItem != null) return Ok(_mappre.Map<PlatformReadDtos>(platformItem));
            return NotFound();
        }

        [HttpPost]
        public ActionResult<PlatformReadDtos> CreatePlatform(PlatformCreateDto platformCreateDto)
        {
            var platformModel = _mappre.Map<Platform>(platformCreateDto);
            _repository.CreatePlatform(platformModel);
            _repository.SaveChanges();

            var platformReadDto = _mappre.Map<PlatformReadDtos>(platformModel);

            return CreatedAtRoute(nameof(GetPlatformById), new { IdentityServiceCollectionExtensions = platformReadDto.Id }, platformReadDto);
        }
    }
}