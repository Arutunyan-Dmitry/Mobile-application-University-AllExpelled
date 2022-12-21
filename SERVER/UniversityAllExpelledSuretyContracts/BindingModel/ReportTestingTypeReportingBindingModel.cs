using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityAllExpelledSuretyContracts.BindingModel
{
    public class ReportTestingTypeReportingBindingModel
    {
        public string FileName { get; set; }
        public int DepartmentId { get; set; }
        public List<int> TypeReportingId { get; set; }
    }
}
