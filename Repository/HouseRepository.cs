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

        public void CreateHouse(House house)
        {
            Create(house);
        }

        public IEnumerable<House> GetAllHouses(bool trackChanges)
        {
            return FindAll(trackChanges)
                .OrderBy(c => c.Address)
                .ToList();
        }

        public IEnumerable<House> GetByIds(IEnumerable<Guid> ids, bool trackChanges)
        {
            return FindByCondition(x =>
                ids.Contains(x.Id), trackChanges).ToList();
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
