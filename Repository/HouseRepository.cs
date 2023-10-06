using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class HouseRepository : RepositoryBase<House>, IHouseRepository
    {
        public HouseRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }

        public void TestHouse()
        {
           
        }
    }
}
