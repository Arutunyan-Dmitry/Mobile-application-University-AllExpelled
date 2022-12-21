using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityAllExpelledDataBaseImplement.Models
{
    public class Discipline
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public int StatementId { get; set; }
        [Required]
        public string DisciplineName { get; set; }
        [ForeignKey("DisciplineId")]
        public virtual List<TypeReportingDiscipline> TypeReportingDisciplines { get; set; }
        public virtual Statement Statement { get; set; }
        public virtual Department Department { get; set; }
    }
}
