using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Subject
    {
        [Required(ErrorMessage = "Subject id is required")]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Subject name is required")]
        [MaxLength(50, ErrorMessage = "Maximum length is 50 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Teacher id is required")]
        [Range(1, int.MaxValue)]
        public int TeacherId { get; set; }
    } 
}
  