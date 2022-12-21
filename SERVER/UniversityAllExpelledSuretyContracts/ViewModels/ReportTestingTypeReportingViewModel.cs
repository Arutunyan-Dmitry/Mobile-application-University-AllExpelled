using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using UniversityAllExpelledExecutorContracts.Enums;

namespace UniversityAllExpelledSuretyContracts.ViewModels
{
    public class ReportTestingTypeReportingViewModel
    {
        public int ReportNumber { get; set; }
        public string ReportName { get; set; }
        public Dictionary<string, string> TestingNameType { get; set; }
    }
}
