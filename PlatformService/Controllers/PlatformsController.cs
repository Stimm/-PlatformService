using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PlatformsController : ControllerBase
{
    private readonly IPlatformRepo _repository;
    private readonly IMapper _mapper;

    public PlatformsController(IPlatformRepo repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
    {
        var results = _repository.GetAllPlatforms();

        return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(results));
    }

    [HttpGet("{id}", Name = "GetPlatfromById")]
    public ActionResult<PlatformReadDto> GetPlatfromById(int id)
    {
        var result = _repository.GetPlatformById(id);

        if(result == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<PlatformReadDto>(result));
    }

    [HttpPost]
    public ActionResult<PlatformReadDto> CreatePlatform(PlatformCreateDto request)
    {
        var platformModel = _mapper.Map<Platform>(request);
        _repository.CreatePlatform(platformModel);
        _repository.SaveChanges();

        var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);

        return CreatedAtRoute(nameof(GetPlatfromById), new { id = platformReadDto.Id }, platformReadDto);
    }
}
