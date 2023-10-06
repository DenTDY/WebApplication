using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configuration
{
    public class ApartmentConfiguration : IEntityTypeConfiguration<Apartment>
    {
        public void Configure(EntityTypeBuilder<Apartment> builder)
        {
            builder.HasData
            (
            new Apartment
            {
                Id = new Guid("1e707554-3a47-43d1-aae7-e990d385081b"),
                ApartmentNumber = 10,
                NumberRooms = 4,
                Cost = "6000000",
                HouseId = new Guid("cf07407f-a0c4-46cc-870b-850d29c6ac93")
            },
            new Apartment
            {
                Id = new Guid("4d57c4a3-b6c3-464f-bad1-6550d4bf3182"),
                ApartmentNumber = 15,
                NumberRooms = 2,
                Cost = "4000000",
                HouseId = new Guid("cf07407f-a0c4-46cc-870b-850d29c6ac93")
            },
            new Apartment
            {
                Id = new Guid("621d298f-8734-4cbb-814b-9a126c5ba0da"),
                ApartmentNumber = 8,
                NumberRooms = 3,
                Cost = "4500000",
                HouseId = new Guid("cf07407f-a0c4-46cc-870b-850d29c6ac93")
            },
            new Apartment
            {
                Id = new Guid("d67977ae-cb1a-4747-8a02-2b0b2e7d1bd8"),
                ApartmentNumber = 17,
                NumberRooms = 1,
                Cost = "2500000",
                HouseId = new Guid("a539dd53-125b-4eef-9cbe-ab9d9b5bfdd8")
            },
            new Apartment
            {
                Id = new Guid("e4ce4d0b-0725-4a56-8ce9-49333bc3682a"),
                ApartmentNumber = 28,
                NumberRooms = 1,
                Cost = "2800000",
                HouseId = new Guid("a539dd53-125b-4eef-9cbe-ab9d9b5bfdd8")
            });
        }
    }
}
