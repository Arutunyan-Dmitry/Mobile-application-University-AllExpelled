namespace UniversityAllExpelledDataBaseImplement.Models
{
    public class TypeReportingDiscipline
    {
        public int Id { get; set; }
        public int TypeReportingId { get; set; }
        public int DisciplineId { get; set; } 
        public virtual TypeReporting TypeReporting { get; set; }
        public virtual Discipline Discipline { get; set; }
    }
}
