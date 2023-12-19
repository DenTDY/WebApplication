using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects.Apartment
{
    public abstract class ApartmentForManipulationDto
    {
        [Required(ErrorMessage = "Apartment number of rooms is a required field.")]
        [MaxLength(2, ErrorMessage = "Maximum length for the NumberRooms is 2 characters.")]
        public int NumberRooms { get; set; }

        [MaxLength(4, ErrorMessage = "Maximum length for the ApartmentNumber is 4 characters")]
        public int ApartmentNumber { get; set; }

        [Required(ErrorMessage = "Cost is a required field.")]
        [MaxLength(20, ErrorMessage = "Maximum length for the Cost is 20 characters.")]
        public string Cost { get; set; }
    }
}
