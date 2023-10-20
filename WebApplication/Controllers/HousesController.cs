using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using WebApplication.ModelBinders;

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

        [HttpGet("{id}", Name = "HouseById")]
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

        [HttpGet("collection/{ids}", Name = "HouseCollection")]
        public IActionResult GetHouseCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                _logger.LogError("Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }

            var houseEntityes = _repository.House.GetByIds(ids, trackChanges: false);

            if (ids.Count() != houseEntityes.Count())
            {
                _logger.LogError("Some ids are not valid in a collection");
                return NotFound();
            }

            var housesToReturn = _mapper.Map<IEnumerable<HouseDto>>(houseEntityes);

            return Ok(housesToReturn);
        }

        [HttpPost]
        public IActionResult CreateHouse([FromBody] HouseForCreationDto house)
        {
            if (house == null)
            {
                _logger.LogError("HouseForCreationDto object sent from client is null.");
                return BadRequest("HouseForCreationDto object is null");
            }

            var houseEntry = _mapper.Map<House>(house);
            _repository.House.CreateHouse(houseEntry);
            _repository.Save();

            var houseToReturn = _mapper.Map<HouseDto>(houseEntry);

            return CreatedAtRoute("HouseById", new { id = houseToReturn.Id }, houseToReturn);
        }

        [HttpPost("collection")]
        public IActionResult CreateHouseCollection([FromBody] IEnumerable<HouseForCreationDto> houseCollection)
        {
            if (houseCollection == null)
            {
                _logger.LogError("House collection sent from client is null.");
                return BadRequest("House collection is null");
            }

            var houseEntities = _mapper.Map<IEnumerable<House>>(houseCollection);

            foreach (var house in houseEntities)
            {
                _repository.House.CreateHouse(house);
            }

            _repository.Save();

            var houseCollectionToReturn = _mapper.Map<IEnumerable<HouseDto>>(houseEntities);
            var ids = string.Join(",", houseCollectionToReturn.Select(c => c.Id));

            return CreatedAtRoute("HouseCollection", new { ids }, houseCollectionToReturn);
        }
    }
}
