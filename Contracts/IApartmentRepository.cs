using Entities.Models;

namespace Contracts
{
    public interface IApartmentRepository
    {
        public void TestApartment();
        IEnumerable<Apartment> GetApartments(Guid houseId, bool trackChanges);
        Apartment GetApartment(Guid houseId, Guid id, bool trackChanges);
        void CreateApartmentForHouse(Guid houseId, Apartment apartment);
        void DeleteApartment(Apartment apartment);
    }
}
