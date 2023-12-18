using Entities.Models;
using Entities.RequestFeatures;

namespace Contracts
{
    public interface IApartmentRepository
    {
        public void TestApartment();
        Task<PagedList<Apartment>> GetApartmentsAsync(Guid houseId, ApartmentParameters employeeParametrs, bool trackChanges);
        Task<Apartment> GetApartmentAsync(Guid houseId, Guid id, bool trackChanges);
        void CreateApartmentForHouse(Guid houseId, Apartment apartment);
        void DeleteApartment(Apartment apartment);
    }
}
