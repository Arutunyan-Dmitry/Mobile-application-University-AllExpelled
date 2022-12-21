
namespace UniversityAllExpelledExecutorContracts.BindingModels
{
    public class StudentBindingModel
    {
        public int? Id { get; set; }
        public int TeacherId { get; set; }
        public string NumbGB { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MiddleName { get; set; }
        public int[] Students { get; set; }
    }
}
