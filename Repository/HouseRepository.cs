using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
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

        public void DeleteHouse(House house)
        {
            Delete(house);
        }

        public async Task<IEnumerable<House>> GetAllHousesAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(c => c.Address)
                .ToListAsync();
        }

        public async Task<IEnumerable<House>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
        {
            return await FindByCondition(x =>
                ids.Contains(x.Id), trackChanges).ToListAsync();
        }

        public async Task<House> GetHouseAsync(Guid houseId, bool trackChanges)
        {
            return await FindByCondition(
                 c => c.Id.Equals(houseId), trackChanges).SingleOrDefaultAsync();
        }

        public void TestHouse()
        {
           
        }
    }
}
