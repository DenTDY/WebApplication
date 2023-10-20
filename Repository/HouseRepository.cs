using Contracts;
using Entities;
using Entities.Models;
using System.ComponentModel.Design;

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

        public House GetHouse(Guid houseId, bool trackChanges)
        {
            return FindByCondition(
                 c => c.Id.Equals(houseId), trackChanges).SingleOrDefault();
        }

        public void TestHouse()
        {
           
        }
    }
}
