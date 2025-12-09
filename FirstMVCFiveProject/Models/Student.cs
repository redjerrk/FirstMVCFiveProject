
using System.ComponentModel.DataAnnotations;

namespace FirstMVCFiveProject.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Roll No")]
        public int RollNo { get; set; }

        [Required]
        [Display(Name = "Student Name")]
        public string Name { get; set; }

        [Required]
        [Range(1, 120)]
        public int Age { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}