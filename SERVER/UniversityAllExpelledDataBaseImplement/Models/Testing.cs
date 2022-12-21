using System.Collections.Generic;
using UniversityAllExpelledExecutorContracts.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityAllExpelledDataBaseImplement.Models
{
    public class Testing
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int LessonId { get; set; }
        [Required]
        public string Naming { get; set; }
        [Required]
        public TestingTypes Type { get; set; }
        [ForeignKey("TestingId")]
        public virtual List<StudentTesting> StudentTestings { get; set; }
        public virtual Lesson Lesson { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
