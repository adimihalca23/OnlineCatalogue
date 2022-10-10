using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Mark
    {
        [Required(ErrorMessage = "Mark id is required")]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Value mark is required")]
        [Range(1, 10)]
        public int Value { get; set; }
        [Required(ErrorMessage = "Subject id is required")]
        [Range(1, int.MaxValue)]
        public int SubjectId { get; set; }
        [Required(ErrorMessage = "Student id is required")]
        [Range(1, int.MaxValue)]
        public int StudentId { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
