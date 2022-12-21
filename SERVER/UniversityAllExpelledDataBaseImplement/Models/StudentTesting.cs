using System.ComponentModel.DataAnnotations;
using UniversityAllExpelledExecutorContracts.Enums;

namespace UniversityAllExpelledDataBaseImplement.Models
{
    public class StudentTesting
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int TestingId { get; set; }
        [Required]
        public Marks Mark { get; set; }
        public virtual Student Student { get; set; }
        public virtual Testing Testing { get; set; }
    }
}
