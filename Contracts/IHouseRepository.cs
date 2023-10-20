using Entities.Models;

namespace Contracts
{
    public interface IHouseRepository
    {
        public void TestHouse();
        IEnumerable<House> GetAllHouses(bool trackChanges);
        House GetHouse(Guid houseId, bool trackChanges);
    }
}
