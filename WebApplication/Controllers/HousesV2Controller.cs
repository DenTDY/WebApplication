using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/companies")]
    [ApiController]
    public class HouseV2Controller : ControllerBase
    {
        private readonly IRepositoryManager _repository;

        public HouseV2Controller(IRepositoryManager repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetHouses()
        {
            var houses = await _repository.House.GetAllHousesAsync(trackChanges: false);
            return Ok(houses);
        }
    }
}
