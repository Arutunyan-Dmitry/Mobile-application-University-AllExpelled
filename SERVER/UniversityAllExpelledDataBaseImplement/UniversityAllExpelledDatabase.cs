using UniversityAllExpelledDataBaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace UniversityAllExpelledDataBaseImplement
{
    public class UniversityAllExpelledDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=ArutunyanWin64\SQLEXPRESS;Initial Catalog=UniversityAllExpelledDatabase;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Department> Departments { set; get; }
        public virtual DbSet<Discipline> Disciplines { set; get; }
        public virtual DbSet<Lesson> Lessons { set; get; }
        public virtual DbSet<Statement> Statements { set; get; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentStatement> StudentStatements { get; set; }
        public virtual DbSet<StudentTesting> StudentTestings { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<Testing> Testings { get; set; }
        public virtual DbSet<TypeReporting> TypeReportings { get; set; }
        public virtual DbSet<TypeReportingDiscipline> TypeReportingDisciplines { get; set; }
        public virtual DbSet<TypeReportingLesson> TypeReportingLessons { get; set; }
    }
}
