using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class HouseForCreationDto
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? NumberFloors { get; set; }
        public int YearConstruction { get; set; }
        public IEnumerable<ApartmentForCreationDto> Apartments{ get; set; }
    }
}
