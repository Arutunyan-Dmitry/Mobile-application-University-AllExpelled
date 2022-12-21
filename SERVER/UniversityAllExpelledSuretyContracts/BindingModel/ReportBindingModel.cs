using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityAllExpelledSuretyContracts.BindingModel
{
    public class ReportBindingModel
    {
        public int DepartmentId { get; set; }
        public string FileName { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
