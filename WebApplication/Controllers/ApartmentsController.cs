using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication.ActionFilters;
using WebApplication.ActionFilters;

namespace WebApplication.Controllers
{
    [Route("api/houses/{houseId}/apartments")]
    [ApiController]
    public class ApartmentsController : ControllerBase
    {
        private readonly IRepositoryManager repositoryManager;
        private readonly ILoggerManager loggerManager;
        private readonly IMapper mapper;
        private readonly IDataShaper<ApartmentDto> dataShaper;

        public ApartmentsController(IRepositoryManager _repositoryManager, ILoggerManager _loggerManager, IMapper _mapper, IDataShaper<ApartmentDto> _dataShaper)
        {
            repositoryManager = _repositoryManager;
            loggerManager = _loggerManager;
            mapper = _mapper;
            dataShaper = _dataShaper;
        }

        [HttpGet]
        [HttpHead]
        public async Task<IActionResult> GetApartmentsForHouse(Guid houseId, [FromQuery] ApartmentParameters apartmentParametrs)
        {

            if (!apartmentParametrs.ValidNumberRoomRange)
            {
                return BadRequest("Max number room can't be less than min number room.");
            }

            var house = await repositoryManager.House.GetHouseAsync(houseId, trackChanges: false);

            if (house == null)
            {
                loggerManager.LogInfo($"House with id: {houseId} doesn't exist in the database.");
                return NotFound();
            }

            var apartmentsFromDb = await repositoryManager.Apartment.GetApartmentsAsync(houseId, apartmentParametrs, trackChanges: false);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(apartmentsFromDb.MetaData));

            var apartmentsDto = mapper.Map<IEnumerable<ApartmentDto>>(apartmentsFromDb);
            return Ok(dataShaper.ShapeData(apartmentsDto, apartmentParametrs.Fields));
        }

        [HttpGet("{id}", Name = "GetApartmentForHouse")]
        public async Task<IActionResult> GetApartmentForHouse(Guid houseId, Guid id)
        {
            var house = await repositoryManager.House.GetHouseAsync(houseId, trackChanges: false);

            if (house == null)
            {
                loggerManager.LogInfo($"House with id: {houseId} doesn't exist in the database.");
                return NotFound();
            }

            var apartmentDb = await repositoryManager.Apartment.GetApartmentAsync(houseId, id, trackChanges: false);

            if (apartmentDb == null)
            {
                loggerManager.LogInfo($"Apartment with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var apartment = mapper.Map<ApartmentDto>(apartmentDb);

            return Ok(apartment);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateApartmentForHouse(Guid houseId, [FromBody] ApartmentForCreationDto apartment)
        {
            var apartmentEntity = mapper.Map<Apartment>(apartment);
            repositoryManager.Apartment.CreateApartmentForHouse(houseId, apartmentEntity);
            await repositoryManager.SaveAsync();

            var apartmentToReturn = mapper.Map<ApartmentDto>(apartmentEntity);

            return CreatedAtRoute("GetApartmentForHouse", new
            {
                houseId,
                id = apartmentToReturn.Id
            }, apartmentToReturn);
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateApartmentForHouseExistsAttribute))]
        public async Task<IActionResult> DeleteApartmentForHouse(Guid houseId, Guid id)
        {
            var apartmentForHouse = HttpContext.Items["apartmen"] as Apartment;
            repositoryManager.Apartment.DeleteApartment(apartmentForHouse);
            await repositoryManager.SaveAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateApartmentForHouseExistsAttribute))]
        public async Task<IActionResult> UpdateApartmentForHouse(Guid houseId, Guid id, [FromBody] ApartmentForUpdateDto apartment)
        {
            var apartmentEntity = HttpContext.Items["apartment"] as Apartment;
            mapper.Map(apartment, apartmentEntity);
            await repositoryManager.SaveAsync();
            return NoContent();
        }

        [HttpPatch("{id}")]
        [ServiceFilter(typeof(ValidateApartmentForHouseExistsAttribute))]
        public async Task<IActionResult> PartiallyUpdateApartmentForHouse(
            Guid houseId,
            Guid id,
            [FromBody] JsonPatchDocument<ApartmentForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                loggerManager.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }

            var apartmentEntity = HttpContext.Items["apartment"] as Apartment;
            var apartmentToPatch = mapper.Map<ApartmentForUpdateDto>(apartmentEntity);

            TryValidateModel(apartmentToPatch);

            if (!ModelState.IsValid)
            {
                loggerManager.LogError("Invalid model state for the patch document");
                return UnprocessableEntity(ModelState);
            }

            mapper.Map(apartmentToPatch, apartmentEntity);
            await repositoryManager.SaveAsync();

            return NoContent();
        }
    }
}
