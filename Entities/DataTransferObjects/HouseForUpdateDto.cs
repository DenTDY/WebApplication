using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class HouseForUpdateDto
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? NumberFloors { get; set; }
        public int YearConstruction { get; set; }
        public IEnumerable<ApartmentForUpdateDto> Apartments { get; set; }
    }
}
