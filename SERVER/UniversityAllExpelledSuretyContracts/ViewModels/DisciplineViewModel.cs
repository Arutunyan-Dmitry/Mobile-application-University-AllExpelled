using System.Collections.Generic;
using System.ComponentModel;

namespace UniversityAllExpelledSuretyContracts.ViewModels
{
    public class DisciplineViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название ведомости")]
        public string StatementName { get; set; }
        [DisplayName("Название дисциплины")]
        public string DisciplineName { get; set; }
        public Dictionary<int, string> DisciplineTypeReporting { get; set; }
    }
}
