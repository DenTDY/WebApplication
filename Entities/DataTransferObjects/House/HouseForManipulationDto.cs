using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DataTransferObjects.Apartment;

namespace Entities.DataTransferObjects.House
{
    public abstract class HouseForManipulation
    {
        [Required(ErrorMessage = "House name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "House address is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Address is 30 characters.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "House country is a required field.")]
        [MaxLength(3, ErrorMessage = "Maximum length for the NumberFloors is 3 characters.")]
        public string NumberFloors { get; set; }

        [Required(ErrorMessage = "House country is a required field.")]
        [MaxLength(5, ErrorMessage = "Maximum length for the YearConstruction is 5 characters.")]
        public int YearConstruction { get; set; }


        public IEnumerable<ApartmentForManipulationDto> Apartments { get; set; }
    }
}
