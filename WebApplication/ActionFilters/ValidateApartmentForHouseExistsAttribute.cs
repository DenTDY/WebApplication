using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApplication.ActionFilters
{
    public class ValidateApartmentForHouseExistsAttribute : IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public ValidateApartmentForHouseExistsAttribute(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var method = context.HttpContext.Request.Method;
            var trackChanges = (method.Equals("PUT") || method.Equals("PATCH")) ? true : false;
            var houseId = (Guid)context.ActionArguments["houseId"];
            var house = await _repository.House.GetHouseAsync(houseId, false);

            if (house == null)
            {
                _logger.LogInfo($"House with id: {houseId} doesn't exist in the database.");
                context.Result = new NotFoundResult();
                return;
            }

            var id = (Guid)context.ActionArguments["id"];
            var apartment = await _repository.Apartment.GetApartmentAsync(houseId, id, trackChanges);

            if (apartment == null)
            {
                _logger.LogInfo($"Apartment with id: {id} doesn't exist in the database.");
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("apartment", apartment);
                await next();
            }
        }
    }
}
