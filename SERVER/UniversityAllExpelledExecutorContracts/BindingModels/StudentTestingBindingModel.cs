using UniversityAllExpelledExecutorContracts.Enums;

namespace UniversityAllExpelledExecutorContracts.BindingModels
{
    public class StudentTestingBindingModel
    {
        public int TestingId { get; set; }
        public int StudentId { get; set; }
        public Marks Mark { get; set; }
    }
}
