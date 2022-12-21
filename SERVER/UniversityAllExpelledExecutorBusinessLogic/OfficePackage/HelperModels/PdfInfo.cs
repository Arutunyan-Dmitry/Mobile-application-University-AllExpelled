using UniversityAllExpelledExecutorContracts.ViewModels;
using System;
using System.Collections.Generic;

namespace UniversityAllExpelledExecutorBusinessLogic.OfficePackage.HelperModels
{
    public class PdfInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public List<ReportStudentStatementTestingViewModel> StudStatTest { get; set; }
    }
}