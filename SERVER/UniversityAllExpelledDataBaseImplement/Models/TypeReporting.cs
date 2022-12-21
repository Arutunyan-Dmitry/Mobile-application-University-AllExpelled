using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityAllExpelledDataBaseImplement.Models
{
    public class TypeReporting
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        [Required]
        public int ReportNumber { get; set; }
        [Required]
        public string ReportName { get; set; }     
        [ForeignKey("TypeReportingId")]
        public virtual List<TypeReportingLesson> TypeReportingLessons { get; set; }
        [ForeignKey("TypeReportingId")]
        public virtual List<TypeReportingDiscipline> TypeReportingDisciplines { get; set; }
        public virtual Department Department { get; set; }

    }
}
