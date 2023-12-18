using Entities.Models;

namespace Contracts
{
    public interface IHouseRepository
    {
        public void TestHouse();
        Task<IEnumerable<House>> GetAllHousesAsync(bool trackChanges);
        Task<House> GetHouseAsync(Guid houseId, bool trackChanges);
        void CreateHouse(House house);
        Task<IEnumerable<House>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteHouse(House house);
    }
}
