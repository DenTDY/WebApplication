using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class House
    {
        [Column("HouseId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "House address is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Address is 60 characters.")] 
        public string Address { get; set; }
                
        [Required(ErrorMessage = "House year of construction is a required field.")]
        [MaxLength(4, ErrorMessage = "Maximum length for rhe YearConstruction is 4 characte")]
        public int YearConstruction { get; set; }

        [Required(ErrorMessage = "House number of floors is a required field.")]
        [MaxLength(3, ErrorMessage = "Maximum length for rhe NumberFloors is 3 characte")]
        public int NumberFloors { get; set; }

        public ICollection<Apartment> Apartments { get; set; }
    }
}
