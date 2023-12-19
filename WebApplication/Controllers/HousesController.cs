using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using WebApplication.ActionFilters;
using WebApplication.ModelBinders;

namespace WebApplication.Controllers
{
    [ApiVersion("1.0")]
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
        public async Task<IActionResult> GetHouses()
        {
            var houses = await _repository.House.GetAllHousesAsync(trackChanges: false);
            var housesDto = _mapper.Map<IEnumerable<HouseDto>>(houses);
            return Ok(housesDto);
        }

        [HttpGet("{id}", Name = "HouseById")]
        public async Task<IActionResult> GetHouse(Guid id)
        {
            var house = await _repository.House.GetHouseAsync(id, trackChanges: false);
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
        public async Task<IActionResult> GetHouseCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                _logger.LogError("Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }

            var houseEntities = await _repository.House.GetByIdsAsync(ids, trackChanges: false);

            if (ids.Count() != houseEntities.Count())
            {
                _logger.LogError("Some ids are not valid in a collection");
                return NotFound();
            }

            var housesToReturn = _mapper.Map<IEnumerable<HouseDto>>(houseEntities);

            return Ok(housesToReturn);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateHouse([FromBody] HouseForCreationDto house)
        {
            if (house == null)
            {
                _logger.LogError("HouseForCreationDto object sent from client is null.");
                return BadRequest("HouseForCreationDto object is null");
            }

            var houseEntry = _mapper.Map<House>(house);
            _repository.House.CreateHouse(houseEntry);
            await _repository.SaveAsync();

            var houseToReturn = _mapper.Map<HouseDto>(houseEntry);

            return CreatedAtRoute("HouseById", new { id = houseToReturn.Id }, houseToReturn);
        }

        [HttpPost("collection")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateHouseCollection([FromBody] IEnumerable<HouseForCreationDto> houseCollection)
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

            await _repository.SaveAsync();

            var houseCollectionToReturn = _mapper.Map<IEnumerable<HouseDto>>(houseEntities);
            var ids = string.Join(",", houseCollectionToReturn.Select(c => c.Id));

            return CreatedAtRoute("HouseCollection", new { ids }, houseCollectionToReturn);
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateHouseExistsAttribute))]
        public async Task<IActionResult> DeleteHouse(Guid id)
        {
            var house = await _repository.House.GetHouseAsync(id, trackChanges: false);

            if (house == null)
            {
                _logger.LogInfo($"House with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _repository.House.DeleteHouse(house);
            await _repository.SaveAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateHouseExistsAttribute))]
        public async Task<IActionResult> UpdateHouse(Guid id, [FromBody] HouseForUpdateDto house)
        {
            if (house == null)
            {
                _logger.LogError("HouseForUpdateDto object sent from client is null.");
                return BadRequest("HouseForUpdateDto object is null");
            }

            var houseEntity = await _repository.House.GetHouseAsync(id, trackChanges: true);

            if (houseEntity == null)
            {
                _logger.LogInfo($"House with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _mapper.Map(house, houseEntity);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpOptions]
        public IActionResult GetHousesOptions()
        {
            Response.Headers.Add("Allow", "GET, OPTIONS, POST");
            return Ok();
        }
    }
}
