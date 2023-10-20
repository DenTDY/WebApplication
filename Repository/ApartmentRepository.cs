using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class ApartmentRepository : RepositoryBase<Apartment>, IApartmentRepository
    {
        public ApartmentRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public Apartment GetApartment(Guid houseId, Guid id, bool trackChanges)
        {
            return FindByCondition(
                e => e.HouseId.Equals(houseId) && e.Id.Equals(id), trackChanges)
                .SingleOrDefault();
        }

        public IEnumerable<Apartment> GetApartments(Guid houseId, bool trackChanges)
        {
            return FindByCondition(e => e.HouseId.Equals(houseId), trackChanges).OrderBy(e => e.ApartmentNumber);
        }

        public void TestApartment()
        {
           
        }
    }
}
