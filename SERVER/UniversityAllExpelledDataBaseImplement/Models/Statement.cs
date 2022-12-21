using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityAllExpelledDataBaseImplement.Models
{
    public class Statement
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        [Required]
        public string Naming { get; set; }
        [Required]
        public DateTime DateCreate { get; set; }
        [ForeignKey("StatementId")]
        public virtual List<StudentStatement> StudentStatements { get; set; }
        [ForeignKey("StatementId")]
        public virtual List<Discipline> Disciplines { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
