using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configuration
{
    public class HouseConfiguration : IEntityTypeConfiguration<House>
    {
        public void Configure(EntityTypeBuilder<House> builder)
        {
            builder.HasData
            (
                new House
                {
                     Id = new Guid("cf07407f-a0c4-46cc-870b-850d29c6ac93"),
                     Address = "Саранск, ул. Коммунистическая, 25а",
                     YearConstruction = 2010,
                     NumberFloors = 9
                },
                new House
                {
                    Id = new Guid("a539dd53-125b-4eef-9cbe-ab9d9b5bfdd8"),
                    Address = "Саранск, ул. Лесная, 64",
                    YearConstruction = 2006,
                    NumberFloors = 5
                }
             );
        }
    }
}
