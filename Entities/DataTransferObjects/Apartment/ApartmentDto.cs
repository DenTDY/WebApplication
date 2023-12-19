namespace Entities.DataTransferObjects.Apartment
{
    public class ApartmentDto
    {
        public Guid Id { get; set; }
        public int ApartmentNumber { get; set; }
        public int NumberRooms { get; set; }
        public string? Cost { get; set; }
    }
}
