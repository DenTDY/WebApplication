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

        public IEnumerable<House> GetAllHouses(bool trackChanges)
        {
            return FindAll(trackChanges)
                .OrderBy(c => c.Address)
                .ToList();
        }

        public void TestHouse()
        {
           
        }
    }
}
