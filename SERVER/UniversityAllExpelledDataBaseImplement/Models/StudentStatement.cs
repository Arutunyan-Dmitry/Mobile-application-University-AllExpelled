using System.ComponentModel.DataAnnotations;
using UniversityAllExpelledExecutorContracts.Enums;

namespace UniversityAllExpelledDataBaseImplement.Models
{
    public class StudentStatement
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int StatementId { get; set; }
        [Required]
        public Achievements Achievement { get; set; }
        public virtual Student Student { get; set; }
        public virtual Statement Statement { get; set; }
    }
}
