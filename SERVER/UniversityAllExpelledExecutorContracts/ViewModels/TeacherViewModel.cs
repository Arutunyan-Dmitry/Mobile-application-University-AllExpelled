using System.ComponentModel;

namespace UniversityAllExpelledExecutorContracts.ViewModels
{
    public class TeacherViewModel
    {
        public int Id { get; set; }
        [DisplayName("Имя")]
        public string Name { get; set; }
        [DisplayName("Фамилия")]
        public string Surname { get; set; }
        [DisplayName("Отчество")]
        public string MiddleName { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Пароль")]
        public string Password { get; set; }
    }
}
