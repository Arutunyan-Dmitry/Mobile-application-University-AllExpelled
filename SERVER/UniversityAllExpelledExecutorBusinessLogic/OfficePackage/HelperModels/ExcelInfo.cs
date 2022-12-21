using UniversityAllExpelledExecutorContracts.ViewModels;
using System.Collections.Generic;

namespace UniversityAllExpelledExecutorBusinessLogic.OfficePackage.HelperModels
{
    public class ExcelInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportStudentDisciplineViewModel> StudDisc { get; set; }
    }
}