using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityAllExpelledDataBaseImplement.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required]
        public string DepartmentName { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [ForeignKey("DepartmentId")]
        public virtual List<TypeReporting> TypeReportings { get; set; }
        [ForeignKey("TypeReportingId")]
        public virtual List<Lesson> Lessons { get; set; }
        [ForeignKey("TypeReportingId")]
        public virtual List<Discipline> Disciplines { get; set; }
    }
}
