using Entities.Models;
using Repository.Extensions.Utility;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic;

namespace Repository.Extensions
{
    public static class RepositoryApartmentExtensions
    {
        public static IQueryable<Apartment> FilterApartments(this IQueryable<Apartment> apartments, uint minNumberRoom, uint maxNumberRoom) =>
            apartments.Where(e => (e.NumberRooms >= minNumberRoom && e.NumberRooms <= maxNumberRoom));

        public static IQueryable<Apartment> Search(this IQueryable<Apartment> apartments, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return apartments;
            }

            var lowerCaseTerm = searchTerm.Trim().ToLower();

            return apartments.Where(e => e.Cost.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<Apartment> Sort(this IQueryable<Apartment> apartments, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                return apartments.OrderBy(e => e.Cost);
            }

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(Apartment).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                {
                    continue;
                }

                var propertyFromQueryName = param.Split(' ')[0];
                var objectProperty = propertyInfos
                    .FirstOrDefault(pi => pi.Name
                    .Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null)
                {
                    continue;
                }

                var direction = param.EndsWith(" desc") ? "descending" : "ascending";
                orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {direction},");
            }


            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Apartment>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
            {
                return apartments.OrderBy(e => e.Cost);
            }

            return apartments.OrderBy(orderQuery);
        }
    }
}