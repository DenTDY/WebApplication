namespace Entities.DataTransferObjects.House
{
    public class HouseDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? AddressAndNumberFloors { get; set; }
        public int YearConstruction { get; set; }
    }
}
