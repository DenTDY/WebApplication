using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HousesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public HousesController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetHouses()
        {
            var houses = _repository.House.GetAllHouses(trackChanges : false);
                var housesDto = _mapper.Map<IEnumerable<HouseDto>>(houses);
                return Ok(housesDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetHouse(Guid id)
        {
            var house = _repository.House.GetHouse(id, trackChanges: false);
            if (house == null)
            {
                _logger.LogInfo($"House with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var houseDto = _mapper.Map<HouseDto>(house);
                return Ok(houseDto);
            }
        }
    }
}
