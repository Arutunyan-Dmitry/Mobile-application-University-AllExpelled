namespace UniversityAllExpelledDataBaseImplement.Models
{
    public class TypeReportingLesson
    {
        public int Id { get; set; }
        public int TypeReportingId { get; set; }
        public int LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }
        public virtual TypeReporting TypeReporting { get; set; }
    }
}
