using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Address
    {
        [Required(ErrorMessage = "Adress id is required")]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
        [Required(ErrorMessage = "City is required")]
        [MaxLength(50, ErrorMessage = "Maximum length is 50 characters")]
        public string City { get; set; }
        [Required(ErrorMessage = "Street is required")]
        [MaxLength(50, ErrorMessage = "Maximum length is 50 characters")]
        public string Street { get; set; }
        [Required(ErrorMessage = "Number is required")]
        [Range(1, int.MaxValue)]
        public int Number { get; set; }
        public int? StudentId { get; set; }
        public int? TeacherId { get; set; }
    }
}