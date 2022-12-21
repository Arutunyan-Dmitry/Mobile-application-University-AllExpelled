using System.Collections.Generic;

namespace UniversityAllExpelledSuretyContracts.BindingModel
{
    public class TypeReportingBindingModel
    {
        public int? Id { get; set; }
        public int ReportNumber { get; set; }
        public string ReportName { get; set; }
        public int DepartmentId { get; set; }
        public List<int> ReportsId { get; set; }
    }
}
