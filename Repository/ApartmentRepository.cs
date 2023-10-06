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

        public void TestApartment()
        {
           
        }
    }
}
