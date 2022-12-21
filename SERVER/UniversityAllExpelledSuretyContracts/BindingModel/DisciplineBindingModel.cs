using System.Collections.Generic;

namespace UniversityAllExpelledSuretyContracts.BindingModel
{
    public class DisciplineBindingModel
    {
        public int? Id { get; set; }
        public int DepartmentId { get; set; }
        public int StatementId { get; set; }
        public string DisciplineName { get; set; }
        public Dictionary<int, string> DisciplineTypeReporting { get; set; }
    }
}
