using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityAllExpelledDataBaseImplement.Models
{
    public class Student
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        [Required]
        public string NumbGB { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string MiddleName { get; set; }
        [ForeignKey("StudentId")]
        public virtual List<StudentTesting> StudentTestings { get; set; }
        [ForeignKey("StudentId")]
        public virtual List<StudentStatement> StudentStatements { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
