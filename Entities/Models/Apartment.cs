using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Apartment
    {
        [Column("ApartmentId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "ApartmentNumber is a required field.")]
        [MaxLength(4, ErrorMessage = "Maximum length for the ApartmentNumber is 4 characters.")]
        public int ApartmentNumber { get; set; }

        [Required(ErrorMessage = "Apartment number of rooms is a required field.")]
        [MaxLength(2, ErrorMessage = "Maximum length for the NumberRooms is 2 characters.")] 
        public int NumberRooms { get; set; }

        [Required(ErrorMessage = "Cost is a required field.")]
        [MaxLength(20, ErrorMessage = "Maximum length for the Cost is 20 characters.")]
        public string Cost { get; set; }

        [ForeignKey(nameof(House))]
        public Guid HouseId { get; set; }

        public House? House { get; set; }
    }
}
