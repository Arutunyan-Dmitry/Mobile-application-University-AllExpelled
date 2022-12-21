using UniversityAllExpelledExecutorContracts.Enums;

namespace UniversityAllExpelledExecutorContracts.BindingModels
{
    public class StudentStatementBindingModel
    {
        public int StatementId { get; set; }
        public int StudentId { get; set; }
        public Achievements Achievement { get; set; }
    }
}
