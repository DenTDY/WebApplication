using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class ApartmentForUpdateDto
    {
        public int ApartmentNumber { get; set; }
        public int NumberRooms { get; set; }
        public string? Cost { get; set; }
    }
}
