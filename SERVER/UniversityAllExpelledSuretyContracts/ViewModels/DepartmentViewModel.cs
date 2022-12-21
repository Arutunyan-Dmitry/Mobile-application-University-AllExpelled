using System.ComponentModel;

namespace UniversityAllExpelledSuretyContracts.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название кафедры")]
        public string DepartmentName { get; set; }
        [DisplayName("Логин")]
        public string Login { get; set; }
        [DisplayName("Пароль")]
        public string Password { get; set; }
    }
}
