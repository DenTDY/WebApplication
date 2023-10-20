using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentsController : ControllerBase
    {
        private readonly IRepositoryManager repositoryManager;
        private readonly ILoggerManager loggerManager;
        private readonly IMapper mapper;

        public ApartmentsController(IRepositoryManager _repositoryManager, ILoggerManager _loggerManager, IMapper _mapper)
        {
            repositoryManager = _repositoryManager;
            loggerManager = _loggerManager;
            mapper = _mapper;
        }

        [HttpGet]
        public IActionResult GetApartmentsForHouse(Guid houseId)
        {
            var house = repositoryManager.House.GetHouse(houseId, trackChanges: false);

            if (house == null)
            {
                loggerManager.LogInfo($"House with id: {houseId} doesn't exist in the database.");
                return NotFound();
            }

            var apartmentsFromDb = repositoryManager.Apartment.GetApartments(houseId, trackChanges: false);
            var apartmentsDto = mapper.Map<IEnumerable<ApartmentDto>>(apartmentsFromDb);
            return Ok(apartmentsDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetapartmentForHouse(Guid houseId, Guid id)
        {
            var house = repositoryManager.House.GetHouse(houseId, trackChanges: false);

            if (house == null)
            {
                loggerManager.LogInfo($"House with id: {houseId} doesn't exist in the database.");
                return NotFound();
            }

            var apartmentDb = repositoryManager.Apartment.GetApartment(houseId, id, trackChanges: false);

            if (apartmentDb == null)
            {
                loggerManager.LogInfo($"Apartment with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var apartment = mapper.Map<ApartmentDto>(apartmentDb);

            return Ok(apartment);
        }
    }
}
