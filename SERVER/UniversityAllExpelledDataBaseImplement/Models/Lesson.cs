using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityAllExpelledDataBaseImplement.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        [Required]
        public string LessonName { get; set; }
        [Required]
        public DateTime LessonDate { get; set; }
        [ForeignKey("LessonId")]
        public virtual List<TypeReportingLesson> TypeReportingLessons { get; set; }
        [ForeignKey("LessonId")]
        public virtual List<Testing> Testings { get; set; }
        public virtual Department Department { get; set; }
    }
}
