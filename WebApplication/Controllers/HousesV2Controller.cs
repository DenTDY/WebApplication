using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/houses")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v2")]
    public class HouseV2Controller : ControllerBase
    {
        private readonly IRepositoryManager _repository;

        public HouseV2Controller(IRepositoryManager repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Получает список всех домов
        /// </summary>
        /// <returns> Список домов</returns>
        [HttpGet(Name = "GetHouses"), Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetHouses()
        {
            var houses = await _repository.House.GetAllHousesAsync(trackChanges: false);
            return Ok(houses);
        }
    }
}
