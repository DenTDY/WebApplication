using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        public WeatherForecastController(IRepositoryManager repositoryManager)
        {
            _repository = repositoryManager;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            _repository.Company.TestCompany();
            _repository.Employee.TestEmployee();
            _repository.House.TestHouse();
            _repository.Apartment.TestApartment();
            return new string[] { "value1", "value2" };
        }
    }
}