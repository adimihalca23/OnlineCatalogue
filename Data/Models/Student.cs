using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Student
    {
        [Required(ErrorMessage = "Student id is required")]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Student first name is required")]
        [MaxLength(50, ErrorMessage = "Maximum length is 50 characters")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Student last name is required")]
        [MaxLength(50, ErrorMessage = "Maximum length is 50 characters")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Student age is required")]
        [Range(1, 100)]
        public int Age { get; set; }
        public Address Address{ get; set; }
        public List<Mark> Marks { get; set; } = new List<Mark>();
    }
}
