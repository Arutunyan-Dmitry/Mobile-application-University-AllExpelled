using System.ComponentModel;

namespace UniversityAllExpelledExecutorContracts.ViewModels
{
    public class StudentViewModel
    {
        public int Id { get; set; }
        [DisplayName("Номер ЗК")]
        public string NumbGB { get; set; }
        [DisplayName("Имя")]
        public string Name { get; set; }
        [DisplayName("Фамилия")]
        public string Surname { get; set; }
        [DisplayName("Отчество")]
        public string MiddleName { get; set; }
    }
}
