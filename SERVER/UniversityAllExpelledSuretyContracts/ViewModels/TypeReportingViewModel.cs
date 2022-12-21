using System.ComponentModel;

namespace UniversityAllExpelledSuretyContracts.ViewModels
{
    public class TypeReportingViewModel
    {
        public int Id { get; set; }
        [DisplayName("Номер формы отчета")]
        public int ReportNumber { get; set; }
        [DisplayName("Название отчета")]
        public string ReportName { get; set; }
    }
}
