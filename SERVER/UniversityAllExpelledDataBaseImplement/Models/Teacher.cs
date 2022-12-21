using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityAllExpelledDataBaseImplement.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string MiddleName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [ForeignKey("TeacherId")]
        public virtual List<Student> Students { get; set; }
        [ForeignKey("TeacherId")]
        public virtual List<Testing> Testings { get; set; }
        [ForeignKey("TeacherId")]
        public virtual List<Statement> Statements { get; set; }
    }
}
